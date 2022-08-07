using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace VaccineCovidManagement.ChiTietNhaps
{
    public interface IChiTietNhapRepository : IRepository<ChiTietNhap, Guid>
    {
        Task<List<ChiTietNhap>> GetListAsync(
                int skipCout,
                int maxResultCount,
                string sorting,
                string filter
            );
        Task<ChiTietNhap> FindByIdNoiSanXuatAsync(Guid id);
    }
}
