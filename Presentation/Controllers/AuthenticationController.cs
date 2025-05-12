using Abstraction;
using Microsoft.AspNetCore.Mvc;
using Shared.DTOS.IdentityDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AuthenticationController(IServiceManger serviceManger) : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<UserDTO>>LoginAsync(LoginDTO login)
        {
            var user=await serviceManger.AuthenticationServices.LoginAsync(login);
            return Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult<UserDTO>> RegisterAsync(RegisterDTO register)
        {
            var user = await serviceManger.AuthenticationServices.RegisterAsync(register);
            return Ok(user);
        }

        [HttpGet]
        public async Task<ActionResult<bool>> CheckEmail(string email)
        {
            var result =await serviceManger.AuthenticationServices.CheckEmailAsync(email);
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<UserDTO>> GetCurrentUser()
        {
            var email=User.FindFirstValue(ClaimTypes.Email);
            var currentuser=await serviceManger.AuthenticationServices.GetCurrentUser(email);
            return Ok(currentuser);

        }

        [HttpGet]
        public async Task<ActionResult<AddressDTO>> GetCurrentUserAdrress()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var address=serviceManger.AuthenticationServices.GetCurrentUserAddress(email);
            return Ok(address);

        }

        [HttpPost]
        public async Task<ActionResult<AddressDTO>>UpdateAddress(AddressDTO address)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var updatedaddress=await serviceManger.AuthenticationServices.updatecurrentuserAddress(email, address);
            return Ok(updatedaddress);

        }


    }
}
