using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace VaccineCovidManagement.NhaSanXuats
{
    public interface INhaSanXuatAppService : IApplicationService
    {
        Task<PagedResultDto<NhaSanXuatDto>> GetListAsync(GetNhaSanXuatInput input);
        Task<NhaSanXuatDto> GetNhaSanXuatAsync(Guid Id);
        Task<NhaSanXuatDto> CreateAsync(CreateUpdateNhaSanXuatDto input);
        Task<NhaSanXuatDto> UpdateAsync(Guid id, CreateUpdateNhaSanXuatDto input);
        Task<bool> DeleteAsync(Guid id);
        Task<bool> CheckTenNhaSanXuatExist(string noiSx);
        Task<bool> CheckTenVaccineSanXuatExist(string tenVaccineSx);
    }
}
