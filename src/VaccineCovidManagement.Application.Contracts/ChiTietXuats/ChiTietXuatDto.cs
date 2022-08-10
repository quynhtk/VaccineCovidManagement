using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace VaccineCovidManagement.ChiTietXuats
{
    public class ChiTietXuatDto : AuditedEntityDto<Guid>
    {
        public int Stt { get; set; }
        public Guid DonViID { get; set; }
        public string TenDonViYTe { get; set; }
        public Guid VaccineTonKhoID { get; set; }
        public string TenVaccineXuat { get; set; }
        public int SoLuongXuat { get; set; }
    }
}
