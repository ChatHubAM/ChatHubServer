using AccountControlService.Models;
using AutoMapper;
using DbAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountControlService.Mapping
{
    /// <summary>
    /// Maps the DTO to Entity class for writing to database
    /// </summary>
    public class UserDTOToUserEntity : Profile
    {
        public UserDTOToUserEntity()
        {
            CreateMap<UserDTO, UserEntity>();
        }
    }
}
