using Serilog;
using Serilog.Core;
using Serilog.Events;
using Serilog.Formatting.Json;
using WebUIAutomationTestFramework.Params;
using WebUIAutomationTestFramework.Settings;

namespace WebUIAutomationTestFramework.Helpers
{
    public interface ILogger
    {
        void Debug(string message);
        void Error(string message);
        void Fatal(string message);
        void Info(string message);
        void Verbose(string message);
        void Warn(string message);
    }

    public class Logger : ILogger
    {
        private readonly TestSettings _testSettings;
        private LoggingLevelSwitch _loggingLevelSwitch;

        public Logger(TestSettings testSettings)
        {
            _testSettings = testSettings;
            _loggingLevelSwitch = new LoggingLevelSwitch(LogEventLevel.Debug);
            SetLogLevel(testSettings.MinimumLogLevel);
            Log.Logger = new LoggerConfiguration()
                            .MinimumLevel.ControlledBy(_loggingLevelSwitch)
                            .WriteTo.File(new JsonFormatter(), DefaultProperties.LogLocation)
                            .Enrich.WithThreadId()
                            .CreateLogger();
        }

        private void SetLogLevel(MinimumLogLevel minimumLogLevel)
        {
            switch (minimumLogLevel)
            {
                case MinimumLogLevel.Verbose:
                    _loggingLevelSwitch.MinimumLevel = LogEventLevel.Verbose;
                    break;
                case MinimumLogLevel.Debug:
                    _loggingLevelSwitch.MinimumLevel = LogEventLevel.Debug;
                    break;
                case MinimumLogLevel.Info:
                    _loggingLevelSwitch.MinimumLevel = LogEventLevel.Information;
                    break;
                case MinimumLogLevel.Warn:
                    _loggingLevelSwitch.MinimumLevel = LogEventLevel.Warning;
                    break;
                case MinimumLogLevel.Error:
                    _loggingLevelSwitch.MinimumLevel = LogEventLevel.Error;
                    break;
                case MinimumLogLevel.Fatal:
                    _loggingLevelSwitch.MinimumLevel = LogEventLevel.Fatal;
                    break;
                default:
                    _loggingLevelSwitch.MinimumLevel = LogEventLevel.Debug;
                    break;
            }
        }

        /// <summary>
        /// Log event hierarchy:
        /// Verbose -> Debug -> Information -> Warning -> Error -> Fatal
        /// </summary>
        public void Verbose(string message)
        {
            Log.Logger.Verbose(message);
        }

        public void Debug(string message)
        {
            Log.Logger.Debug(message);
        }

        public void Info(string message)
        {
            Log.Logger.Information(message);
        }

        public void Warn(string message)
        {
            Log.Logger.Warning(message);
        }

        public void Error(string message)
        {
            Log.Logger.Error(message);
        }
        public void Fatal(string message)
        {
            Log.Logger.Fatal(message);
        }
    }
}

public enum MinimumLogLevel
{
    Verbose,
    Debug,
    Info,
    Warn,
    Error,
    Fatal
}
