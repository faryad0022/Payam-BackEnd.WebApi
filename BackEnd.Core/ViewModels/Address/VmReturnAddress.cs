using System;
using System.Collections.Generic;
using System.Text;
using BackEnd.DataLayer.Entities.Common;

namespace BackEnd.Core.ViewModels.Address
{
    public class VmReturnAddress
    {
        public long Id { get; set; }
        public bool IsDelete { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string CellPhone { get; set; }
        public string Telephone { get; set; }
        public string WorkHour { get; set; }
    }
}
