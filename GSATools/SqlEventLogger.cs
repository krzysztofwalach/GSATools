using System;
using System.Data.SqlClient;
using GSATools.VersioningUtil.Models;

namespace GSATools.VersioningUtil
{
    public interface IEventLogger
    {
        void LogConfigUpdate(ConfigUpdateLogEntry logEntry);
    }

    public class SqlEventLogger : IEventLogger
    {
        private readonly string _connectionString;
        public SqlEventLogger(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void LogConfigUpdate(ConfigUpdateLogEntry logEntry)
        {
            const string commandSql = @"
    INSERT INTO [dbo].[gsa_update_event]
           ([time]
           ,[gsa_host_info]
           ,[update_key]
           ,[git_branch_name]
           ,[git_revision]
           ,[exception_message]
           ,[exception_stack_trace])
     VALUES
           (@time
           ,@gsa_host_info
           ,@update_key
           ,@git_branch_name
           ,@git_revision
           ,@exception_message
           ,@exception_stack_trace)";


            var sqlCommand = new SqlCommand(commandSql);
            sqlCommand.Parameters.AddWithValue("@time", logEntry.Time);
            sqlCommand.Parameters.AddWithValue("@gsa_host_info", logEntry.GSAHostInfo);
            sqlCommand.Parameters.AddWithValue("@update_key", logEntry.GSAUpdateKey);
            sqlCommand.Parameters.AddWithValue("@git_branch_name", logEntry.GitBranchName);
            sqlCommand.Parameters.AddWithValue("@git_revision", logEntry.GitRevision);
            sqlCommand.Parameters.AddWithValue("@exception_message", (logEntry.ExceptionMessage != null) ? (object) logEntry.ExceptionMessage : DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@exception_stack_trace", (logEntry.ExceptionStackTrace != null) ? (object)logEntry.ExceptionStackTrace : DBNull.Value);

            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                sqlCommand.Connection = sqlConnection;
                sqlConnection.Open();
                sqlCommand.ExecuteNonQuery();
            }
        }
    }
}
