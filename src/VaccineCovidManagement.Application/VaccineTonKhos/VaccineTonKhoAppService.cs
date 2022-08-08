using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VaccineCovidManagement.ChiTietNhaps;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace VaccineCovidManagement.VaccineTonKhos
{
    public class VaccineTonKhoAppService : ApplicationService, IVaccineTonKhoAppService
    {
        private readonly IVaccineTonKhoRepository _vaccineTonKhoRepository;
        private readonly IChiTietNhapRepository _chiTietNhapRepository;

        public VaccineTonKhoAppService(
            IVaccineTonKhoRepository vaccineTonKhoRepository,
            IChiTietNhapRepository chiTietNhapRepository)
        {
            _vaccineTonKhoRepository = vaccineTonKhoRepository;
            _chiTietNhapRepository = chiTietNhapRepository;
        }

        public async Task<PagedResultDto<VaccineTonKhoDto>> GetListAsync(GetVaccineTonKhoInput input)
        {
            if (input.Sorting.IsNullOrWhiteSpace())
            {
                input.Sorting = nameof(VaccineTonKho.CreationTime);
            }
            var vaccine = await _vaccineTonKhoRepository.GetListAsync(
                    input.SkipCount,
                    input.MaxResultCount,
                    input.Sorting,
                    input.Filter
                );
            var vaccinedto = ObjectMapper.Map<List<VaccineTonKho>, List<VaccineTonKhoDto>>(vaccine);
            var count = await _vaccineTonKhoRepository.GetCountAsync();
            var stt = 1;
            foreach (var item in vaccinedto)
            {
                item.Stt = stt;
                stt++;
                var tenvaccine = await _chiTietNhapRepository.FindAsync(item.ChiTietNhapId);
                item.TenVaccineTonKho = tenvaccine.TenVaccineSX;
                item.SoLuongTonKho = item.SoLuongTonKho + tenvaccine.SoLuongNhap;
            }
            return new PagedResultDto<VaccineTonKhoDto>(
                    count,
                    vaccinedto);
        }

        public async Task<VaccineTonKhoDto> GetVaccineTonKhoAsync(Guid id)
        {
            var vaccinetk = await _vaccineTonKhoRepository.FindAsync(id);
            return ObjectMapper.Map<VaccineTonKho, VaccineTonKhoDto>(vaccinetk);
        }

        public async Task<VaccineTonKhoDto> CreateAsync(CreateUpdateVaccineTonKhoDto input)
        {
            var createvaccine = ObjectMapper.Map<CreateUpdateVaccineTonKhoDto, VaccineTonKho>(input);
            await _vaccineTonKhoRepository.InsertAsync(createvaccine);
            return ObjectMapper.Map<VaccineTonKho, VaccineTonKhoDto>(createvaccine);
        }

        public async Task<VaccineTonKhoDto> UpdateAsync(Guid id, CreateUpdateVaccineTonKhoDto input)
        {
            var updatevaccine = await _vaccineTonKhoRepository.FindAsync(id);
            updatevaccine.ChiTietNhapId = input.ChiTietNhapId;
            updatevaccine.SoLuongTonKho = input.SoLuongTonKho;
            await _vaccineTonKhoRepository.UpdateAsync(updatevaccine);
            return ObjectMapper.Map<VaccineTonKho, VaccineTonKhoDto>(updatevaccine);
        }
    }
}
