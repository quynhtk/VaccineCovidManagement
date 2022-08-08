using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace VaccineCovidManagement.ChiTietXuats
{
    public interface IChiTietXuatRepository : IRepository<ChiTietXuat, Guid>
    {
        Task<List<ChiTietXuat>> GetListAsync(
                int skipCount,
                int maxResultCount,
                string sorting,
                string filter
            );
        Task<ChiTietXuat> FindByIdDonViYTeAsync(Guid id);
        Task<ChiTietXuat> FindByIdVaccineTonKhoAsync(Guid id);
    }
}
