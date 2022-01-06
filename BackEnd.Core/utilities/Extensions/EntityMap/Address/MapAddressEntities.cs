using BackEnd.Core.DTOs.Address;
using BackEnd.DataLayer.Entities.Site;
using System.Collections.Generic;

namespace BackEnd.Core.utilities.Extensions.EntityMap.Address
{
    public static class MapAddressEntities
    {
        public static List<AddressDTO> MapToAddrressDTOList(this List<ContactAddress> source)
        {
            var addressesDTO = new List<AddressDTO>();
            foreach (var address in source)
            {
                var addr = new AddressDTO
                {
                    Id = address.Id,
                    Telephone = address.Telephone,
                    Address = address.Address,
                    CellPhone = address.CellPhone,
                    City = address.City,
                    IsDelete = address.IsDelete,
                    WorkHour = address.WorkHour
                };
                addressesDTO.Add(addr);
            }
            return addressesDTO;
        }
        public static AddressDTO MapToAddrressDTO(this ContactAddress source)
        {

                var addressDTO = new AddressDTO
                {
                    Id = source.Id,
                    Telephone = source.Telephone,
                    Address = source.Address,
                    CellPhone = source.CellPhone,
                    City = source.City,
                    IsDelete = source.IsDelete,
                    WorkHour = source.WorkHour
                };
            return addressDTO;
        }
    }
}
