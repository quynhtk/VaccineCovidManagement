using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace VaccineCovidManagement.NhaSanXuats
{
    public interface INhaSanXuatRepository : IRepository<NhaSanXuat, Guid>
    {
        Task<List<NhaSanXuat>> GetListAsync(
                int skipCount,
                int maxResultCount,
                string sorting,
                string filter
            );
        Task<NhaSanXuat> FindByNoiSanXuatAsync(string noiSX);
        Task<NhaSanXuat> FindNhaSanXuatByIdAsync(Guid id);
    }
}
