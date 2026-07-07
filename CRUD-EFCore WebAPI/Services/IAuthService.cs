using CRUDEFCore.Common;
using CRUDEFCore.DTOs;

namespace CRUDEFCore.Services
{
    public interface IAuthService
    {
        Task<ServiceResult> RegisterAsync(RegisterDto dto);
        Task<ServiceResult<string>> LoginAsync(LoginDto dto);
    }
}
