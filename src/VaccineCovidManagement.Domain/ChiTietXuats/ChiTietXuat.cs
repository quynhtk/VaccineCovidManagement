using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace VaccineCovidManagement.ChiTietXuats
{
    public class ChiTietXuat : AuditedAggregateRoot<Guid>
    {
        public Guid DonViID { get; set; }
        public string TenDonViYTe { get; set; }
        public Guid VaccineTonKhoID { get; set; }
        public string TenVaccineXuat { get; set; }
        public int SoLuongXuat { get; set; }
    }
}
