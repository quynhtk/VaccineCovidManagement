using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace VaccineCovidManagement.DonViYTes
{
    public class DonViYTe : AuditedAggregateRoot<Guid>
    {
        public string TenDonViYTe { get; set; }
        public string DiaChi { get; set; }
        public string SDT { get; set; }
    }
}
