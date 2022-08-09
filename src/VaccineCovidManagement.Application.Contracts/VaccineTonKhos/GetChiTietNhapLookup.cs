using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace VaccineCovidManagement.VaccineTonKhos
{
    public class GetChiTietNhapLookup : EntityDto<Guid>
    {
        public string TenVaccineSX { get; set; }
    }
}
