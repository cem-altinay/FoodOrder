using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoodOrder.Application.Interfaces.Repositories;
using FoodOrder.Domain.Common;
using FoodOrder.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace FoodOrder.Persistence.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly FoodOrderDbContext _context;
        private DbSet<T> _entities;
        public GenericRepository(FoodOrderDbContext context)
        {
            _context = context;
            _entities = context.Set<T>();
        }
        protected virtual DbSet<T> Entities => _entities ?? (_entities = _context.Set<T>());

        public IQueryable<T> Table => Entities;

        public IQueryable<T> TableNoTracking => Entities.AsNoTracking();

        public void Delete(T entity)
        {
            if (entity is null)
                throw new ArgumentNullException(nameof(entity));

            Entities.Remove(entity);
            _context.SaveChanges();
        }

        public async Task DeleteAsync(T entity)
        {
            if (entity is null)
                throw new ArgumentNullException(nameof(entity));

            Entities.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public void Delete(IEnumerable<T> entities)
        {
            if (entities is null)
                throw new ArgumentNullException(nameof(entities));


            Entities.RemoveRange(entities);
            _context.SaveChanges();
        }

        public async Task DeleteAsync(IEnumerable<T> entities)
        {
            if (entities is null)
                throw new ArgumentNullException(nameof(entities));


            Entities.RemoveRange(entities);
            await _context.SaveChangesAsync();
        }

        public T GetById(object id)
        {
            T entity = Entities.Find(id);

            return entity;
        }

        public async Task<T> GetByIdAsync(object id)
        {
            T entity = await Entities.FindAsync(id);

            return entity;
        }

        public void Insert(T entity)
        {
            if (entity is null)
                throw new ArgumentNullException(nameof(entity));

            entity.CreateDate = DateTime.UtcNow;

            Entities.Add(entity);
            _context.SaveChanges();
        }


        public async Task InsertAsync(T entity)
        {
            if (entity is null)
                throw new ArgumentNullException(nameof(entity));

            entity.CreateDate = DateTime.UtcNow;

            Entities.Add(entity);
            await _context.SaveChangesAsync();
        }

        public void UpdateMatchEntity(T updateEntity, T setEntity)
        {
            //updateEntity: Varolan hali, setEntity: Güncellenmiş hali
            if (setEntity == null)
                throw new ArgumentNullException(nameof(setEntity));

            if (updateEntity == null)
                throw new ArgumentNullException(nameof(updateEntity));


            _context.Entry(updateEntity).CurrentValues.SetValues(setEntity);//Tüm kayıtlar, kolon eşitlemesine gitmeden bir entity'den diğerine atanır.

            foreach (var property in _context.Entry(setEntity).Properties)
            {
                if (property.CurrentValue == null) { _context.Entry(updateEntity).Property(property.Metadata.Name).IsModified = false; }
            }

            _context.SaveChanges();
        }

        public async Task UpdateMatchEntityAsync(T updateEntity, T setEntity)
        {
            //updateEntity: Varolan hali, setEntity: Güncellenmiş hali
            if (setEntity == null)
                throw new ArgumentNullException(nameof(setEntity));

            if (updateEntity == null)
                throw new ArgumentNullException(nameof(updateEntity));


            _context.Entry(updateEntity).CurrentValues.SetValues(setEntity);//Tüm kayıtlar, kolon eşitlemesine gitmeden bir entity'den diğerine atanır.

            foreach (var property in _context.Entry(setEntity).Properties)
            {
                if (property.CurrentValue == null) { _context.Entry(updateEntity).Property(property.Metadata.Name).IsModified = false; }
            }

            await _context.SaveChangesAsync();
        }
    }
}