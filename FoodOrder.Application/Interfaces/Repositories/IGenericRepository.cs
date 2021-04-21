using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoodOrder.Domain.Common;

namespace FoodOrder.Application.Interfaces.Repositories
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        // Kaynak bora kaşmer anlatımlar
        IQueryable<T> Table { get; }
        IQueryable<T> TableNoTracking { get; }
        T GetById(object id);
        void Insert(T entity);
        void UpdateMatchEntity(T updateEntity, T setEntity);//isEncrypt şifreli propertyler var ise "true" atanır.
        void Delete(T entity);
        void Delete(IEnumerable<T> entities);
        Task DeleteAsync(T entity);
        Task DeleteAsync(IEnumerable<T> entities);
        Task<T> GetByIdAsync(object id);
        Task InsertAsync(T entity);
        Task UpdateMatchEntityAsync(T updateEntity, T setEntity);
    }
}