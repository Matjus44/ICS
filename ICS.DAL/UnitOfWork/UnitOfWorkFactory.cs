using Microsoft.EntityFrameworkCore;

namespace ICS.DAL.UnitOfWork;

public class UnitOfWorkFactory(IDbContextFactory<AppDbContext> dbContextFactory) : IUnitOfWorkFactory
{
     public IUnitOfWork Create() => new UnitOfWork(dbContextFactory.CreateDbContext());
}