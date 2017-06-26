using System;
using System.Configuration;

namespace ManagerSystem.WebUI.Util
{
    public static class ConstantStorage
    {
        public static int pageSize = Int32.Parse(ConfigurationManager.AppSettings["pageSize"]);
        public static string AllRecordsInListValue = "All";
    }
}