using FP.Core.Entities.Common;
using FP.Core.Repositories;
using FP.DAL.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FP.DAL.Repositories;

public class GenericRepository<T>(FPDbContext _context) : IGenericRepository<T> where T : BaseEntity, new()
{
    protected DbSet<T> Table = _context.Set<T>();
    public async Task AddAsync(T entity)
    {
        await Table.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(T entity)
    {
        Table.Remove(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await Table.ToListAsync();
    }

    public async Task<T> GetByIdAsync(int id)
    {
        T? entity = await Table.FindAsync(id);
        if (entity == null)
            return null;
        return entity;
    }

    public async Task<IEnumerable<T>> GetWhereAsync(Expression<Func<T, bool>> expression)
    {
        return await Table.Where(expression).ToListAsync();
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }
}
