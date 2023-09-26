using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Platform.Certificate.API.Common.Helpers;
using Platform.Certificate.API.Models.Dtos.User;
using Platform.Certificate.API.Models.Forms.User;
using Platform.Certificate.API.Services.Interfaces;

namespace Platform.Certificate.API.Controllers
{
    [ApiController]
    [Route(template: "[controller]")]
    [EnableCors(Constants.AllowOrigin)]
    // [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(ClientResponse<UserDto>), 200)]
        [ProducesResponseType(typeof(ClientResponse<string>), 400)]
        [ProducesResponseType(typeof(void), 204)]
        public async Task<IActionResult> Create([FromBody] CreateUserForm form)
        {
            var serviceResponse = await _userService.Create(form);
            if (serviceResponse.Failed)
            {
                return BadRequest(new ClientResponse<string>(true, serviceResponse.MessageWithErrors));
            }

            return Ok(new ClientResponse<UserDto>(serviceResponse.Data, serviceResponse.ItemsCount));
        }

        [HttpGet]
        [ProducesResponseType(typeof(ClientResponse<List<UserDto>>), 200)]
        [ProducesResponseType(typeof(ClientResponse<string>), 400)]
        [ProducesResponseType(typeof(void), 204)]
        public async Task<IActionResult> Get([FromQuery] GetUserListForm form)
        {
            var serviceResponse = await _userService.Get(form);
            if (serviceResponse.Failed)
            {
                return BadRequest(new ClientResponse<string>(true, serviceResponse.MessageWithErrors));
            }

            return Ok(new ClientResponse<List<UserDto>>(serviceResponse.Data, serviceResponse.ItemsCount));
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(ClientResponse<UserDto>), 200)]
        [ProducesResponseType(typeof(ClientResponse<string>), 400)]
        [ProducesResponseType(typeof(void), 204)]
        public async Task<IActionResult> GetById(int id)
        {
            var serviceResponse = await _userService.GetById(id);
            if (serviceResponse.Failed)
            {
                return BadRequest(new ClientResponse<string>(true, serviceResponse.MessageWithErrors));
            }

            return Ok(new ClientResponse<UserDto>(serviceResponse.Data, serviceResponse.ItemsCount));
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(typeof(ClientResponse<bool>), 200)]
        [ProducesResponseType(typeof(ClientResponse<string>), 400)]
        [ProducesResponseType(typeof(void), 204)]
        public async Task<IActionResult> Delete(int id)
        {
            var serviceResponse = await _userService.Delete(id);
            if (serviceResponse.Failed)
            {
                return BadRequest(new ClientResponse<string>(true, serviceResponse.MessageWithErrors));
            }

            return Ok(new ClientResponse<bool>(serviceResponse.Data, serviceResponse.ItemsCount));
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(typeof(ClientResponse<UserDto>), 200)]
        [ProducesResponseType(typeof(ClientResponse<string>), 400)]
        [ProducesResponseType(typeof(void), 204)]
        public async Task<IActionResult> Update(int id, UpdateUserForm form)
        {
            var serviceResponse = await _userService.Update(id, form);
            if (serviceResponse.Failed)
            {
                return BadRequest(new ClientResponse<string>(true, serviceResponse.MessageWithErrors));
            }

            return Ok(new ClientResponse<UserDto>(serviceResponse.Data, serviceResponse.ItemsCount));
        }

        [HttpPatch("{id:int}")]
        [ProducesResponseType(typeof(ClientResponse<UserDto>), 200)]
        [ProducesResponseType(typeof(ClientResponse<string>), 400)]
        [ProducesResponseType(typeof(void), 204)]
        public async Task<IActionResult> UpdatePassword(int id, UpdatePasswordForm form)
        {
            var serviceResponse = await _userService.UpdatePassword(id, form);
            if (serviceResponse.Failed)
            {
                return BadRequest(new ClientResponse<string>(true, serviceResponse.MessageWithErrors));
            }

            return Ok(new ClientResponse<UserDto>(serviceResponse.Data, serviceResponse.ItemsCount));
        }
    }
}