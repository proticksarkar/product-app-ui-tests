using System;
using System.IO;

namespace WebUIAutomationTestFramework.Params
{
    public static class DefaultProperties
    {
        public static string TestResultsLocation => Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).FullName
                                                    + @"\TestResults";
        public static string LogLocation => TestResultsLocation 
                                            + @"\Logs\Log_" + DateTime.Now.ToString("yyyyMMdd_hhmmss") + ".json";
        public static string ExtentReportsLocation => TestResultsLocation
                                                      + @"\ExtentReports\index.html";
    }
}
