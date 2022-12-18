﻿using Ordering.Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Core.Repositories.Base;

public interface IRepository<T> where T : Entity
{
    Task<IReadOnlyList<T>> GetAllAsync();
    
    Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> expression);
    
    Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> expression = null,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
        bool disableTracking = true);

    Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> expression = null,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
        List<Expression<Func<T,object>>> includes = null,
        bool disableTracking = true);

    Task<T> GetByIdAsync(int id);
    Task<T> AddAsync(T entity);
    Task<T> UpdateAsync(T entity);
    Task<T> DeleteAsync(T entity);
}