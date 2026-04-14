using Caso2_EmpresaServicioConsultoria.Models;
using Microsoft.EntityFrameworkCore;

namespace Caso2_EmpresaServicioConsultoria.Repository.Implements;

public class GenericRepository<T>(ConsultoriaDBContext dbConsultorio) : IGenericRepository<T> where T : class
{
    protected readonly ConsultoriaDBContext _dbConsultorio = dbConsultorio;

    public async Task<IEnumerable<T>> GetAll() => 
        await _dbConsultorio.Set<T>().ToListAsync();

    public async Task<T?> GetById(Guid id) => 
        await _dbConsultorio.Set<T>().FindAsync(id);

    public async Task Add(T entity) => 
        await _dbConsultorio.Set<T>().AddAsync(entity);

    public async Task Update(T entity) => 
        _dbConsultorio.Set<T>().Update(entity);

    public async Task Delete(Guid id)
    {
        var entity = await GetById(id);
        if (entity != null) _dbConsultorio.Set<T>().Remove(entity);
    }
}
