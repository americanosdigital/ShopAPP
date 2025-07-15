using ShopAPP.Application.DTOs.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopAPP.Application.Interfaces.Account
{
    public interface IUserService
    {
        Task<(bool Success, IEnumerable<string> Errors)> RegisterUserAsync(RegisterUserDto dto);
        Task<LoginResponseDto?> AuthenticateAsync(LoginRequestDto loginDto);
    }

}
