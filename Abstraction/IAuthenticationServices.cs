using Shared.DTOS.IdentityDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstraction
{
    public interface IAuthenticationServices
    {
        Task<UserDTO>LoginAsync(LoginDTO loginDTO);
        Task<UserDTO> RegisterAsync(RegisterDTO registerDTO);
        Task<bool>CheckEmailAsync(string email);
        Task<AddressDTO>GetCurrentUserAddress(string email);    
        Task<AddressDTO>updatecurrentuserAddress(string email,AddressDTO address);
        Task<UserDTO>GetCurrentUser(string email);





    }
}
