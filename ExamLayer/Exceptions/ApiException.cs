using ExamLayer.Enums;

namespace ExamLayer.Exceptions
{
    public class ApiException : Exception
    {
        private ApiEnum.ErrorCode _code;

        public ApiException(ApiEnum.ErrorCode code) : base()
        {
            _code = code;
        }

        public override string Message
        {
            get
            {
                //return ErrorCodeHandle.Instance.GetDescription(_code);
                return _code.GetDescription();
            }
        }

        public ApiEnum.ErrorCode ErrorCode
        {
            get
            {
                return _code;
            }
        }
    }
}
