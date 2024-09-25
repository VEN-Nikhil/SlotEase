
namespace SlotEase.Infrastructure.Interfaces;

public interface IUnitOfWork
{
    void BeginTransaction();
    void CommitTransaction();
    void RollbackTransaction();
    int SaveChanges();
}