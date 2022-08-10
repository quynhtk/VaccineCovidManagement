using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace VaccineCovidManagement.ChiTietNhaps
{
    public interface IChiTietNhapAppService : IApplicationService
    {
        Task<PagedResultDto<ChiTietNhapDto>> GetListAsync(GetChiTietNhapInput input);
        Task<ChiTietNhapDto> GetChiTietNhapAsync(Guid id);
        Task<ListResultDto<GetNhaSanXuatLookup>> GetNhaSanXuatLookupAsync();
        Task<ListResultDto<GetVaccineTKLookup>> GetVaccineTonKhoLookup();
        Task<ChiTietNhapDto> CreateAsync(CreateUpdateChiTietNhapDto input);
        Task<ChiTietNhapDto> UpdateAsync(Guid id, CreateUpdateChiTietNhapDto input);
        Task<bool> DeleteAsync(Guid id);
    }
}
