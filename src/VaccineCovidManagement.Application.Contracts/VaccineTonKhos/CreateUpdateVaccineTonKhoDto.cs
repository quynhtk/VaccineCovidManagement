using System;
using System.Collections.Generic;
using System.Text;

namespace VaccineCovidManagement.VaccineTonKhos
{
    public class CreateUpdateVaccineTonKhoDto
    {
        public Guid ChiTietNhapId { get; set; }
        public int SoLuongTonKho { get; set; }
    }
}
