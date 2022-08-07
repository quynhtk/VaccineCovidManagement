using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace VaccineCovidManagement.ChiTietNhaps
{
    public class GetNhaSanXuatLookup : EntityDto<Guid>
    {
        public string TenNhaSX { get; set; }
    }
}
