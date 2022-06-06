using AutoMapper;
using Microsoft.Extensions.Logging;
using RecruitmentManager.Contracts.Repository;
using RecruitmentManager.Core.Core.ErrorsHandler;
using RecruitmentManager.Entities.DTOs;
using RecruitmentManager.Entities.Entities;
using RecruitmentManager.Entities.Utils;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace RecruitmentManager.Core.Core.V1
{
    public class ClientCore
    {
        private readonly IClientRepository _context;
        private readonly ILogger<Client> _logger;
        private readonly ErrorHandler<Client> _errorHandler;
        private readonly IMapper _mapper;

        public ClientCore(ILogger<Client> logger, IMapper mapper, IClientRepository context)
        {
            _logger = logger;
            _errorHandler = new ErrorHandler<Client>(logger);
            _context = context;
            _mapper = mapper;
        }

        public async Task<ResponseService<List<Client>>> GetClientsAsync()
        {
            try
            {
                var response = await _context.GetAllAsync();
                return new ResponseService<List<Client>>(false, response.Count == 0 ? "No records found" : $"{response.Count} records found", HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {
                return _errorHandler.Error(ex, "GetClientAsync", new List<Client>());
            }
        }

        public async Task<ResponseService<Client>> CreateClientAsync(ClientCreateDto entity)
        {
            Client newClient = new();
            newClient = _mapper.Map<Client>(entity);

            try
            {
                var newClientCreated = await _context.AddAsync(newClient);
                return new ResponseService<Client>(false, "Succefully created Client", HttpStatusCode.Created, newClientCreated.Item1);
            }
            catch (Exception ex)
            {
                return _errorHandler.Error(ex, "CreateClientAsync", new Client());
            }
        }

        public async Task<ResponseService<bool>> UpdateClientAsync(Client clientToUpdate)
        {
            try
            {
                var result = await _context.UpdateAsync(clientToUpdate);
                return new ResponseService<bool>(false, result == true?"Record Updated!!":"Record Not Updated", HttpStatusCode.Created, result);
            }
            catch (Exception ex)
            {
                return _errorHandler.Error(ex, "UpdateClientAsync", false);
            }
        }
    }
}
