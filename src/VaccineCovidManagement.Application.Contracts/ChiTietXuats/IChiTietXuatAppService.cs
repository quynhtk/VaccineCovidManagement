using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace VaccineCovidManagement.ChiTietXuats
{
    public interface IChiTietXuatAppService : IApplicationService
    {
        Task<PagedResultDto<ChiTietXuatDto>> GetListAsync(GetChiTietXuatInput input);
        Task<ChiTietXuatDto> CreateAsync(CreateUpdateChiTietXuatDto input);
        Task<ChiTietXuatDto> UpdateAsync(Guid id, CreateUpdateChiTietXuatDto input);
        Task<bool> DeleteAsync(Guid id);
        Task<ListResultDto<GetDonViYTeLookup>> GetDonViYTeLookup();
        Task<ListResultDto<GetVaccineTonKhoLookup>> GetVaccineTonKhoLookup();
    }
}
