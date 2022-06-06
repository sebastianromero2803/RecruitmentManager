using Microsoft.EntityFrameworkCore;
using RecruitmentManager.Contracts.Repository;
using RecruitmentManager.DataAccess.Context;
using RecruitmentManager.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace RecruitmentManger.Repositories.ImplementRepositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly SqlServerContext _context;

        public ClientRepository()
        {
            _context = new SqlServerContext();
        }
        public async Task<Tuple<Client, bool>> AddAsync(Client entity)
        {

            try
            {
                var result = _context.Client.Add(entity);
                await _context.SaveChangesAsync();

                return Tuple.Create(result.Entity, true);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<Client>> GetAllAsync()
        {
            try
            {
                var result = await _context.Client.ToListAsync();
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<Client>> GetByFilterAsync(Expression<Func<Client, bool>> expressionFilter)
        {
            try
            {
                return await _context.Client.Where(expressionFilter).ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Client> GetByIdAsync(int id)
        {
            try
            {
                var result = await _context.Client.FindAsync(id);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> UpdateAsync(Client entity)
        {
            try
            {
                var result = _context.Client.Update(entity);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
