using ExamLayer.Exceptions;
using ExamLayer.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using Microsoft.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace ExamLayer.Middlewares
{
    public class ClientAuthenticationHandler : AuthenticationHandler<ClientAuthenticationSchemeOptions>
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public ClientAuthenticationHandler(
            IOptionsMonitor<ClientAuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            IHttpClientFactory httpClientFactory,
            IConfiguration configuration
            )
            : base(options, logger, encoder, clock)
        {
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            //模組路徑
            var modulePrefixPath = ""; // /dataverifyapi
            //var currpath = string.Format("{0} {1}{2}", Context.Request.Method.ToUpper(), modulePrefixPath, Context.Request.Path.Value?.ToLower() ?? "");
            //_logger.LogInformation($"Ingress Path: {currpath}");

            //模組白名單 
            var whiteList = new List<string>{
                        //$"GET {modulePrefixPath}/api/DataVerify/All", //TODO:外部先放行(for dataplatform)
                        //$"GET {modulePrefixPath}/api/Worker/GetDefinitionFromDataPlatform", //TODO:排程先放行
                        //$"GET {modulePrefixPath}/api/Worker/GetDefinitionFromFormDesign", //TODO:排程先放行
                        //$"GET {modulePrefixPath}/api/Worker/FormDesignDataSync", //TODO:排程先放行
                        //$"POST {modulePrefixPath}/api/DataVerify/DataplatformVerifyData", //TODO:外部先放行(for dataplatform)
                    };

            if (Context.Request.Headers.TryGetValue(HeaderNames.Authorization, out StringValues token))
            {
                try
                {
                    //驗證=> post authserver check
                    var authServer = _configuration.GetValue<string>("AuthServer");
                    var httpRequestMessage =
                      new HttpRequestMessage(HttpMethod.Get, $"{authServer}/api/Auth/ValidateToken")
                      {
                          Headers =
                          {
                              { HeaderNames.Authorization,$"{token}" }
                          },
                      };
                    var client = _httpClientFactory.CreateClient();
                    var response = client.SendAsync(httpRequestMessage).Result;
                    if (!response.IsSuccessStatusCode)
                        throw new ApiException(Enums.ApiEnum.ErrorCode.FeatureNotUse);
                    using var contentStream = response.Content.ReadAsStream();
                    var loginInfo = JsonSerializer.Deserialize<BaseOutput<GetCurrentUserInfoOutput>>(contentStream)?.Data;

                    //模組白名單 
                    loginInfo.Features.AddRange(whiteList);

                    //功能卡控
                    VerifyFeature(modulePrefixPath, loginInfo.Features);

                    //建立Claims
                    var claims = new List<Claim>
                        {
                            new Claim(ClaimsIdentity.DefaultNameClaimType, loginInfo.Name),//使用者識別
                            new Claim("Id", loginInfo.Id),//使用者識別
                            new Claim("DisplayName", loginInfo.DisplayName)//使用者識別
                        };
                    loginInfo?.Depts?.ForEach(e => claims.Add(new Claim("Dept", e)));
                    loginInfo?.Features?.ForEach(e => claims.Add(new Claim("Feature", e)));
                    var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity[]{
                            new ClaimsIdentity(
                                claims.ToArray(),
                                "AuthApiToken" //必須要加入authenticationType，否則會被作為未登入
                            )
                        });
                    return Task.FromResult(AuthenticateResult.Success(new AuthenticationTicket(claimsPrincipal, "bearer")));
                }
                catch (ApiException aex)
                {
                    throw;
                }
                catch
                {
                    throw new Exception("Token Authenticate Fail.");
                }
            }
            else
            {
                //功能卡控
                VerifyFeature(modulePrefixPath, whiteList);
            }
            return Task.FromResult(AuthenticateResult.NoResult());
        }

        /// <summary>
        /// 驗證更能是否可用
        /// </summary>
        /// <param name="modulePrefixPath"></param>
        /// <param name="allowPaths"></param>
        /// <exception cref="ApiException"></exception>
        private void VerifyFeature(string modulePrefixPath, List<string> allowPaths)
        {
            var byPass = false;
            var currpathary = string.Format("{0}{1}", modulePrefixPath, Context.Request.Path.Value).Split('/');
            var allowfeature = allowPaths.Where(e =>
                    e.Split(' ')[0].ToUpper() == Context.Request.Method.ToUpper()
                    && e.Split(' ')[1].Split('/').Length == currpathary.Length
                ).ToList();
            allowfeature.ForEach(allowpath =>
            {
                if (byPass)
                    return;
                var a = allowpath.Split(' ')[1].Split('/');
                for (int i = 0; i < currpathary.Length; i++)
                {
                    if (!a[i].StartsWith('{') && currpathary[i].ToLower() != a[i].ToLower())
                        return;
                    var id = Guid.Empty;
                    if (a[i].StartsWith('{') && !Guid.TryParse(currpathary[i], out id))
                        return;
                }
                byPass = true;
            });
            if (!byPass)
                throw new ApiException(Enums.ApiEnum.ErrorCode.FeatureNotUse);
        }
    }

    public class ClientAuthenticationSchemeOptions : AuthenticationSchemeOptions
    {
    }
}
