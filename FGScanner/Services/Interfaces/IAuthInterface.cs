using FGScanner.DTOs;
using FGScanner.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGScanner.Services.Interfaces
{
    public interface IAuthInterface
    {
        Task<(ServiceResponseDto, User)> AuthenticateUser(UserInputDto inputDto);
    }
}
