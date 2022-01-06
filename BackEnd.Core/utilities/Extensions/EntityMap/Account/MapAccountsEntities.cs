using BackEnd.Core.DTOs.Account;
using BackEnd.DataLayer.Entities.Access;
using BackEnd.DataLayer.Entities.Account;
using System;
using System.Collections.Generic;
using System.Text;

namespace BackEnd.Core.utilities.Extensions.EntityMap
{
    public static class MapAccountsEntities
    {
        public static List<UserDTO> MapToUserDTO(this List<User> source)
        {
            var userDTO = new List<UserDTO>();
            foreach (var user in source)
            {
                var vm = new UserDTO
                {
                    Address = user.Address,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Id = user.Id,
                    IsDelete = user.IsDelete,
                    IsActivated = user.IsActivated

                };
                userDTO.Add(vm);
            }
            return userDTO;
        }

        public static List<RoleDTO> MapToRoleDTO(this List<Role> source)
        {
            var rolesDTO = new List<RoleDTO>();
            foreach (var role in source)
            {
                var vm = new RoleDTO
                {
                    Id = role.Id,
                    Name = role.Name,
                    Title = role.Title
                };
                rolesDTO.Add(vm);
            }
            return rolesDTO;
        }

    }
}
