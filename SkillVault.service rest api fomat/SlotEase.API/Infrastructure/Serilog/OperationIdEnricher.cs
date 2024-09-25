using Serilog.Core;
using Serilog.Events;
using System.Diagnostics;

namespace SlotEase.API.Serilog;

/// <summary>
/// 
/// </summary>
public class OperationIdEnricher : ILogEventEnricher
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="logEvent"></param>
    /// <param name="propertyFactory"></param>
    public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
    {
        Activity activity = Activity.Current;

        if (activity is null) return;

        logEvent.AddPropertyIfAbsent(new LogEventProperty(LoggingConstants.OperationId, new ScalarValue(activity.TraceId)));
        if (activity.Parent != null)
        {
            // Like internal method in AI W3CUtilities.FormatTelemetryId
            string parentId = $"|{activity.TraceId}.{activity.Parent.SpanId}.";
            logEvent.AddPropertyIfAbsent(new LogEventProperty(LoggingConstants.ParentId, new ScalarValue(parentId)));
        }
        //logEvent.AddPropertyIfAbsent(new LogEventProperty("Parent Id", new ScalarValue(activity.ParentId)));
    }
}
