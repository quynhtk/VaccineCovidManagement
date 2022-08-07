using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace VaccineCovidManagement.VaccineTonKhos
{
    public class VaccineTonKho : AuditedAggregateRoot<Guid>
    {
        public Guid ChiTietNhapId { get; set; }
        public string TenVaccineTonKho { get; set; }
        public int SoLuongTonKho { get; set; }
    }
}
