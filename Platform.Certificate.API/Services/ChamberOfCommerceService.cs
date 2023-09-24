using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Platform.Certificate.API.Common.Extensions;
using Platform.Certificate.API.Common.Helpers;
using Platform.Certificate.API.DAL.Data;
using Platform.Certificate.API.Models.Dbs;
using Platform.Certificate.API.Models.Dtos.ChamberOfCommerces;
using Platform.Certificate.API.Models.Dtos.User;
using Platform.Certificate.API.Models.Forms.ChamberOfCommerces;
using Platform.Certificate.API.Services.Interfaces;

namespace Platform.Certificate.API.Services;

public class ChamberOfCommerceService : IChamberOfCommerceService
{
    private readonly EfContext _efContext;
    private readonly IMapper _mapper;

    public ChamberOfCommerceService(EfContext efContext, IMapper mapper)
    {
        _efContext = efContext;
        _mapper = mapper;
    }

    public async Task<ServiceResponse<ChamberOfCommerceDto>> Create(CreateChamberOfCommerceFrom form)
    {
        var serviceResponse = new ServiceResponse<ChamberOfCommerceDto>();
        try
        {
            var chamberOfCommerce = _mapper.Map<ChamberOfCommerce>(form);
            await _efContext.ChamberOfCommerces.AddAsync(chamberOfCommerce);
            await _efContext.SaveChangesAsync();
            serviceResponse.Successful().WithData(_mapper.Map<ChamberOfCommerceDto>(chamberOfCommerce));
        }
        catch (Exception e)
        {
            serviceResponse.Failed().WithError(0, e.Message, null);
        }

        return serviceResponse;
    }

    public async Task<ServiceResponse<ChamberOfCommerceDto>> Update(int id, UpdateChamberOfCommerceForm form)
    {
        var serviceResponse = new ServiceResponse<ChamberOfCommerceDto>();
        try
        {
            var chamberOfCommerce = await _efContext.ChamberOfCommerces.FindAsync(id);
            if (chamberOfCommerce == null)
            {
                serviceResponse.Failed().WithError(1, "ChamberOfCommerce Not Found", null);
            }
            else
            {
                _mapper.Map(form, chamberOfCommerce);
                _efContext.ChamberOfCommerces.Update(chamberOfCommerce);
                await _efContext.SaveChangesAsync();
                serviceResponse.Successful().WithData(_mapper.Map<ChamberOfCommerceDto>(chamberOfCommerce));
            }
        }
        catch (Exception e)
        {
            serviceResponse.Failed().WithError(0, e.Message, null);
        }

        return serviceResponse;
    }

    public async Task<ServiceResponse<bool>> Delete(int id)
    {
        var serviceResponse = new ServiceResponse<bool>();
        try
        {
            var chamberOfCommerce = await _efContext.ChamberOfCommerces.FindAsync(id);
            if (chamberOfCommerce == null)
            {
                serviceResponse.Failed().WithError(1, "ChamberOfCommerce Not Found", null);
            }
            else
            {
                _efContext.ChamberOfCommerces.Remove(chamberOfCommerce);
                await _efContext.SaveChangesAsync();
                serviceResponse.Successful().WithData(true);
            }
        }
        catch (Exception e)
        {
            serviceResponse.Failed().WithError(0, e.Message, null);
        }

        return serviceResponse;
    }

    public async Task<ServiceResponse<ChamberOfCommerceDto>> GetById(int id)
    {
        var serviceResponse = new ServiceResponse<ChamberOfCommerceDto>();
        try
        {
            var dto = await _efContext.ChamberOfCommerces
                .ProjectTo<ChamberOfCommerceDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (dto == null)
            {
                serviceResponse.Failed().WithError(1, "ChamberOfCommerce Not Found", null);
            }
            else
            {
                serviceResponse.Successful().WithData(dto);
            }
        }
        catch (Exception e)
        {
            serviceResponse.Failed().WithError(0, e.Message, null);
        }

        return serviceResponse;
    }

    public async Task<ServiceResponse<List<ChamberOfCommerceDto>>> GetAll()
    {
        var serviceResponse = new ServiceResponse<List<ChamberOfCommerceDto>>();
        try
        {
            var dtos = await _efContext.ChamberOfCommerces
                .ProjectTo<ChamberOfCommerceDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
            serviceResponse.Successful().WithData(dtos);
        }
        catch (Exception e)
        {
            serviceResponse.Failed().WithError(0, e.Message, null);
        }

        return serviceResponse;
    }
}