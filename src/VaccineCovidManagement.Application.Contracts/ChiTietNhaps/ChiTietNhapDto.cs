using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace VaccineCovidManagement.ChiTietNhaps
{
    public class ChiTietNhapDto : AuditedEntityDto<Guid>
    {
        public int Stt { get; set; }
        public Guid NhaSxID { get; set; }
        public string TenNhaSX { get; set; }
        public string TenVaccineSX { get; set; }
        public DateTime NgaySx { get; set; }
        public string HanSuDung { get; set; }
        public int SoLuongNhap { get; set; }
    }
}
