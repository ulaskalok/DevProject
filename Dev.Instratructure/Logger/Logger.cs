using System;
using System.IO;
using System.Linq;
using Dev.Domain.Core;
using Dev.Domain.Interfaces;
using Dev.Enums;
using Serilog;

namespace Dev.Instratructure.Logger
{
    public class Logger : Dev.Domain.Interfaces.ILogger
    {
        public event LoggedMessage LoggedMessage;
        private readonly Serilog.ILogger _serilog;

        public Logger()
        {
            var logFilePath = GetLogFilePath();

            var loggerConfig = new LoggerConfiguration();
            loggerConfig.MinimumLevel.Verbose();
            loggerConfig.WriteTo.RollingFile(logFilePath, shared: true);
            loggerConfig.WriteTo.ColoredConsole();

            _serilog = loggerConfig.CreateLogger();
        }

        public void Verbose(string message, params object[] propertyValues)
        {
            _serilog.Verbose(message, propertyValues);

            LogMessage(LogType.Verbose, message, propertyValues);
        }

        public void Info(string message, params object[] propertyValues)
        {
            _serilog.Information(message, propertyValues);

            LogMessage(LogType.Info, message, propertyValues);
        }

        public void Debug(string message, params object[] propertyValues)
        {
            _serilog.Debug(message, propertyValues);

            LogMessage(LogType.Debug, message, propertyValues);
        }

        private void LogMessage(LogType Type, string message, params object[] propertyValues)
        {
            LoggedMessage?.Invoke(new LogEntry
            {
                Message = message,
                PropertyValues = propertyValues.ToArray(),
                Type = Type,
                Timestamp = DateTime.Now
            });
        }
        private static string GetLogFilePath()
        {
            var sdir = GetLogDirectory();
            return Path.Combine(sdir, "Devlog.log");
        }

        private static string GetLogDirectory()
        {
            var programDataDir = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
            var sdir = Path.Combine(programDataDir, "Dev");

            EnsureLogDirectoryExists(sdir);

            return sdir;
        }

        private static void EnsureLogDirectoryExists(string dir)
        {
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
        }
    }
}
