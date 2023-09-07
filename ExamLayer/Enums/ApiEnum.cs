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

            // 1001 - 1899  Success類型回覆-前端會show
            [Description("儲存成功")]
            SaveSuccess = 1001,
            [Description("上傳成功")]
            UploadSuccess = 1002,
            [Description("操作成功")]
            ExecuteSuccess = 1003,
            [Description("更新成功")]
            UpdateSuccess = 1004,
            [Description("新增成功")]
            CreateSuccess = 1005,
            [Description("刪除成功")]
            DeleteSuccess = 1007,

            // 1901 - 1999  Success類型回覆-前端不會show
            [Description("查詢成功")]
            GetSuccess = 1901,

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
            [Description("資料庫操作失敗")]
            DatabaseExcuteFail = 2006,
            [Description("已有相同EventCode之簽核設定啟用中")]
            EnableSignSettingExist = 2007,
            [Description("無法做此操作")]
            CannotPerformOperation = 2008,
            [Description("必填參數不得為空")]
            RequiredFieldParam = 2012,

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

            [Description("DataPlatform資料表找不到來源設定")]
            DataVerifySourceNotFound = 4001,
            [Description("找不到該筆填單人")]
            DataVerifyUserNotFound = 4002,
            [Description("找不到資料欄位")]
            DataVerifyColumnNotFound = 4003,
        }
        public enum SortType : int
        {
            [Description("ASC")]
            ASC = 1,
            [Description("DESC")]
            DESC = 2
        }

        public enum BookType
        {
            Undefined,
            Adventure,
            Biography,
            Dystopia,
            Fantastic,
            Horror,
            Science,
            ScienceFiction,
            Poetry
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
