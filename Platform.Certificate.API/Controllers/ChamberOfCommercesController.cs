using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Platform.Certificate.API.Common.Helpers;
using Platform.Certificate.API.Models.Dtos.ChamberOfCommerces;
using Platform.Certificate.API.Models.Forms.ChamberOfCommerces;
using Platform.Certificate.API.Services.Interfaces;

namespace Platform.Certificate.API.Controllers;

[ApiController]
[Route(template: "[controller]")]
[EnableCors(Constants.AllowOrigin)]
public class ChamberOfCommercesController : ControllerBase
{
    private readonly IChamberOfCommerceService _service;

    public ChamberOfCommercesController(IChamberOfCommerceService service)
    {
        _service = service;
    }

    [HttpPost]
    [ProducesResponseType(typeof(ClientResponse<ChamberOfCommerceDto>), 200)]
    [ProducesResponseType(typeof(ClientResponse<string>), 400)]
    [ProducesResponseType(typeof(void), 204)]
    public async Task<IActionResult> Create([FromForm] CreateChamberOfCommerceFrom form)
    {
        var serviceResponse = await _service.Create(form);
        if (serviceResponse.Failed)
        {
            return BadRequest(new ClientResponse<string>(true, serviceResponse.MessageWithErrors));
        }

        return Ok(new ClientResponse<ChamberOfCommerceDto>(serviceResponse.Data, serviceResponse.ItemsCount));
    }

    [HttpGet]
    [ProducesResponseType(typeof(ClientResponse<List<ChamberOfCommerceDto>>), 200)]
    [ProducesResponseType(typeof(ClientResponse<string>), 400)]
    [ProducesResponseType(typeof(void), 204)]
    public async Task<IActionResult> Get()
    {
        var serviceResponse = await _service.GetAll();
        if (serviceResponse.Failed)
        {
            return BadRequest(new ClientResponse<string>(true, serviceResponse.MessageWithErrors));
        }

        return Ok(new ClientResponse<List<ChamberOfCommerceDto>>(serviceResponse.Data, serviceResponse.ItemsCount));
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(ClientResponse<ChamberOfCommerceDto>), 200)]
    [ProducesResponseType(typeof(ClientResponse<string>), 400)]
    [ProducesResponseType(typeof(void), 204)]
    public async Task<IActionResult> GetById(int id)
    {
        var serviceResponse = await _service.GetById(id);
        if (serviceResponse.Failed)
        {
            return BadRequest(new ClientResponse<string>(true, serviceResponse.MessageWithErrors));
        }

        return Ok(new ClientResponse<ChamberOfCommerceDto>(serviceResponse.Data, serviceResponse.ItemsCount));
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(typeof(ClientResponse<bool>), 200)]
    [ProducesResponseType(typeof(ClientResponse<string>), 400)]
    [ProducesResponseType(typeof(void), 204)]
    public async Task<IActionResult> Delete(int id)
    {
        var serviceResponse = await _service.Delete(id);
        if (serviceResponse.Failed)
        {
            return BadRequest(new ClientResponse<string>(true, serviceResponse.MessageWithErrors));
        }

        return Ok(new ClientResponse<bool>(serviceResponse.Data, serviceResponse.ItemsCount));
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(ClientResponse<ChamberOfCommerceDto>), 200)]
    [ProducesResponseType(typeof(ClientResponse<string>), 400)]
    [ProducesResponseType(typeof(void), 204)]
    public async Task<IActionResult> Update(int id, UpdateChamberOfCommerceForm form)
    {
        var serviceResponse = await _service.Update(id, form);
        if (serviceResponse.Failed)
        {
            return BadRequest(new ClientResponse<string>(true, serviceResponse.MessageWithErrors));
        }

        return Ok(new ClientResponse<ChamberOfCommerceDto>(serviceResponse.Data, serviceResponse.ItemsCount));
    }
}