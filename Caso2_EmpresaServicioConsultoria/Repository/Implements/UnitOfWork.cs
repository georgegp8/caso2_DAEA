using System.Collections;
using Caso2_EmpresaServicioConsultoria.Models;

namespace Caso2_EmpresaServicioConsultoria.Repository.Implements;

public class UnitOfWork : IUnitOfWork
{
    private Hashtable? _repository;
    private readonly ConsultoriaDBContext _dbContext;

    public UnitOfWork(ConsultoriaDBContext context)
    {
        _dbContext = context;
        _repository = new Hashtable();
    }

    public IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : class
    {
        var Type = typeof(TEntity).Name;

        if (_repository.ContainsKey(Type))
        {
            return (IGenericRepository<TEntity>)_repository[Type];
        }
        var repositoryType = typeof(GenericRepository<>);
        var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), _dbContext);
        
        if (repositoryInstance != null)
        {
            _repository.Add(Type, repositoryInstance);
            return (IGenericRepository<TEntity>)repositoryInstance;
        }
        throw new Exception($"Couldn't create repository of type {Type}");
    }

    public Task<int> Complete()
    {
        return _dbContext.SaveChangesAsync();
    }
    
    public void Dispose()
    {
        _dbContext.Dispose();
    }
}