using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace VaccineCovidManagement.DonViYTes
{
    public interface IDonViYTeAppService : IApplicationService
    {
        Task<PagedResultDto<DonViYTeDto>> GetListAsync(GetDonViYTeInput input);
        Task<DonViYTeDto> GetDonViYTeAsync(Guid id);
        Task<DonViYTeDto> CreateAsync(CreateUpdateDonViYTeDto input);
        Task<DonViYTeDto> UpdateAsync(Guid id, CreateUpdateDonViYTeDto input);
        Task<bool> DeleteAsync(Guid id);
        Task<bool> CheckDonViYTeExist(string donViYTe);
    }
}
