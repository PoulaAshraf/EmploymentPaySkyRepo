using EmploymentApi.Contracts;
using EmploymentApi.Models;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System;
using System.Linq.Expressions;

namespace EmploymentApi.Services
{
    public class Base<T> : IBase<T> where T : class
    {
        protected readonly ApplicationDbContext _context;
        public Base(ApplicationDbContext context)
        {
            _context = context;
        }


        public T SoftDelete(int iD, bool isActive, string columnName)
        {
            try
            {
                var queryResult = _context.Set<T>().Find(iD);

                if (queryResult != null)
                {

                    queryResult.GetType().GetProperty(columnName).SetValue(queryResult, isActive);

                    return null;
                }
                return null;

            }
            catch (Exception ex)
            {

                return null; throw;
            }

        }

        public T Find(Expression<Func<T, bool>> criteria, string[] includes = null)
        {
            try
            {
                IQueryable<T> query = _context.Set<T>();

                if (includes != null)
                    foreach (var incluse in includes)
                        query = query.Include(incluse);

                var returnObj = query.SingleOrDefault(criteria);
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<T> FindAll(Expression<Func<T, bool>> criteria, string[] includes = null)
        {
            try
            {
                IQueryable<T> query = _context.Set<T>();

                if (includes != null)
                    foreach (var include in includes)
                        query = query.Include(include);

                return query.Where(criteria).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<T> GetAll()
        {
            try
            {
                return _context.Set<T>().ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<T> GetAll(string[] includes = null)
        {
            try
            {
                IQueryable<T> query = _context.Set<T>();

                if (includes != null)
                    foreach (var incluse in includes)
                        query = query.Include(incluse);

                var returnObj = query.ToList();

                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public T GetById(Guid id)
        {
            try
            {
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public T GetById(int id)
        {
            try
            {
                return _context.Set<T>().Find(id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public T Insert(T entity)
        {
            try
            {
                _context.Set<T>().Add(entity);
                _context.SaveChanges();
                return entity;
            }
            catch (Exception ex)
            {
                Log.ForContext("CustomMessage", "Exception Insert")
                .Error("Insert Exception");
                return null;
            }
        }
        public T Update(T entity)
        {
            try
            {
                _context.Set<T>().Update(entity);
                _context.SaveChanges();
                return entity;
            }
            catch (Exception ex)
            {
                Log.ForContext("CustomMessage", "Exception Update")
                                .Error("Update Exception");
                return null;
            }
        }
        public IEnumerable<T> InsertRange(IEnumerable<T> entities)
        {
            try
            {
                _context.Set<T>().AddRange(entities);
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public IQueryable<T> FindAll()
        {
            return this._context.Set<T>().AsNoTracking();
        }
    }
}
