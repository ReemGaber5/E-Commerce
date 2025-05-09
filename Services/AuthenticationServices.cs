using Abstraction;
using Domain.Exceptions;
using Domain.Models.IdentityModule;
using Microsoft.AspNetCore.Identity;
using Shared.DTOS.IdentityDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class AuthenticationServices(UserManager<ApplicationUser> userManager) : IAuthenticationServices
    {
        public async Task<UserDTO> LoginAsync(LoginDTO loginDTO)
        {
            var user=await userManager.FindByEmailAsync(loginDTO.Email);
            if (user is null) throw new UserNotFoundException(loginDTO.Email);
            //if email exist check password exist or not
            var ispasswoedvalid =await userManager.CheckPasswordAsync(user, loginDTO.Password);
            if (ispasswoedvalid)
            {
                return new UserDTO()
                {
                    DisplayName = user.DisplayName,
                    Email = loginDTO.Email,
                    Token = CreateTokenAsync(user)
                };
            }
            else
            {
                throw new UnAuthorizedException();
            }


        }

        public async Task<UserDTO> RegisterAsync(RegisterDTO registerDTO)
        {
            var user = new ApplicationUser()
            {
                DisplayName = registerDTO.DisplayName,
                Email = registerDTO.Email,
                PhoneNumber = registerDTO.PhoneNumber,
                UserName = registerDTO.UserName,
            };
            //create user
            var result=await userManager.CreateAsync(user,registerDTO.Password);
            if (result.Succeeded)
                return new UserDTO()
                {
                    DisplayName = registerDTO.DisplayName,
                    Email = registerDTO.Email,
                    Token = CreateTokenAsync(user)
                };
            else
            {
                var Errors = result.Errors.Select(E => E.Description).ToList();
                throw new BadRequestException(Errors);
            }
           
        }
        public static string CreateTokenAsync(ApplicationUser user)
        {
            return "todo";

        }
    }
}
