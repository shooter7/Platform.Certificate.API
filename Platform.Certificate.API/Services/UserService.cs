using AutoMapper;
using Platform.Certificate.API.Models.Dbs;
using Microsoft.EntityFrameworkCore;
using Platform.Certificate.API.Common.Helpers;
using Platform.Certificate.API.DAL.Data;
using Platform.Certificate.API.Models.Dtos.User;
using Platform.Certificate.API.Models.Forms.User;
using Platform.Certificate.API.Common.Extensions;
using Platform.Certificate.API.Services.Interfaces;

namespace Platform.Certificate.API.Services
{
    public class UserService : IUserService
    {
        private readonly EfContext _efContext;
        private readonly IMapper _mapper;

        public UserService(EfContext efContext, IMapper mapper)
        {
            _efContext = efContext;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<UserDto>> Create(CreateUserForm createUserForm)
        {
            var serviceResponse = new ServiceResponse<UserDto>();
            try
            {
                var userTemp = await _efContext.Users
                    .FirstOrDefaultAsync(x => x.Username.Equals(createUserForm.Username));
                if (userTemp != null)
                {
                    serviceResponse.Failed().WithError(1, "Username Is Exist", null);
                }
                else
                {
                    var user = _mapper.Map<User>(createUserForm);
                    user.Password = createUserForm.Password.HashPassword();
                    await _efContext.Users.AddAsync(user);
                    await _efContext.SaveChangesAsync();
                    serviceResponse.Successful().WithData(_mapper.Map<UserDto>(user));
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
                var user = await _efContext.Users
                    .FirstOrDefaultAsync(x => x.Id.Equals(id));
                if (user == null)
                {
                    serviceResponse.Failed().WithError(1, "Not found", null);
                }
                else
                {
                    _efContext.Users.Remove(user);
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

        public async Task<ServiceResponse<List<UserDto>>> Get(GetUserListForm getUserListForm)
        {
            var query = _efContext.Users
                .WhereIf(getUserListForm.Fullname != null,
                    x => x.Fullname.ToLower().Contains(getUserListForm.Fullname.ToLower()))
                .WhereIf(getUserListForm.Username != null, x => x.Username.Equals(getUserListForm.Username))
                .Select(x => _mapper.Map<UserDto>(x));
            var count = await query.CountAsync();
            var list = await query.Skip(getUserListForm.Start).Take(getUserListForm.Take).ToListAsync();
            return new ServiceResponse<List<UserDto>>().Successful().WithData(list).WithCount(count);
        }

        public async Task<ServiceResponse<UserDto>> GetById(int id)
        {
            var serviceResponse = new ServiceResponse<UserDto>();
            var user = await _efContext.Users.FirstOrDefaultAsync(x => x.Id.Equals(id));
            if (user == null)
            {
                return serviceResponse.Failed().WithError(1, "Not found", null);
            }

            var userDto = _mapper.Map<UserDto>(user);
            return serviceResponse.Successful().WithData(userDto);
        }

        public async Task<ServiceResponse<UserDto>> Login(LoginForm loginForm)
        {
            var serviceResponse = new ServiceResponse<UserDto>();
            try
            {
                var user = await _efContext.Users
                    .FirstOrDefaultAsync(x => x.Username.Equals(loginForm.Username));
                if (user is null || !user.Password.VerifyHash(loginForm.Password))
                {
                    serviceResponse.Failed().WithError(1, "Invalid Username or Password", null);
                }
                else
                {
                    serviceResponse.Successful().WithData(_mapper.Map<UserDto>(user));
                }
            }
            catch (Exception e)
            {
                serviceResponse.Failed().WithError(0, e.Message, null);
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<UserDto>> Update(int id, UpdateUserForm updateUserForm)
        {
            var serviceResponse = new ServiceResponse<UserDto>();
            try
            {
                var user = await _efContext.Users
                    .FirstOrDefaultAsync(x => x.Id.Equals(id));

                var userTemp = await _efContext.Users.FirstOrDefaultAsync(x =>
                    x.Username.Equals(updateUserForm.Username));
                if (user == null)
                {
                    serviceResponse.Failed().WithError(1, "Not found", null);
                }
                else if (userTemp != null && !userTemp.Username.Equals(user.Username))
                {
                    serviceResponse.Failed().WithError(2, "Username is exist", null);
                }
                else
                {
                    _mapper.Map(updateUserForm, user);
                    _efContext.Users.Update(user);
                    await _efContext.SaveChangesAsync();
                    serviceResponse.Successful().WithData(_mapper.Map<UserDto>(user));
                }
            }
            catch (Exception e)
            {
                serviceResponse.Failed().WithError(0, e.Message, null);
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<UserDto>> UpdatePassword(int id, UpdatePasswordForm form)
        {
            var serviceResponse = new ServiceResponse<UserDto>();
            try
            {
                var user = await _efContext.Users
                    .FirstOrDefaultAsync(x => x.Id == id);
                if (user == null)
                {
                    serviceResponse.Failed().WithError(1, "Not found", null);
                }
                else if (!user.Password.VerifyHash(form.OldPassword))
                {
                    serviceResponse.Failed().WithError(2, "Invalid password", null);
                }
                else
                {
                    user.Password = form.NewPassword.HashPassword();
                    _efContext.Users.Update(user);
                    await _efContext.SaveChangesAsync();
                    serviceResponse.Successful().WithData(_mapper.Map<UserDto>(user));
                }
            }
            catch (Exception e)
            {
                serviceResponse.Failed().WithError(0, e.Message, null);
            }

            return serviceResponse;
        }
    }
}