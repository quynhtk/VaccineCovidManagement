using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace VaccineCovidManagement.VaccineTonKhos
{
    public interface IVaccineTonKhoRepository : IRepository<VaccineTonKho, Guid>
    {
        Task<List<VaccineTonKho>> GetListAsync(
                int skipCount,
                int maxResultCount,
                string sorting,
                string filter
            );
        Task<VaccineTonKho> FindVaccineTonKhoByIdAsync(Guid id);
    }
}
