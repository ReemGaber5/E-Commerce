using Abstraction;
using AutoMapper;
using Domain.Exceptions;
using Domain.Models.IdentityModule;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Shared.DTOS.IdentityDTOs;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class AuthenticationServices(UserManager<ApplicationUser> userManager,IConfiguration configuration,IMapper mapper) : IAuthenticationServices
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
                    Token =await CreateTokenAsync(user)
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
                    Token = await CreateTokenAsync(user)
                };
            else
            {
                var Errors = result.Errors.Select(E => E.Description).ToList();
                throw new BadRequestException(Errors);
            }
           
        }

        public async Task<bool> CheckEmailAsync(string email)
        {
           var user =await userManager.FindByNameAsync(email);
            return user != null;

        }

        public async Task<AddressDTO> updatecurrentuserAddress(string email, AddressDTO address)
        {
            var user = await userManager.FindByEmailAsync(email) ?? throw new UserNotFoundException(email);
            if (user.Address != null)
            {
                user.Address.FirstName=address.FirstName;
                user.Address.LastName=address.LastName;
                user.Address.Street=address.Street;
                user.Address.City=address.City;
                user.Address.Country=address.Country;
              
            }
            else
            {
                user.Address = mapper.Map<AddressDTO,Address>(address);
            }
            await userManager.UpdateAsync(user);
            return mapper.Map<Address, AddressDTO>(user.Address);
        }
     
        public async Task<UserDTO> GetCurrentUser(string email)
        {
            var user=await userManager.FindByEmailAsync(email)??throw new UserNotFoundException(email);
            return new UserDTO()
            {
                DisplayName= user.DisplayName,
                Email=email,
                Token= await CreateTokenAsync(user)
            };
        }
        public async Task<AddressDTO> GetCurrentUserAddress(string email)
        {
          var user=await userManager.Users.Include(u=>u.Address).FirstOrDefaultAsync(u=>u.Email==email)?? throw new UserNotFoundException(email);
          if(user.Address!=null)
                return mapper.Map<Address,AddressDTO>(user.Address);
          else
                throw new AddressNotFoundException(user.UserName);
                  
        }
        private async Task<string> CreateTokenAsync(ApplicationUser user)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.Name,user.UserName),
                new Claim(ClaimTypes.NameIdentifier,user.Id)
            };
            var roles = await userManager.GetRolesAsync(user);

            foreach(var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            var SecretKey = configuration.GetSection("JWTOptions")["SecretKey"];
            var key=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));
            var Cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var Token = new JwtSecurityToken
                (
                issuer:configuration.GetSection("JWTOptions")["Issuer"],
                audience:configuration.GetSection("JWTOptions")["Audience"],
                claims:claims,
                expires:DateTime.Now.AddHours(1),
                signingCredentials:Cred
                );

            return new JwtSecurityTokenHandler().WriteToken(Token);





        }
    }
}
