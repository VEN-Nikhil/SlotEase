using System.Collections.Generic;

namespace SlotEase.Infrastructure.Interfaces;

public interface IDapperRepository
{
    /// <summary>
    /// Execute SQL stored procedure and map to IEnumerable<T>
    /// </summary>
    /// <typeparam name="T">Type of items in response list</typeparam>
    /// <param name="storedProcedureName">Name of stored procedure</param>
    /// <param name="parameters">Parameters for stored procedure</param>
    /// <returns>IEnumerable<T></returns>
    IEnumerable<T> ExecuteStoredProcedure<T>(string storedProcedureName, Dictionary<string, object> parameters = null);

    /// <summary>
    /// Execute SQL query and map to IEnumerable<T>
    /// </summary>
    /// <typeparam name="T">Type of items in response list</typeparam>
    /// <param name="query">SQL query</param>
    /// <param name="parameters">Parameters to use in query. User parameters to avoid SQL injection</param>
    /// <returns>IEnumerable<T></returns>
    IEnumerable<T> ExecuteQuery<T>(string query, Dictionary<string, object> parameters = null);

}
