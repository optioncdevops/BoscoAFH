using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.PostgreSQL;

namespace BoscoAFH.Base.Extension;

public static class SeriLogExtension
{
    public static void AddSerilogConfiguration(this IHostBuilder hostBuilder, string conString, string tableName = "logs")
    {
        // Get the current date and time as fileName
        string fileName = "serilogs/" + tableName + "_" + DateTime.Now.ToString("yyyyMMdd") + ".log";

        // Define column writers for audit logs
        var auditColumnWriters = new Dictionary<string, ColumnWriterBase>
        {
            { "Timestamp", new TimestampColumnWriter() },
            { "Message", new RenderedMessageColumnWriter() },
            { "UserId", new SinglePropertyColumnWriter("UserId", PropertyWriteMethod.Raw) },
            { "ControllerName", new SinglePropertyColumnWriter("ControllerName", PropertyWriteMethod.Raw) },
            { "ActionName", new SinglePropertyColumnWriter("ActionName", PropertyWriteMethod.Raw) },
            { "ChangeData", new SinglePropertyColumnWriter("ChangeData", PropertyWriteMethod.Raw) },
        };
        // Define column writers for audit logs
        var errorColumnWriters = new Dictionary<string, ColumnWriterBase>
        {
            { "Timestamp", new TimestampColumnWriter() },
            { "Level", new LevelColumnWriter(true, NpgsqlTypes.NpgsqlDbType.Varchar) },
            { "Message", new RenderedMessageColumnWriter() },
            { "Exception", new ExceptionColumnWriter() },
            { "Properties", new LogEventSerializedColumnWriter() }, // Optional for any other properties
        };

        // Configure Serilog with conditional logging for different levels to different tables
        Log.Logger = new LoggerConfiguration()
            .Enrich.FromLogContext() // Enrich logs with additional context
            .MinimumLevel.Information() // Set the default minimum log level
            .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
            .MinimumLevel.Override("System", LogEventLevel.Warning)
            .WriteTo.File(fileName)
            .WriteTo.Logger(lc => lc
                .Filter.ByIncludingOnly(logEvent => logEvent.Level == LogEventLevel.Information) // Only include logs with the "UserId" property
                .WriteTo.PostgreSQL(
                    connectionString: conString,
                    tableName: tableName + "_audit_log", // First table for Info logs
                    needAutoCreateTable: true,
                    columnOptions: auditColumnWriters
                )
            )
            .WriteTo.Logger(lc => lc
                .Filter.ByIncludingOnly(logEvent => logEvent.Level == LogEventLevel.Error) // Error logs only
                .WriteTo.PostgreSQL(
                    connectionString: conString,
                    tableName: tableName + "_error_logs", // Second table for Error logs
                    needAutoCreateTable: true,
                    columnOptions: errorColumnWriters))
            .WriteTo.Logger(lc => lc
                .Filter.ByIncludingOnly(logEvent => logEvent.Level == LogEventLevel.Debug) // Debug logs only
                .WriteTo.PostgreSQL(
                    connectionString: conString,
                    tableName: tableName + "_debug_logs", // Thrid table for Debug logs
                    needAutoCreateTable: true,
                    columnOptions: errorColumnWriters))
            .WriteTo.Logger(lc => lc
                .Filter.ByIncludingOnly(logEvent => logEvent.Level == LogEventLevel.Warning) // Warning logs only
                .WriteTo.PostgreSQL(
                    connectionString: conString,
                    tableName: tableName + "_warning_logs", // Thrid table for Warning logs
                    needAutoCreateTable: true,
                    columnOptions: errorColumnWriters))
            .CreateLogger();

        hostBuilder.UseSerilog();
    }
}