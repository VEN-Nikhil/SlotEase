using EFCore.BulkExtensions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using SlotEase.Domain.Interfaces;
using SlotEase.Domain.Models;

namespace SlotEase.Infrastructure.Repositories;

public class BaseRepository<TEntity> : BaseRepository<TEntity, int>, IRepository<TEntity> where TEntity : class, IEntity<int>
{
    public BaseRepository(SlotEaseContext context, IUserService userService) : base(context, userService) { }
}
public class BaseRepository<TEntity, TPrimaryKey> : IRepository<TEntity, TPrimaryKey> where TEntity : class, IEntity<TPrimaryKey>
{

    private readonly SlotEaseContext _context;
    private readonly DbSet<TEntity> dbSet;
    private readonly IUserService _userService;


    public BaseRepository(SlotEaseContext context, IUserService userService)
    {

        _context = context;
        dbSet = context.Set<TEntity>();
        _userService = userService;
    }


    public virtual IEnumerable<TEntity> Get(
        Expression<Func<TEntity, bool>> filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        string includeProperties = "",
        bool noTracking = true)
    {

        IQueryable<TEntity> query = dbSet;
        if (filter != null)
        {
            query = query?.Where(filter);
        }
        if (noTracking)
            query = query.AsNoTracking();
        if (includeProperties != null)
        {
            foreach (string includeProperty in includeProperties.Split
            (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }
        }
        if (orderBy != null)
        {
            return orderBy(query)?.ToList();
        }
        else
        {
            return query?.ToList();
        }

    }

    public virtual TEntity Get(TPrimaryKey id, bool noTracking = true)
    {
        TEntity entity = FirstOrDefault(id, noTracking);

        return entity;
    }



    public virtual TEntity GetByID(object id)
    {
        return dbSet.Find(id);
    }


    public virtual TEntity Insert(TEntity entity)
    {
        AddCreationAudit(entity);
        Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<TEntity> entityAdded = dbSet.Add(entity);
        return entityAdded.Entity;
    }

    public virtual Task DeleteAsync(TEntity entity)
    {
        Delete(entity);
        return Task.CompletedTask;
    }

    public virtual Task DeleteAsync(TPrimaryKey id)
    {
        Delete(id);
        return Task.CompletedTask;
    }

    public virtual async Task DeleteAsync(Expression<Func<TEntity, bool>> predicate)
    {
        List<TEntity> entities = await GetAllListAsync(predicate);

        foreach (TEntity entity in entities)
        {
            await DeleteAsync(entity);
        }
    }

    public virtual async Task SoftDeleteAsync(Expression<Func<TEntity, bool>> predicate)
    {
        List<TEntity> entities = await GetAllListAsync(predicate);

        foreach (TEntity entity in entities)
        {
            await SoftDeleteAsync(entity);
        }
    }

    public virtual void Delete(Expression<Func<TEntity, bool>> predicate)
    {
        foreach (TEntity entity in GetAllList(predicate))
        {
            Delete(entity);
        }
    }

    public virtual void Delete(object id)
    {
        TEntity entityToDelete = dbSet.Find(id);
        Delete(entityToDelete);
    }


    public virtual void Delete(TEntity entity)
    {
        if (_context.Entry(entity).State == EntityState.Detached)
        {
            dbSet.Attach(entity);
        }
        dbSet.Remove(entity);
    }

    public void Delete(TPrimaryKey id)
    {
        TEntity entity = dbSet.Local.FirstOrDefault(ent => EqualityComparer<TPrimaryKey>.Default.Equals(ent.Id, id));
        if (entity == null)
        {
            entity = FirstOrDefault(id);
            if (entity == null)
            {
                return;
            }
        }

        Delete(entity);
    }


    public virtual TEntity Single(Expression<Func<TEntity, bool>> predicate, bool noTracking = true)
    {
        return GetAll(noTracking).Single(predicate);
    }

    public virtual Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> predicate, bool noTracking = true)
    {
        return Task.FromResult(Single(predicate, noTracking));
    }



    public virtual Task<TEntity> InsertAsync(TEntity entity)
    {
        return Task.FromResult(Insert(entity));
    }

    public virtual TPrimaryKey InsertAndGetId(TEntity entity)
    {
        TEntity insertedEntity = Insert(entity);
        _context.SaveChanges();
        return insertedEntity.Id;
    }

    public virtual async Task<TPrimaryKey> InsertAndGetIdAsync(TEntity entity)
    {
        TEntity insertedEntity = await InsertAsync(entity);
        _context.SaveChanges();
        return insertedEntity.Id;
    }

    public virtual TEntity InsertOrUpdate(TEntity entity)
    {
        return entity.IsTransient()
            ? Insert(entity)
            : Update(entity);
    }

    public virtual async Task<TEntity> InsertOrUpdateAsync(TEntity entity)
    {
        return entity.IsTransient()
            ? await InsertAsync(entity)
            : await UpdateAsync(entity);
    }

    public virtual TPrimaryKey InsertOrUpdateAndGetId(TEntity entity)
    {
        TEntity insertedEntity = InsertOrUpdate(entity);
        _context.SaveChanges();
        return insertedEntity.Id;
    }

    public virtual async Task<TPrimaryKey> InsertOrUpdateAndGetIdAsync(TEntity entity)
    {
        TEntity insertedEntity = await InsertOrUpdateAsync(entity);
        _context.SaveChanges();
        return insertedEntity.Id;
    }

    public virtual TEntity Update(TPrimaryKey id, Action<TEntity> updateAction)
    {
        TEntity entity = Get(id);
        AddModificationAudit(entity);
        updateAction(entity);
        return entity;
    }

    public virtual TEntity Update(TEntity entity)
    {
        AddModificationAudit(entity);
        dbSet.Attach(entity);
        _context.Entry(entity).State = EntityState.Modified;
        return entity;
    }
    public virtual TEntity EntityStateAdded(TEntity entity)
    {
        AddModificationAudit(entity);
        dbSet.Attach(entity);
        _context.Entry(entity).State = EntityState.Added;
        return entity;
    }

    private void AddModificationAudit(TEntity entity)
    {
        if (entity is IModificationAudited modificationAudited)
        {
            modificationAudited.LastModifierUserId = _userService.GetUserId();
            modificationAudited.LastModificationTime = DateTime.UtcNow;
        }
    }

    private void AddCreationAudit(TEntity entity)
    {
        if (entity is ICreationAudited creationAudited)
        {
            creationAudited.CreatorUserId = _userService.GetUserId();
            creationAudited.CreationTime = DateTime.UtcNow;
        }
    }

    private void AddDeletionAudit(TEntity entity)
    {
        if (entity is IDeletionAudited deletionAudited)
        {
            deletionAudited.DeleterUserId = _userService.GetUserId();
            deletionAudited.IsDeleted = true;
            deletionAudited.DeletionTime = DateTime.UtcNow;
        }
        else
        {
            throw new InvalidCastException($"The entity - {entity.GetType().Name} can't be soft deleted");
        }

    }

    public virtual void SoftDelete(TEntity entity)
    {
        AddDeletionAudit(entity);
        Update(entity);
    }

    public virtual void SoftDelete(TPrimaryKey id)
    {
        TEntity entity = dbSet.Local.FirstOrDefault(ent => EqualityComparer<TPrimaryKey>.Default.Equals(ent.Id, id));
        if (entity == null)
        {
            entity = FirstOrDefault(id);
            if (entity == null)
            {
                return;
            }
        }
        SoftDelete(entity);
    }

    public virtual Task SoftDeleteAsync(TPrimaryKey id)
    {
        SoftDelete(id);
        return Task.CompletedTask;
    }

    public virtual Task SoftDeleteAsync(TEntity entity)
    {
        SoftDelete(entity);
        return Task.CompletedTask;
    }


    public virtual DbSet<TEntity> GetTable()
    {
        SlotEaseContext context = _context;
        return context.Set<TEntity>();
    }

    protected virtual void AttachIfNot(TEntity entity)
    {
        Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry entry = _context.ChangeTracker.Entries().FirstOrDefault(ent => ent.Entity == entity);
        if (entry != null)
        {
            return;
        }

        GetTable().Attach(entity);
    }

    public virtual int Count(Expression<Func<TEntity, bool>> predicate, bool noTracking = true)
    {
        return GetAll(noTracking).Count(predicate);
    }

    public virtual int Count(bool noTracking = true)
    {
        return GetAll(noTracking).Count();
    }

    public virtual Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate, bool noTracking = true)
    {
        return Task.FromResult(Count(predicate, noTracking));
    }

    public virtual Task<int> CountAsync(bool noTracking = true)
    {
        return Task.FromResult(Count(noTracking));
    }


    public virtual long LongCount(bool noTracking = true)
    {
        return GetAll(noTracking).LongCount();
    }

    public virtual long LongCount(Expression<Func<TEntity, bool>> predicate, bool noTracking = true)
    {
        return GetAll(noTracking).LongCount(predicate);
    }

    public virtual Task<long> LongCountAsync(Expression<Func<TEntity, bool>> predicate, bool noTracking = true)
    {
        return Task.FromResult(LongCount(predicate, noTracking));
    }

    public virtual Task<long> LongCountAsync(bool noTracking = true)
    {
        return Task.FromResult(LongCount(noTracking));
    }

    protected virtual Expression<Func<TEntity, bool>> CreateEqualityExpressionForId(TPrimaryKey id)
    {
        ParameterExpression lambdaParam = Expression.Parameter(typeof(TEntity));

        MemberExpression leftExpression = Expression.PropertyOrField(lambdaParam, "Id");

        object idValue = Convert.ChangeType(id, typeof(TPrimaryKey));

        Expression<Func<object>> closure = () => idValue;
        UnaryExpression rightExpression = Expression.Convert(closure.Body, leftExpression.Type);

        BinaryExpression lambdaBody = Expression.Equal(leftExpression, rightExpression);

        return Expression.Lambda<Func<TEntity, bool>>(lambdaBody, lambdaParam);
    }

    public IQueryable<TEntity> GetAllIncluding(bool noTracking = true, params Expression<Func<TEntity, object>>[] propertySelectors)
    {
        IQueryable<TEntity> query = GetAll(noTracking);

        foreach (Expression<Func<TEntity, object>> propertySelector in propertySelectors)
        {
            query = query.Include(propertySelector);
        }
        return query;
    }

    public virtual List<TEntity> GetAllList(Expression<Func<TEntity, bool>> predicate, bool noTracking = true)
    {
        return GetAll(noTracking).Where(predicate).ToList();
    }

    public virtual IQueryable<TEntity> GetAll(bool noTracking = true)
    {
        IQueryable<TEntity> query = dbSet;
        return noTracking ? query.AsNoTracking() : query;
    }

    public virtual Task<List<TEntity>> GetAllListAsync(Expression<Func<TEntity, bool>> predicate, bool noTracking = true)
    {
        return Task.FromResult(GetAllList(predicate, noTracking));
    }

    public T Query<T>(Func<IQueryable<TEntity>, T> queryMethod, bool noTracking = true)
    {
        return queryMethod(GetAll(noTracking));
    }

    public Task<TEntity> GetAsync(TPrimaryKey id, bool noTracking = true)
    {
        return Task.FromResult(Get(id, noTracking));
    }

    public virtual TEntity FirstOrDefault(TPrimaryKey id, bool noTracking = true)
    {
        return GetAll(noTracking).FirstOrDefault(CreateEqualityExpressionForId(id));
    }

    public TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate, bool noTracking = true)
    {
        return GetAll(noTracking).FirstOrDefault(predicate);
    }

    public Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, bool noTracking = true)
    {
        return Task.FromResult(FirstOrDefault(predicate, noTracking));
    }

    public Task<TEntity> FirstOrDefaultAsync(TPrimaryKey id, bool noTracking = true)
    {
        return Task.FromResult(FirstOrDefault(id, noTracking));
    }

    public TEntity Load(TPrimaryKey id, bool noTracking = true)
    {
        return Get(id, noTracking);
    }

    public virtual Task<TEntity> UpdateAsync(TEntity entity)
    {
        return Task.FromResult(Update(entity));
    }

    public virtual async Task<TEntity> UpdateAsync(TPrimaryKey id, Func<TEntity, Task> updateAction, bool noTracking = true)
    {
        TEntity entity = await GetAsync(id, noTracking);
        await updateAction(entity);
        return entity;
    }

    public virtual DbContext GetDbContext()
    {
        return _context;
    }

    public virtual void BulkSoftDelete(List<TEntity> entities)
    {
        foreach (TEntity entity in entities)
        {
            AddDeletionAudit(entity);
        }

        _context.BulkUpdate(entities);
    }

    public virtual void BulkInsert(List<TEntity> entities)
    {
        foreach (TEntity entity in entities)
        {
            AddCreationAudit(entity);
        }

        _context.BulkInsert(entities);
    }

    public virtual void BulkUpdate(List<TEntity> entities)
    {
        foreach (TEntity entity in entities)
        {
            AddModificationAudit(entity);
        }

        _context.BulkUpdate(entities);
    }

    public virtual bool ExecuteSp(string storedProcedureName, Dictionary<string, object> parameters)
    {

        string sql = "";
        List<SqlParameter> sqlparameer = new();

        foreach (KeyValuePair<string, object> param in parameters)
        {
            if (param.Value.GetType() != typeof(TableDbParameter))
            {
                sqlparameer.Add(new SqlParameter(param.Key, param.Value));
            }
            else
            {
                TableDbParameter dbParam = ((TableDbParameter)param.Value);
                sqlparameer.Add(new SqlParameter(param.Key, SqlDbType.Structured)
                {
                    TypeName = dbParam.Type,
                    Value = dbParam.Value
                });
            }

            sql = $"{sql}, @{param.Key}";
        }

        sql = $"{storedProcedureName} {sql.Substring(1)}";
        return _context.Database.ExecuteSqlRaw(sql, sqlparameer) > 0;

    }
}
