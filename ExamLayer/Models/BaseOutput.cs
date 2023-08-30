using ExamLayer.Enums;
using Microsoft.AspNetCore.Http;

namespace ExamLayer.Models
{
    public class BaseOutput<T>
    {
        private ApiEnum.ErrorCode _errorCode;
        public BaseOutput()
        {
            _errorCode = 0;
        }

        public ApiEnum.ErrorCode ErrorCode
        {
            get
            {
                return _errorCode;
            }
            set
            {
                _errorCode = value;
            }
        }

        public string ErrorMessage
        {
            get
            {
                return _errorCode == 0 ? "Success" : _errorCode.GetDescription();
            }
        }

        public T? Data { get; set; }
        public int Total { get; set; }
    }



}
