using Mango.Service.AuthAPI.Models.Dto;

namespace Mango.Service.AuthAPI.Service.IService
{
    public interface IAuthService
    {
        Task<UserDto> Register(RegistrationRequestDto registrationRequestDto);
        Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto); 
    }
}
