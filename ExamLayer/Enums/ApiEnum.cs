using System.ComponentModel;

namespace ExamLayer.Enums
{
    public static class ApiEnum
    {
        public enum ErrorCode : int
        {
            // 9999  未預期的錯誤
            [Description("未預期的錯誤")]
            UncatchException = 9999,

            // 1001 - 1999  Success類型回覆
            [Description("儲存成功")]
            SaveSuccess = 1001,
            [Description("上傳成功")]
            UploadSuccess = 1002,
            [Description("操作成功")]
            ExecuteSuccess = 1003,
            [Description("更新成功")]
            UpdateSuccess = 1004,

            // 2001 - 2999  操作類
            [Description("無效的參數")]
            InvalidParam = 2001,
            [Description("找不到資料")]
            DataNotFound = 2002,
            [Description("資料已存在")]
            DataExist = 2003,
            [Description("原始資料已變更")]
            DataAlreadyChanged = 2004,
            [Description("無效的時間參數")]
            InvalidDateParam = 2005,

            // 3001 - 3999  權限類
            [Description("請輸入正確的帳號密碼")]
            LoginFail = 3001,
            [Description("Token驗證失敗")]
            TokenVaildFail = 3002,
            [Description("無此使用者帳戶")]
            AccNotFound = 3003,
            [Description("該帳戶已失效")]
            AccInvalid = 3004,
            [Description("該帳戶已鎖定")]
            AccLocked = 3005,
            [Description("該帳戶未啟用")]
            AccInActive = 3006,

            [Description("該功能未未授權")]
            FeatureNotUse = 3007,
        }

        public static string GetDescription<T>(this T enumValue)
            where T : struct, IConvertible
        {
            if (!typeof(T).IsEnum)
                return null;

            var description = enumValue.ToString();
            var fieldInfo = enumValue.GetType().GetField(enumValue.ToString());

            if (fieldInfo != null)
            {
                var attrs = fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), true);
                if (attrs != null && attrs.Length > 0)
                    description = ((DescriptionAttribute)attrs[0]).Description;
            }

            return description;
        }

        public static Dictionary<string, string> GetDict<T>()
             where T : struct, System.Enum
        {
            var values = System.Enum.GetValues(typeof(T));
            var dict = new Dictionary<string, string>();
            foreach (T item in values)
                dict.Add(item.ToString(), item.GetDescription());
            return dict;
        }
    }
}
