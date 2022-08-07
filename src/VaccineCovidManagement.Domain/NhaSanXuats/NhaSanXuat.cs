using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace VaccineCovidManagement.NhaSanXuats
{
    public class NhaSanXuat : AuditedAggregateRoot<Guid>
    {
        public string TenNhaSX { get; set; }
        public string TenVaccineSX { get; set; }
        public string Diachi { get; set; }
        public string SDT { get; set; }
        public string Email { get; set; }
    }
}
