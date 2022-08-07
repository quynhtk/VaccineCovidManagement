using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace VaccineCovidManagement.DonViYTes
{
    public class DonViYTeDto : AuditedEntityDto<Guid>
    {
        public int Stt { get; set; }
        public string TenDonViYTe { get; set; }
        public string DiaChi { get; set; }
        public string SDT { get; set; }
    }
}
