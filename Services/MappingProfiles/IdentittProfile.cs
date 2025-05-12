using AutoMapper;
using Domain.Models.IdentityModule;
using Shared.DTOS.IdentityDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.MappingProfiles
{
    public class IdentittProfile:Profile
    {
        public IdentittProfile()
        {
            CreateMap<Address,AddressDTO>().ReverseMap();   
        }
    }
}
