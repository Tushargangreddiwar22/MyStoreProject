using MyStore.DTOs;

namespace MyStore.Services
{
    public interface IAuthService
    {
        Task RegisterAsync(RegisterDto dto);

        Task<AuthResponseDto> LoginAsync(LoginDto dto);
    }
}
