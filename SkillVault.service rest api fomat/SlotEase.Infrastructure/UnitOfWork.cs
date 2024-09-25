using SlotEase.Infrastructure.Interfaces;

namespace SlotEase.Infrastructure;

public class UnitOfWork : IUnitOfWork
{
    private readonly SlotEaseContext _dbContext;

    public UnitOfWork(SlotEaseContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public void BeginTransaction()
    {
        _dbContext.Database.BeginTransaction();
    }

    public void CommitTransaction()
    {
        _dbContext.Database.CommitTransaction();
    }

    public void RollbackTransaction()
    {
        _dbContext.Database.RollbackTransaction();
    }

    public int SaveChanges()
    {
        int result = _dbContext.SaveChanges();
        return result;
    }
}
