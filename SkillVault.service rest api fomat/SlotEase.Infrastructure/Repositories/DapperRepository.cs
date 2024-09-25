using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using SlotEase.Infrastructure.Interfaces;
using SlotEase.Domain;

namespace SlotEase.Infrastructure.Repositories;

public class DapperRepository : IDapperRepository
{
    private readonly ConfigSettings _configuration;
    private readonly SlotEaseContext _context;

    public DapperRepository(IOptions<ConfigSettings> configuration, SlotEaseContext context)
    {
        _configuration = configuration.Value ?? throw new ArgumentNullException(nameof(configuration));
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    /// <summary>
    /// Execute SQL stored procedure and map to IEnumerable<T>
    /// </summary>
    /// <typeparam name="T">Type of items in response list</typeparam>
    /// <param name="storedProcedureName">Name of stored procedure</param>
    /// <param name="parameters">Parameters for stored procedure</param>
    /// <returns>returns IEnumerable<T></returns>
    public IEnumerable<T> ExecuteStoredProcedure<T>(string storedProcedureName, Dictionary<string, object> parameters = null)
    {
        if (string.IsNullOrWhiteSpace(storedProcedureName))
            throw new ArgumentNullException(nameof(storedProcedureName));

        IEnumerable<T> result;
        DynamicParameters dynamicParameters = (parameters != null && parameters.Count > 0) ? new DynamicParameters(parameters) : null;
        DbConnection conn = _context.Database.GetDbConnection();

        if (_context.Database.CurrentTransaction != null)
        {
            IDbTransaction tran = (_context.Database.CurrentTransaction as IInfrastructure<DbTransaction>).Instance;
            result = conn.Query<T>(storedProcedureName, dynamicParameters, transaction: tran, commandType: CommandType.StoredProcedure);
        }
        else
        {
            result = conn.Query<T>(storedProcedureName, dynamicParameters, commandType: CommandType.StoredProcedure);
        }

        return result;
    }
    /// <summary>
    /// Execute SQL query and map to IEnumerable<T>
    /// </summary>
    /// <typeparam name="T">Type of items in response list</typeparam>
    /// <param name="query">SQL query</param>
    /// <param name="parameters">Parameters to use in query. User parameters to avoid SQL injection</param>
    /// <returns>IEnumerable<T></returns>
    public IEnumerable<T> ExecuteQuery<T>(string query, Dictionary<string, object> parameters = null)
    {
        if (string.IsNullOrWhiteSpace(query))
            throw new ArgumentNullException(nameof(query));

        IEnumerable<T> result;
        DynamicParameters dynamicParameters = (parameters != null && parameters.Count > 0) ? new DynamicParameters(parameters) : null;
        DbConnection conn = _context.Database.GetDbConnection();

        if (_context.Database.CurrentTransaction != null)
        {
            IDbTransaction tran = (_context.Database.CurrentTransaction as IInfrastructure<DbTransaction>).Instance;
            result = conn.Query<T>(query, dynamicParameters, transaction: tran);
        }
        else
        {
            result = conn.Query<T>(query, dynamicParameters);
        }

        return result;
    }
}
