using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using WebAPIMongo.Models;
using WebAPIMongo.Services;

namespace WebAPIMongo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly ClientServices _clientServices;
        private readonly AddressServices _addressServices;

        public ClientController(ClientServices clientServices, AddressServices addressServices)
        {
            _clientServices = clientServices;
            _addressServices = addressServices;
        }

        [HttpGet]
        public ActionResult<List<Client>> Get() => _clientServices.Get();

        [HttpGet("{id:length(24)}", Name = "GetClient")]
        public ActionResult<Client> Get(string id)
        {
            var client = _clientServices.Get(id);

            if (client == null)
                return NotFound();

            return Ok(client);
        }

        [HttpGet("Name",Name = "GetClientName")]
        public ActionResult<Client> GetName(string name)
        {
            var client = _clientServices.GetName(name);

            if (client == null)
                return NotFound();

            return Ok(client);
        }

        [HttpGet("Address/{id:length(24)}", Name = "GetClientEnd")]
        public ActionResult<Client> GetEnd(string id)
        {
            var client = _clientServices.GetEnd(id);

            if (client == null)
                return NotFound();

            return Ok(client);
        }

        [HttpPost]
        public ActionResult<Client> Create(Client client)
        {
            Address address = _addressServices.Create(client.Address);
            client.Address = address;
            _clientServices.Create(client);
            return CreatedAtRoute("GetClient", new { id = client.Id.ToString() }, client);
        }

        [HttpPut]
        public ActionResult<Client> Update(Client clientIn, string id)
        {
            var client = _clientServices.Get(id);

            if (client == null)
                return NotFound();

            clientIn.Id = id;
            _clientServices.Update(clientIn.Id, clientIn);

            return Ok(clientIn);
        }

        [HttpDelete]
        public ActionResult Delete(string id)
        {
            var client = _clientServices.Get(id);

            if (client == null)
                return NotFound();

            _clientServices.Delete(client);

            return NoContent();
        }
    }
}
