using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace VaccineCovidManagement.NhaSanXuats
{
    public class NhaSanXuatDto : AuditedEntityDto<Guid>
    {
        public int Stt { get; set; }
        public string TenNhaSX { get; set; }
        public string TenVaccineSX { get; set; }
        public string Diachi { get; set; }
        public string SDT { get; set; }
        public string Email { get; set; }
    }
}
