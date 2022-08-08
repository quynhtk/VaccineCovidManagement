using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace VaccineCovidManagement.VaccineTonKhos
{
    public interface IVaccineTonKhoAppService : IApplicationService
    {
        Task<PagedResultDto<VaccineTonKhoDto>> GetListAsync(GetVaccineTonKhoInput input);
        Task<VaccineTonKhoDto> GetVaccineTonKhoAsync(Guid id);
        Task<VaccineTonKhoDto> CreateAsync(CreateUpdateVaccineTonKhoDto input);
        Task<VaccineTonKhoDto> UpdateAsync(Guid id, CreateUpdateVaccineTonKhoDto input);
    }
}
