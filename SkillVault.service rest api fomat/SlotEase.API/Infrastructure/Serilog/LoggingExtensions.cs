using Serilog;
using Serilog.Configuration;
using System;

namespace SlotEase.API.Serilog;

/// <summary>
/// 
/// </summary>
public static class LoggingExtensions
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="enrichConfiguration"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static LoggerConfiguration WithOperationId(this LoggerEnrichmentConfiguration enrichConfiguration)
    {
        if (enrichConfiguration is null) throw new ArgumentNullException(nameof(enrichConfiguration));

        return enrichConfiguration.With<OperationIdEnricher>();
    }
}
