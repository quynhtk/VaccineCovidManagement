using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace VaccineCovidManagement.DonViYTes
{
    public interface IDonViYTeRepository : IRepository<DonViYTe, Guid>
    {
        Task<List<DonViYTe>> GetListAsync(
                int skipCount,
                int maxResultCount,
                string sorting,
                string filter
            );
        Task<DonViYTe> FindByDonViYTeAsync(string donViYTe);
    }
}
