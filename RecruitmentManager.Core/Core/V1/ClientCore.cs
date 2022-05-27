using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RecruitmentManager.Core.Core.ErrorsHandler;
using RecruitmentManager.DataAccess.Context;
using RecruitmentManager.Entities.DTOs;
using RecruitmentManager.Entities.Entities;
using RecruitmentManager.Entities.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RecruitmentManager.Core.Core.V1
{
    public class ClientCore
    {
        private readonly SqlServerContext _context;
        private readonly ILogger<Client> _logger;
        private readonly ErrorHandler<Client> _errorHandler;

        public ClientCore(ILogger<Client> logger)
        {
            _logger = logger;
            _errorHandler = new ErrorHandler<Client>(logger);
            _context = new SqlServerContext();
        }

        public async Task<ResponseService<List<Client>>> GetClientsAsync()
        {
            try
            {
                var response = await _context.Client.ToListAsync();
                return new ResponseService<List<Client>>(false, response.Count == 0 ? "No records found" : $"{response.Count} records found", System.Net.HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {
                return _errorHandler.Error(ex, "GetClientAsync", new List<Client>());
            }
        }

        public async Task<ResponseService<Client>> CreateClientAsync(ClientCreateDto entity)
        {
            Client newClient = new();
            newClient.Name = entity.Name;
            newClient.Address = entity.Address;
            newClient.PhoneNumber = entity.PhoneNumber;

            try
            {
                var newClientCreated = _context.Client.Add(newClient);
                await _context.SaveChangesAsync();

                return new ResponseService<Client>(false, "Succefully created Client", HttpStatusCode.Created, newClientCreated.Entity);
            }
            catch (Exception ex)
            {
                return new ResponseService<Client>(true, $"Record not created {ex.Message}", HttpStatusCode.InternalServerError, new Client());
            }
        }

        public async Task<bool> UpdateClientAsync(Client clientToUpdate)
        {
            Client client = _context.Client.Find(clientToUpdate.IdClient);

            client.Name = clientToUpdate.Name;
            client.Address = clientToUpdate.Address;
            client.PhoneNumber = clientToUpdate.PhoneNumber;

            _context.Client.Update(client);

            int recordsAffected = await _context.SaveChangesAsync();

            return (recordsAffected == 1);
        }
    }
}
