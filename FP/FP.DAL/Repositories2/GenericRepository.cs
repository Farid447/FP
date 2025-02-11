using FP.Core.Entities.Common;
using FP.Core.Repositories;
using FP.DAL.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FP.DAL.Repositories
{
    public class GenericRepository<T>(FPDbContext _context) : IGenericRepository<T> where T : BaseEntity, new()
    {
        protected DbSet<T> Table = _context.Set<T>();
        public async Task AddAsync(T entity)
        {
            await Table.AddAsync(entity);
        }

        public void Delete(T entity)
        {
            Table.Remove(entity);
        }

        public async Task DeleteAsync(int id)
        {
            await Table.Where(x=> x.Id == id).ExecuteDeleteAsync();
            //T? entity = await GetByIdAsync(id);
            //Delete(entity!);
        }

        public IQueryable<T> GetAll()
            => Table.AsQueryable();

        public async Task<T?> GetByIdAsync(int id)
            => await Table.FindAsync(id);

        public IQueryable<T> GetWhere(Func<T, bool> expression)
            => Table.Where(expression).AsQueryable();

        public async Task<bool> IsExistAsync(int id)
            => await Table.AnyAsync(t => t.Id == id);

        public async Task<int> SaveAsync()
            => await _context.SaveChangesAsync();
    }
}
