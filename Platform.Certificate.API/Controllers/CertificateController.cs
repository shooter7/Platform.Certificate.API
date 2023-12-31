﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Platform.Certificate.API.Common.Enums;
using Platform.Certificate.API.Common.Helpers;
using Platform.Certificate.API.Models.Dtos.Certificates;
using Platform.Certificate.API.Models.Forms.Certificates;
using Platform.Certificate.API.Services.Interfaces;

namespace Platform.Certificate.API.Controllers
{
    [ApiController]
    [Route(template: "[controller]")]
    [EnableCors(Constants.AllowOrigin)]
    [Authorize]
    public class CertificateController : ControllerBase
    {
        private readonly ICertificateService _certificateService;

        public CertificateController(ICertificateService certificateService)
        {
            _certificateService = certificateService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(ClientResponse<CertificateDto>), 200)]
        [ProducesResponseType(typeof(ClientResponse<string>), 400)]
        [ProducesResponseType(typeof(void), 204)]
        [AllowAnonymous]
        public async Task<IActionResult> Create([FromForm] CertificateForm form)
        {
            var serviceResponse = await _certificateService.Add(form);
            if (serviceResponse.Failed)
            {
                return BadRequest(new ClientResponse<string>(true, serviceResponse.MessageWithErrors));
            }

            return Ok(new ClientResponse<CertificateDto>(serviceResponse.Data, serviceResponse.ItemsCount));
        }

        [HttpGet]
        [ProducesResponseType(typeof(ClientResponse<List<CertificateDto>>), 200)]
        [ProducesResponseType(typeof(ClientResponse<string>), 400)]
        [ProducesResponseType(typeof(void), 204)]
        public async Task<IActionResult> Get([FromQuery] GetCertificateListForm form)
        {
            var serviceResponse = await _certificateService.Get(form);
            if (serviceResponse.Failed)
            {
                return BadRequest(new ClientResponse<string>(true, serviceResponse.MessageWithErrors));
            }

            return Ok(new ClientResponse<List<CertificateDto>>(serviceResponse.Data, serviceResponse.ItemsCount));
        }

        [HttpGet("GetByCode/{code}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ClientResponse<CertificateDto>), 200)]
        [ProducesResponseType(typeof(ClientResponse<string>), 400)]
        [ProducesResponseType(typeof(void), 204)]
        public async Task<IActionResult> GetByCode(string code)
        {
            var serviceResponse = await _certificateService.GetByCode(code);
            if (serviceResponse.Failed)
            {
                return BadRequest(new ClientResponse<string>(true, serviceResponse.MessageWithErrors));
            }

            return Ok(new ClientResponse<CertificateDto>(serviceResponse.Data, serviceResponse.ItemsCount));
        }

        [HttpGet("{id:int}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ClientResponse<CertificateDto>), 200)]
        [ProducesResponseType(typeof(ClientResponse<string>), 400)]
        [ProducesResponseType(typeof(void), 204)]
        public async Task<IActionResult> GetById(int id)
        {
            var serviceResponse = await _certificateService.GetById(id);
            if (serviceResponse.Failed)
            {
                return BadRequest(new ClientResponse<string>(true, serviceResponse.MessageWithErrors));
            }

            return Ok(new ClientResponse<CertificateDto>(serviceResponse.Data, serviceResponse.ItemsCount));
        }

        [HttpPatch("{id:int}/Approve")]
        [ProducesResponseType(typeof(ClientResponse<bool>), 200)]
        [ProducesResponseType(typeof(ClientResponse<string>), 400)]
        [ProducesResponseType(typeof(void), 204)]
        public async Task<IActionResult> Approve(int id)
        {
            var serviceResponse = await _certificateService.UpdateState(id, CertificateStateEnum.Approved);
            if (serviceResponse.Failed)
            {
                return BadRequest(new ClientResponse<string>(true, serviceResponse.MessageWithErrors));
            }

            return Ok(new ClientResponse<bool>(serviceResponse.Data, serviceResponse.ItemsCount));
        }

        [HttpPatch("{id:int}/Reject")]
        [ProducesResponseType(typeof(ClientResponse<bool>), 200)]
        [ProducesResponseType(typeof(ClientResponse<string>), 400)]
        [ProducesResponseType(typeof(void), 204)]
        public async Task<IActionResult> Reject(int id)
        {
            var serviceResponse = await _certificateService.UpdateState(id, CertificateStateEnum.Rejected);
            if (serviceResponse.Failed)
            {
                return BadRequest(new ClientResponse<string>(true, serviceResponse.MessageWithErrors));
            }

            return Ok(new ClientResponse<bool>(serviceResponse.Data, serviceResponse.ItemsCount));
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(typeof(ClientResponse<bool>), 200)]
        [ProducesResponseType(typeof(ClientResponse<string>), 400)]
        [ProducesResponseType(typeof(void), 204)]
        public async Task<IActionResult> Delete(int id)
        {
            var serviceResponse = await _certificateService.Delete(id);
            if (serviceResponse.Failed)
            {
                return BadRequest(new ClientResponse<string>(true, serviceResponse.MessageWithErrors));
            }

            return Ok(new ClientResponse<bool>(serviceResponse.Data, serviceResponse.ItemsCount));
        }
    }
}