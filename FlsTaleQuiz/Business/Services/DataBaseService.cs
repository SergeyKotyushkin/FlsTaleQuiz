using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using FlsTaleQuiz.Business.Interfaces;

namespace FlsTaleQuiz.Business.Services
{
    public class DataBaseService : IDataBaseService
    {
        private readonly string _sqlConnectionString;

        public DataBaseService()
        {
            _sqlConnectionString = ConfigurationManager.ConnectionStrings["DataBase"].ConnectionString;
        }

        public bool TryMapReadLines(string storeProcedureName, SqlParameter[] sqlParameters, Action<SqlDataReader> readerAction)
        {
            try
            {
                using (var connection = new SqlConnection(_sqlConnectionString))
                using (var command = new SqlCommand(storeProcedureName, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddRange(sqlParameters);

                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            readerAction(reader);
                        }
                    }
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Execute(string storeProcedureName, SqlParameter[] sqlParameters)
        {
            try
            {
                using (var connection = new SqlConnection(_sqlConnectionString))
                using (var command = new SqlCommand(storeProcedureName, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddRange(sqlParameters);

                    connection.Open();
                    var affectedCount = command.ExecuteNonQuery();

                    return affectedCount == 1;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}