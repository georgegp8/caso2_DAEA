namespace Caso2_EmpresaServicioConsultoria.Repository;

public interface IUnitOfWork : IDisposable
{
    IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : class;
    Task<int> Complete();
}