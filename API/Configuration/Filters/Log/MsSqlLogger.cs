using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Sinks.MSSqlServer;

namespace API.Configuration.Filters.Log
{
    public class MsSqlLogger
    {
        //Logging process to MsSql
        public ILogger LoggerManager;
        public MsSqlLogger(IConfiguration configuration)
        {
            var sinkOpt = new MSSqlServerSinkOptions()
            {
                TableName = "Logs",
                AutoCreateSqlTable = true
            };

            var columnOpts = new ColumnOptions();
            columnOpts.Store.Remove(StandardColumn.Message);
            columnOpts.Store.Remove(StandardColumn.Properties);

            var seriLogConf = new LoggerConfiguration().WriteTo
                .MSSqlServer(
                    connectionString: configuration.GetConnectionString("MsComm"),
                    sinkOptions: sinkOpt,
                    columnOptions: columnOpts); 

            LoggerManager = seriLogConf.CreateLogger();
        }
    }
}
