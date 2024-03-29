using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.Specification;
using DDDInvoicingCleanL.SharedKernel.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
namespace DDDCleanArchStarter.Infrastructure.Data
{
    public class CachedRepository<T> : IReadRepository<T> where T : class, IAggregateRoot
    {
        private readonly IMemoryCache _cache;
        private readonly ILogger<CachedRepository<T>> _logger;
        private readonly EfRepository<T> _sourceRepository;
        private readonly MemoryCacheEntryOptions _cacheOptions;
        public CachedRepository(IMemoryCache cache,
            ILogger<CachedRepository<T>> logger,
            EfRepository<T> sourceRepository)
        {
            _cache = cache;
            _logger = logger;
            _sourceRepository = sourceRepository;
            _cacheOptions = new MemoryCacheEntryOptions()
                .SetAbsoluteExpiration(TimeSpan.FromSeconds(10));
        }
        public Task<int> CountAsync(ISpecification<T> specification, CancellationToken cancellationToken = default)
        {
            return _sourceRepository.CountAsync(specification, cancellationToken);
        }
        public Task<int> CountAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
        public Task<T> GetByIdAsync<TId>(TId id, CancellationToken cancellationToken = default)
        {
            var key = $"{typeof(T).Name}-{id}";
            _logger.LogInformation("Checking cache for " + key);
            return _cache.GetOrCreate(key, entry =>
            {
                entry.SetOptions(_cacheOptions);
                _logger.LogWarning("Fetching source data for " + key);
                return _sourceRepository.GetByIdAsync(id, cancellationToken);
            });
        }
        public Task<TResult> FirstOrDefaultAsync<TResult>(ISpecification<T, TResult> specification,
            CancellationToken cancellationToken = default)
        {
            if (specification.CacheEnabled)
            {
                var key = $"{specification.CacheKey}-FirstOrDefaultAsync";
                _logger.LogInformation("Checking cache for " + key);
                return _cache.GetOrCreate(key, entry =>
                {
                    entry.SetOptions(_cacheOptions);
                    _logger.LogWarning("Fetching source data for " + key);
                    return _sourceRepository.FirstOrDefaultAsync(specification, cancellationToken);
                });
            }
            return _sourceRepository.FirstOrDefaultAsync(specification, cancellationToken);
        }
        public Task<TResult> GetBySpecAsync<TResult>(ISpecification<T, TResult> specification,
            CancellationToken cancellationToken = default)
        {
            if (specification.CacheEnabled)
            {
                var key = $"{specification.CacheKey}-GetBySpecAsync";
                _logger.LogInformation("Checking cache for " + key);
                return _cache.GetOrCreate(key, entry =>
                {
                    entry.SetOptions(_cacheOptions);
                    _logger.LogWarning("Fetching source data for " + key);
                    return _sourceRepository.FirstOrDefaultAsync(specification, cancellationToken);
                });
            }
            return _sourceRepository.FirstOrDefaultAsync(specification, cancellationToken);
        }
        public Task<List<T>> ListAsync(CancellationToken cancellationToken = default)
        {
            var key = $"{typeof(T).Name}-List";
            _logger.LogInformation($"Checking cache for {key}");
            return _cache.GetOrCreate(key, entry =>
            {
                entry.SetOptions(_cacheOptions);
                _logger.LogWarning($"Fetching source data for {key}");
                return _sourceRepository.ListAsync(cancellationToken);
            });
        }
        public Task<List<T>> ListAsync(ISpecification<T> specification,
            CancellationToken cancellationToken = default)
        {
            if (specification.CacheEnabled)
            {
                var key = $"{specification.CacheKey}-ListAsync";
                _logger.LogInformation($"Checking cache for {key}");
                return _cache.GetOrCreate(key, entry =>
                {
                    entry.SetOptions(_cacheOptions);
                    _logger.LogWarning($"Fetching source data for {key}");
                    return _sourceRepository.ListAsync(specification, cancellationToken);
                });
            }
            return _sourceRepository.ListAsync(specification, cancellationToken);
        }
        public Task<List<TResult>> ListAsync<TResult>(ISpecification<T, TResult> specification,
            CancellationToken cancellationToken = default)
        {
            if (specification.CacheEnabled)
            {
                var key = $"{specification.CacheKey}-ListAsync";
                _logger.LogInformation($"Checking cache for {key}");
                return _cache.GetOrCreate(key, entry =>
                {
                    entry.SetOptions(_cacheOptions);
                    _logger.LogWarning($"Fetching source data for {key}");
                    return _sourceRepository.ListAsync(specification, cancellationToken);
                });
            }
            return _sourceRepository.ListAsync(specification, cancellationToken);
        }
        public Task<T> GetBySpecAsync(ISpecification<T> specification, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
        public Task<bool> AnyAsync(ISpecification<T> specification, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
        public Task<bool> AnyAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
        public Task<T> FirstOrDefaultAsync(ISpecification<T> specification,
            CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
        public Task<T> SingleOrDefaultAsync(ISingleResultSpecification<T> specification,
            CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
        public Task<TResult?> SingleOrDefaultAsync<TResult>(ISingleResultSpecification<T, TResult> specification,
            CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
        public Task<T> AddAsync(T entity)
        {
            return _sourceRepository.AddAsync(entity);
        }
        public Task DeleteAsync(T entity)
        {
            return _sourceRepository.DeleteAsync(entity);
        }
        public Task DeleteRangeAsync(IEnumerable<T> entities)
        {
            return _sourceRepository.DeleteRangeAsync(entities);
        }
        public Task<T> FirstOrDefaultAsync<Spec>(Spec specification,
            CancellationToken cancellationToken = default) where Spec : ISingleResultSpecification, ISpecification<T>
        {
            if (specification.CacheEnabled)
            {
                var key = $"{specification.CacheKey}-FirstOrDefaultAsync";
                _logger.LogInformation("Checking cache for " + key);
                return _cache.GetOrCreate(key, entry =>
                {
                    entry.SetOptions(_cacheOptions);
                    _logger.LogWarning("Fetching source data for " + key);
                    return _sourceRepository.FirstOrDefaultAsync(specification, cancellationToken);
                });
            }
            return _sourceRepository.FirstOrDefaultAsync(specification);
        }
        public Task<T> GetBySpecAsync<Spec>(Spec specification,
            CancellationToken cancellationToken = default) where Spec : ISingleResultSpecification, ISpecification<T>
        {
            if (specification.CacheEnabled)
            {
                var key = $"{specification.CacheKey}-GetBySpecAsync";
                _logger.LogInformation("Checking cache for " + key);
                return _cache.GetOrCreate(key, entry =>
                {
                    entry.SetOptions(_cacheOptions);
                    _logger.LogWarning("Fetching source data for " + key);
                    return _sourceRepository.FirstOrDefaultAsync(specification, cancellationToken);
                });
            }
            return _sourceRepository.FirstOrDefaultAsync(specification);
        }
        public Task SaveChangesAsync()
        {
            return _sourceRepository.SaveChangesAsync();
        }
        public Task UpdateAsync(T entity)
        {
            return _sourceRepository.UpdateAsync(entity);
        }
    }
}