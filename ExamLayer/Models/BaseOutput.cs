using ExamLayer.Enums;
using Microsoft.AspNetCore.Http;

namespace ExamLayer.Models
{
    public class BaseOutput<T>
    {
        private ApiEnum.ErrorCode _statusCode;
        public BaseOutput()
        {
            _statusCode = 0;
        }

        public ApiEnum.ErrorCode StatusCode
        {
            get
            {
                return _statusCode;
            }
            set
            {
                _statusCode = value;
            }
        }

        public string StatusMessage
        {
            get
            {
                return (_statusCode == 0) ? "Success" : _statusCode.GetDescription();
            }
        }

        public T? Data { get; set; }
    }



}
