using Platform.Certificate.API.Common.Helpers;
using Platform.Certificate.API.Common.Interfaces.Services;
using Platform.Certificate.API.Models.Dtos.User;
using Platform.Certificate.API.Models.Forms.User;

namespace Platform.Certificate.API.Services.Interfaces
{
    public interface IUserService : IServiceBase<int, UserDto, GetUserListForm, CreateUserForm, UpdateUserForm>
    {
        Task<ServiceResponse<UserDto>> Login(LoginForm loginForm);
        Task<ServiceResponse<UserDto>> UpdatePassword(int id, UpdatePasswordForm form);
    }
}