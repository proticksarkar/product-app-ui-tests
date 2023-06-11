using System;
using WebUIAutomationTestFramework.Driver;

namespace WebUIAutomationTestFramework.Settings;
public class TestSettings
{
    public BrowserType BrowserType { get; set; }
    public Uri? ApplicationUrl { get; set; }
    public int TimeoutInterval { get; set; }
    public int PollingInterval { get; set; }
    public Uri? SeleniumGridUrl { get; set; }
    public ExecutionType ExecutionType { get; set; }
    public MinimumLogLevel MinimumLogLevel { get; set; }
}
