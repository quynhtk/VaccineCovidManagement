using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace VaccineCovidManagement.ChiTietNhaps
{
    public class GetVaccineTKLookup : EntityDto<Guid>
    {
        public string TenVaccineTonKho { get; set; }
    }
}
