using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace VaccineCovidManagement.VaccineTonKhos
{
    public class VaccineTonKhoDto : AuditedEntityDto<Guid>
    {
        public int Stt { get; set; }
        public Guid ChiTietNhapId { get; set; }
        public string TenVaccineTonKho { get; set; }
        public int SoLuongTonKho { get; set; }
    }
}
