using Platform.Certificate.API.Common.Helpers;

namespace Platform.Certificate.API.Common.Interfaces.Services
{
    public interface IServiceBase<TId,TModelDto,TGetList,TCreate,TUpdate>
    {
        Task<ServiceResponse<TModelDto>> Create(TCreate createForm);
        Task<ServiceResponse<TModelDto>> GetById(TId id);
        Task<ServiceResponse<List<TModelDto>>> Get(TGetList getListForm);
        Task<ServiceResponse<TModelDto>> Update(TId id,TUpdate updateForm);
        Task<ServiceResponse<bool>> Delete(TId id);
    }
}
