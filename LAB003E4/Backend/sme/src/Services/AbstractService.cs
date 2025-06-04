using Microsoft.EntityFrameworkCore;
using sme.src.Auth;
using sme.src.Data;
using sme.src.Middlewares.Exceptions;
using sme.src.Public.DTOs;

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

        public async Task<T> UpdateAsync(T entity, int id)
        {
            if (entity == null) throw new CustomArgumentNullException(nameof(entity), "Entity cannot be null.");

            var existingEntity = await _context.Set<T>().FindAsync(id) 
                ?? throw new NotFoundException($"Entity with id {id} not found.");

            var entry = _context.Entry(existingEntity);
            var newValues = _context.Entry(entity).CurrentValues;

            foreach (var property in entry.Metadata.GetProperties().Where(p => !p.IsPrimaryKey()))
            {
                var newValue = newValues[property.Name];
                if (newValue != null)
                    entry.Property(property.Name).CurrentValue = newValue;
            }

            await _context.SaveChangesAsync();
            return existingEntity;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id) ?? throw new NotFoundException($"Entity with id {id} not found.");
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}