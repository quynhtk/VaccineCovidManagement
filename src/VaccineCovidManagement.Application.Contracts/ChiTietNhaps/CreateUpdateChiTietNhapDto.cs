using System;
using System.Collections.Generic;
using System.Text;

namespace VaccineCovidManagement.ChiTietNhaps
{
    public class CreateUpdateChiTietNhapDto
    {
        public Guid NhaSxID { get; set; }
        public string TenNhaSX { get; set; }
        public Guid VaccineTonKhoID { get; set; }
        public string TenVaccineNhap { get; set; }
        public DateTime NgaySx { get; set; }
        public int HanSuDung { get; set; }
        public int SoLuongNhap { get; set; }
    }
}
