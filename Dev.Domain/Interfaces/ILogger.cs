using Dev.Domain.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dev.Domain.Interfaces
{
    public delegate void LoggedMessage(LogEntry logEntry);

    public interface ILogger
    {
        event LoggedMessage LoggedMessage;

        void Verbose(string message, params object[] propertyValues);
        void Info(string message, params object[] propertyValues);
        void Debug(string message, params object[] propertyValues);
    }
}
