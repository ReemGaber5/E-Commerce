using Abstraction;
using Microsoft.AspNetCore.Mvc;
using Shared.DTOS.IdentityDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    class AuthenticationController(IServiceManger serviceManger) : APIBaseController
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



    }
}
