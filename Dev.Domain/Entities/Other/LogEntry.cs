using Dev.Enums;
using System;
using System.Linq;

namespace Dev.Domain.Core
{
    public class LogEntry
    {
        public DateTime Timestamp { get; set; }
        public LogType Type { get; set; }
        public string Message { get; set; }
        public object[] PropertyValues { get; set; } = new object[0];

        public override string ToString()
        {
            return PropertyValues.Any() ? string.Format(Message, PropertyValues) : Message;
        }
    }
}
