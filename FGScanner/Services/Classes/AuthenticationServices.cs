using FGScanner.DTOs;
using FGScanner.Models;
using FGScanner.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;


namespace FGScanner.Services.Classes
{
    public class AuthenticationServices : IAuthInterface
    {
        private readonly InventoryDbDevContext context;

        public AuthenticationServices(InventoryDbDevContext context)
        {
           this.context = context;
        }


        public async Task<(ServiceResponseDto, User)> AuthenticateUser(UserInputDto inputDto)
        {
            try
            {
                var isUserExist = await context.Users.FirstOrDefaultAsync(x => x.UserId == inputDto.Username);

                if (isUserExist == null)
                {
                    return (new ServiceResponseDto
                    {
                        Success = false,
                        Message = "User account not exist on database.",
                    }, null);
                }

                if (isUserExist.Password != inputDto.Password)
                {
                    return (new ServiceResponseDto
                    {
                        Success = false,
                        Message = "Password incorrect.",
                    }, null);
                }

                return (new ServiceResponseDto
                {
                    Success = true,
                    Message = "Login successfully.",
                }, isUserExist);
            }
            catch(Exception ex) 
            {
                return (new ServiceResponseDto
                {
                    Success = false,
                    Message = $"SQL Error: {ex.Message}",
                }, null);
            }
        }
    }
}
