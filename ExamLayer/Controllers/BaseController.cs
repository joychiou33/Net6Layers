using Microsoft.AspNetCore.Mvc;

namespace ExamLayer.Controllers
{
    public class BaseController : ControllerBase
    {
        public BaseController()
        {
        }

        public string GetLoginId
        {
            get
            {
                return User.Identity.Name;
            }
        }
    }
}
