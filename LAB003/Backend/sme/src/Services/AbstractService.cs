using Microsoft.EntityFrameworkCore;
using sme.src.Data;
using sme.src.Middlewares.Exceptions;

namespace sme.src.Services
{
    public class Service<T>(AppDbContext _context) where T : class
    {
        public async Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id) ?? throw new NotFoundException($"Entity with id {id} not found.");
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> AddAsync(T entity)
        {
            if (entity == null) throw new CustomArgumentNullException(nameof(entity), "Entity cannot be null.");
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task UpdateAsync(T entity)
        {
            if (entity == null) throw new CustomArgumentNullException(nameof(entity), "Entity cannot be null.");
            
            _context.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id) ?? throw new NotFoundException($"Entity with id {id} not found.");
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}