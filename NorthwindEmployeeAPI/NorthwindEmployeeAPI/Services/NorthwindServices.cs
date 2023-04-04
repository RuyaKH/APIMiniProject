using Microsoft.EntityFrameworkCore;
using NorthwindEmployeeAPI.Controllers;
using NorthwindEmployeeAPI.Data.Repositories;
using NorthwindEmployeeAPI.Models;
using NorthwindEmployeeAPI.Services;

namespace NorthwindAPI.Services
{
    public class NorthwindServices<T> : INorthwindService<T> where T : class
    {
        private readonly ILogger _logger;
        private readonly INorthwindRepository<T> _repository;

        public NorthwindServices(ILogger<INorthwindService<T>> logger, INorthwindRepository<T> repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task<bool> CreateAsync(T entity)
        {
            if (_repository.IsNull)
            {
                return false;
            }
            _repository.Add(entity);
            await _repository.SaveAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            if (_repository.IsNull)
            {
                return false;
            }
            var entity = await _repository.FindAsync(id);
            if (entity == null)
            {
                return false;
            }

            _repository.Remove(entity);

            await _repository.SaveAsync();

            return true;
        }

        public async Task<IEnumerable<T>?> GetAllAsync()
        {
            if (_repository.IsNull)
            {
                return null;
            }
            return await _repository.GetAllAsync();

        }

        public async Task<T?> GetAsync(int id)
        {
            if (_repository.IsNull)
            {
                return null;
            }
            var entity = await _repository.FindAsync(id);

            if (entity == null)
            {
                _logger.LogWarning($"{typeof(T).Name} with id: {id} not found");
                return null;
            }
            _logger.LogInformation($"{typeof(T).Name} with id: {id}  found");
            return entity;
        }

        public async Task<T?> GetAsync(string id)
        {
            if (_repository.IsNull)
            {
                return null;
            }
            var entity = await _repository.FindAsync(id);

            if (entity == null)
            {
                _logger.LogWarning($"{typeof(T).Name} with id: {id} not found");
                return null;
            }
            _logger.LogInformation($"{typeof(T).Name} with id: {id}  found");
            return entity;
        }

        public virtual async Task<bool> UpdateAsync(int id, T entity)
        {
            _repository.Update(entity);

            try
            {
                await _repository.SaveAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await EntityExists(id))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }

            return true;
        }

        private async Task<bool> EntityExists(int id)
        {
            return (await _repository.FindAsync(id)) != null;
        }

        public async Task SaveAsync()
        {
            await _repository.SaveAsync();
        }
    }
}
