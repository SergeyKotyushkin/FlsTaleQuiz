using System;
using System.Data.SqlClient;

namespace FlsTaleQuiz.Business.Interfaces
{
    public interface IDataBaseService
    {
        bool TryMapReadLines(string storeProcedureName, SqlParameter[] sqlParameters, Action<SqlDataReader> readerAction);
    }
}