﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace RecruitmentManager.Contracts.Generics
{
    public interface IGenericActionDbQuery<T> where T : class
    {
        Task<List<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task<List<T>> GetByFilterAsync(Expression<Func<T, bool>> expressionFilter = null);
    }
}
