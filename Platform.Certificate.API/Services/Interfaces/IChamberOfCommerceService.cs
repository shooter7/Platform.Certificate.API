using Platform.Certificate.API.Common.Helpers;
using Platform.Certificate.API.Models.Dtos.ChamberOfCommerces;
using Platform.Certificate.API.Models.Forms.ChamberOfCommerces;

namespace Platform.Certificate.API.Services.Interfaces;

public interface IChamberOfCommerceService
{
    Task<ServiceResponse<ChamberOfCommerceDto>> Create(CreateChamberOfCommerceFrom form);
    Task<ServiceResponse<ChamberOfCommerceDto>> Update(int id, UpdateChamberOfCommerceForm form);
    Task<ServiceResponse<bool>> Delete(int id);
    Task<ServiceResponse<ChamberOfCommerceDto>> GetById(int id);
    Task<ServiceResponse<List<ChamberOfCommerceDto>>> GetAll();
}