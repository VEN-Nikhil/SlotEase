using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SlotEase.Domain.Interfaces;

public interface IRepository<TEntity> : IRepository<TEntity, int> where TEntity : class, IEntity<int>
{

}

public interface IRepository<TEntity, TPrimaryKey> where TEntity : class, IEntity<TPrimaryKey>
{


    IQueryable<TEntity> GetAll(bool noTracking = true);

    #region Select/Get/Query



    /// <summary>
    /// Used to get a IQueryable that is used to retrieve entities from entire table.
    /// One or more 
    /// </summary>
    /// <param name="propertySelectors">A list of include expressions.</param>
    /// <returns>IQueryable to be used to select entities from database</returns>
    IQueryable<TEntity> GetAllIncluding(bool noTracking = true, params Expression<Func<TEntity, object>>[] propertySelectors);

    /// <summary>
    /// Used to get all entities based on given <paramref name="predicate"/>.
    /// </summary>
    /// <param name="predicate">A condition to filter entities</param>
    /// <returns>List of all entities</returns>
    List<TEntity> GetAllList(Expression<Func<TEntity, bool>> predicate, bool noTracking = true);

    /// <summary>
    /// Used to get all entities based on given <paramref name="predicate"/>.
    /// </summary>
    /// <param name="predicate">A condition to filter entities</param>
    /// <returns>List of all entities</returns>
    Task<List<TEntity>> GetAllListAsync(Expression<Func<TEntity, bool>> predicate, bool noTracking = true);

    /// <summary>
    /// Used to run a query over entire entities.
    /// <see cref="UnitOfWorkAttribute"/> attribute is not always necessary (as opposite to <see cref="GetAll"/>)
    /// if <paramref name="queryMethod"/> finishes IQueryable with ToList, FirstOrDefault etc..
    /// </summary>
    /// <typeparam name="T">Type of return value of this method</typeparam>
    /// <param name="queryMethod">This method is used to query over entities</param>
    /// <returns>Query result</returns>
    T Query<T>(Func<IQueryable<TEntity>, T> queryMethod, bool noTracking = true);

    /// <summary>
    /// Gets an entity with given primary key.
    /// </summary>
    /// <param name="id">Primary key of the entity to get</param>
    /// <returns>Entity</returns>
    TEntity Get(TPrimaryKey id, bool noTracking = true);

    /// <summary>
    /// Gets an entity with given primary key.
    /// </summary>
    /// <param name="id">Primary key of the entity to get</param>
    /// <returns>Entity</returns>
    Task<TEntity> GetAsync(TPrimaryKey id, bool noTracking = true);

    /// <summary>
    /// Gets exactly one entity with given predicate.
    /// Throws exception if no entity or more than one entity.
    /// </summary>
    /// <param name="predicate">Entity</param>
    TEntity Single(Expression<Func<TEntity, bool>> predicate, bool noTracking = true);

    /// <summary>
    /// Gets exactly one entity with given predicate.
    /// Throws exception if no entity or more than one entity.
    /// </summary>
    /// <param name="predicate">Entity</param>
    Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> predicate, bool noTracking = true);

    /// <summary>
    /// Gets an entity with given primary key or null if not found.
    /// </summary>
    /// <param name="id">Primary key of the entity to get</param>
    /// <returns>Entity or null</returns>
    TEntity FirstOrDefault(TPrimaryKey id, bool noTracking = true);

    /// <summary>
    /// Gets an entity with given given predicate or null if not found.
    /// </summary>
    /// <param name="predicate">Predicate to filter entities</param>
    TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate, bool noTracking = true);
    /// <summary>
    /// Gets an entity with given primary key or null if not found.
    /// </summary>
    /// <param name="id">Primary key of the entity to get</param>
    /// <returns>Entity or null</returns>
    Task<TEntity> FirstOrDefaultAsync(TPrimaryKey id, bool noTracking = true);

    /// <summary>
    /// Gets an entity with given given predicate or null if not found.
    /// </summary>
    /// <param name="predicate">Predicate to filter entities</param>
    Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, bool noTracking = true);

    /// <summary>
    /// Creates an entity with given primary key without database access.
    /// </summary>
    /// <param name="id">Primary key of the entity to load</param>
    /// <returns>Entity</returns>
    TEntity Load(TPrimaryKey id, bool noTracking = true);

    #endregion

    #region Insert

    /// <summary>
    /// Inserts a new entity.
    /// </summary>
    /// <param name="entity">Inserted entity</param>
    TEntity Insert(TEntity entity);

    /// <summary>
    /// Inserts a new entity.
    /// </summary>
    /// <param name="entity">Inserted entity</param>
    Task<TEntity> InsertAsync(TEntity entity);

    /// <summary>
    /// Inserts a new entity and gets it's Id.
    /// It may require to save current unit of work
    /// to be able to retrieve id.
    /// </summary>
    /// <param name="entity">Entity</param>
    /// <returns>Id of the entity</returns>
    TPrimaryKey InsertAndGetId(TEntity entity);

    /// <summary>
    /// Inserts a new entity and gets it's Id.
    /// It may require to save current unit of work
    /// to be able to retrieve id.
    /// </summary>
    /// <param name="entity">Entity</param>
    /// <returns>Id of the entity</returns>
    Task<TPrimaryKey> InsertAndGetIdAsync(TEntity entity);

    /// <summary>
    /// Inserts or updates given entity depending on Id's value.
    /// </summary>
    /// <param name="entity">Entity</param>
    TEntity InsertOrUpdate(TEntity entity);

    /// <summary>
    /// Inserts or updates given entity depending on Id's value.
    /// </summary>
    /// <param name="entity">Entity</param>
    Task<TEntity> InsertOrUpdateAsync(TEntity entity);

    /// <summary>
    /// Inserts or updates given entity depending on Id's value.
    /// Also returns Id of the entity.
    /// It may require to save current unit of work
    /// to be able to retrieve id.
    /// </summary>
    /// <param name="entity">Entity</param>
    /// <returns>Id of the entity</returns>
    TPrimaryKey InsertOrUpdateAndGetId(TEntity entity);

    /// <summary>
    /// Inserts or updates given entity depending on Id's value.
    /// Also returns Id of the entity.
    /// It may require to save current unit of work
    /// to be able to retrieve id.
    /// </summary>
    /// <param name="entity">Entity</param>
    /// <returns>Id of the entity</returns>
    Task<TPrimaryKey> InsertOrUpdateAndGetIdAsync(TEntity entity);

    /// <summary>
    /// Bulk insert the entities
    /// </summary>
    /// <param name="entities">Entitites to be inserted</param>
    void BulkInsert(List<TEntity> entities);

    #endregion

    #region Update

    /// <summary>
    /// Updates an existing entity.
    /// </summary>
    /// <param name="entity">Entity</param>
    TEntity Update(TEntity entity);



    /// <summary>
    /// Updates an existing entity.
    /// </summary>
    /// <param name="id">Id of the entity</param>
    /// <param name="updateAction">Action that can be used to change values of the entity</param>
    /// <returns>Updated entity</returns>
    TEntity Update(TPrimaryKey id, Action<TEntity> updateAction);

    /// <summary>
    /// Updates an existing entity. 
    /// </summary>
    /// <param name="entity">Entity</param>
    Task<TEntity> UpdateAsync(TEntity entity);

    /// <summary>
    /// Updates an existing entity.
    /// </summary>
    /// <param name="id">Id of the entity</param>
    /// <param name="updateAction">Action that can be used to change values of the entity</param>
    /// <returns>Updated entity</returns>
    Task<TEntity> UpdateAsync(TPrimaryKey id, Func<TEntity, Task> updateAction, bool noTracking = true);

    /// <summary>
    /// Bulk update the entities
    /// </summary>
    /// <param name="entities">Entitites to be updated</param>
    void BulkUpdate(List<TEntity> entities);

    #endregion

    #region Delete

    /// <summary>
    /// Deletes an entity.
    /// </summary>
    /// <param name="entity">Entity to be deleted</param>
    void Delete(TEntity entity);



    /// <summary>
    /// Deletes an entity by primary key.
    /// </summary>
    /// <param name="id">Primary key of the entity</param>
    void Delete(TPrimaryKey id);

    /// <summary>
    /// Deletes many entities by function.
    /// Notice that: All entities fits to given predicate are retrieved and deleted.
    /// This may cause major performance problems if there are too many entities with
    /// given predicate.
    /// </summary>
    /// <param name="predicate">A condition to filter entities</param>
    void Delete(Expression<Func<TEntity, bool>> predicate);


    /// <summary>
    /// Deletes an entity.
    /// </summary>
    /// <param name="entity">Entity to be deleted</param>
    Task DeleteAsync(TEntity entity);

    /// <summary>
    /// Deletes an entity by primary key.
    /// </summary>
    /// <param name="id">Primary key of the entity</param>
    Task DeleteAsync(TPrimaryKey id);



    /// <summary>
    /// Deletes many entities by function.
    /// Notice that: All entities fits to given predicate are retrieved and deleted.
    /// This may cause major performance problems if there are too many entities with
    /// given predicate.
    /// </summary>
    /// <param name="predicate">A condition to filter entities</param>
    Task DeleteAsync(Expression<Func<TEntity, bool>> predicate);

    /// <summary>
    /// Soft deletes the entities in the list
    /// </summary>
    /// <param name="entities">Entities to be removed</param>
    /// <returns></returns>
    void BulkSoftDelete(List<TEntity> entities);

    #endregion

    #region Stored Procedures

    /// <summary>
    /// Executes an sp and returns the status
    /// </summary>
    /// <param name="storedProcedureName">Name of the stored procedure</param>
    /// <param name="parameters">List of sql parameters</param>
    /// <returns></returns>
    bool ExecuteSp(string storedProcedureName, Dictionary<string, object> parameters);

    #endregion Stored Procedures

    #region Aggregates

    /// <summary>
    /// Gets count of all entities in this repository.
    /// </summary>
    /// <returns>Count of entities</returns>
    int Count(bool noTracking = true);



    /// <summary>
    /// Gets count of all entities in this repository based on given <paramref name="predicate"/>.
    /// </summary>
    /// <param name="predicate">A method to filter count</param>
    /// <returns>Count of entities</returns>
    int Count(Expression<Func<TEntity, bool>> predicate, bool noTracking = true);

    /// <summary>
    /// Gets count of all entities in this repository.
    /// </summary>
    /// <returns>Count of entities</returns>
    Task<int> CountAsync(bool noTracking = true);

    /// <summary>
    /// Gets count of all entities in this repository based on given <paramref name="predicate"/>.
    /// </summary>
    /// <param name="predicate">A method to filter count</param>
    /// <returns>Count of entities</returns>
    Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate, bool noTracking = true);



    /// <summary>
    /// Gets count of all entities in this repository based on given <paramref name="predicate"/>
    /// (use this overload if expected return value is greater than <see cref="int.MaxValue"/>).
    /// </summary>
    /// <param name="predicate">A method to filter count</param>
    /// <returns>Count of entities</returns>
    long LongCount(Expression<Func<TEntity, bool>> predicate, bool noTracking = true);

    /// <summary>
    /// Gets count of all entities in this repository (use if expected return value is greater than <see cref="int.MaxValue"/>.
    /// </summary>
    /// <returns>Count of entities</returns>
    long LongCount(bool noTracking = true);


    /// <summary>
    /// Gets count of all entities in this repository based on given <paramref name="predicate"/>
    /// (use this overload if expected return value is greater than <see cref="int.MaxValue"/>).
    /// </summary>
    /// <param name="predicate">A method to filter count</param>
    /// <returns>Count of entities</returns>
    Task<long> LongCountAsync(Expression<Func<TEntity, bool>> predicate, bool noTracking = true);



    /// <summary>
    /// Gets count of all entities in this repository (use if expected return value is greater than <see cref="int.MaxValue"/>.
    /// </summary>
    /// <returns>Count of entities</returns>
    Task<long> LongCountAsync(bool noTracking = true);



    /// <summary>
    /// Soft delete an entity, always use this to delete entities
    /// </summary>
    /// <param name="entity"></param>
    void SoftDelete(TEntity entity);

    /// <summary>
    /// Soft delete an entity using id, always use this to delete entities
    /// </summary>
    /// <param name="id"></param>
    void SoftDelete(TPrimaryKey id);

    /// <summary>
    /// Soft delete an entity using id, always use this to delete entities
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task SoftDeleteAsync(TPrimaryKey id);

    /// <summary>
    /// Soft delete an entity, always use this to delete entities
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    Task SoftDeleteAsync(TEntity entity);
    Task SoftDeleteAsync(Expression<Func<TEntity, bool>> predicate);
    DbContext GetDbContext();

    #endregion

    TEntity EntityStateAdded(TEntity entity);

}
