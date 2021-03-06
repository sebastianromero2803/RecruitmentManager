using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RecruitmentManager.Contracts.Repository;
using RecruitmentManager.Core.Core.V1;
using RecruitmentManager.Entities.DTOs;
using RecruitmentManager.Entities.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RecruitmentManager.Services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly ClientCore _clientCore;

        public ClientController(ILogger<Client> logger, IMapper mapper, IClientRepository context)
        {
            _clientCore = new ClientCore(logger, mapper, context);
        }

        // GET: api/<ClientController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Client>>> Get()
        {
            var response = await _clientCore.GetClientsAsync();
            return StatusCode((int)response.StatusHttp, response);
        }

        // GET api/<ClientController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ClientController>
        [HttpPost]
        public async Task<ActionResult<Client>> Post([FromBody] ClientCreateDto client)
        {
            var response = await _clientCore.CreateClientAsync(client);
            return StatusCode((int)response.StatusHttp, response);
        }

        // PUT api/<ClientController>/5
        [HttpPut]
        public async Task<ActionResult<bool>> Put([FromBody] Client client)
        {
            var response = await _clientCore.UpdateClientAsync(client);
            return StatusCode((int)response.StatusHttp, response);
        }
    }
}
