using System.Configuration;

namespace ManagerSystem.WebUI.Util
{
    public static class ConstantStorage
    {
        public static readonly int PageSize = int.Parse(ConfigurationManager.AppSettings["pageSize"]);
        public const string AllRecordsInDropDownListValue = "All";
        public const string ConnectionString = "DefaultConnection";
        public const string LoginError = "Incorrect login or password";
    }
}