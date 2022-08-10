using System;
using System.Collections.Generic;
using System.Text;

namespace VaccineCovidManagement.ChiTietXuats
{
    public class CreateUpdateChiTietXuatDto
    {
        public Guid DonViID { get; set; }
        public string TenDonViYTe { get; set; }
        public Guid VaccineTonKhoID { get; set; }
        public string TenVaccineXuat { get; set; }
        public int SoLuongXuat { get; set; }
    }
}
