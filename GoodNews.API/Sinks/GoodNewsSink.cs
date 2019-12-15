using System;
using Serilog.Core;
using Serilog.Events;

namespace GoodNews.API.Sinks
{
    public class GoodNewsSink : ILogEventSink
    {
        private readonly IFormatProvider _formatProvider;

        public GoodNewsSink(IFormatProvider formatProvider)
        {
            _formatProvider = formatProvider;
        }

        public void Emit(LogEvent logEvent)
        {
            var message = logEvent.RenderMessage(_formatProvider);
            var logLevel = logEvent.Level;

            Console.WriteLine($"{DateTime.Now} [{logLevel.ToString().ToUpperInvariant()}] {message}");

        }
    }
}
