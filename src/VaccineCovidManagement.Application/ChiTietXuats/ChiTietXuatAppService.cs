using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VaccineCovidManagement.DonViYTes;
using VaccineCovidManagement.VaccineTonKhos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace VaccineCovidManagement.ChiTietXuats
{
    public class ChiTietXuatAppService : ApplicationService, IChiTietXuatAppService
    {
        private readonly IChiTietXuatRepository _chiTietXuatRepository;
        private readonly IDonViYTeRepository _donViYTeRepository;
        private readonly IVaccineTonKhoRepository _vaccineTonKhoRepository;

        public ChiTietXuatAppService(
            IChiTietXuatRepository chiTietXuatRepository,
            IDonViYTeRepository donViYTeRepository,
            IVaccineTonKhoRepository vaccineTonKhoRepository)
        {
            _chiTietXuatRepository = chiTietXuatRepository;
            _donViYTeRepository = donViYTeRepository;
            _vaccineTonKhoRepository = vaccineTonKhoRepository;
        }

        public async Task<PagedResultDto<ChiTietXuatDto>> GetListAsync(GetChiTietXuatInput input)
        {
            if (input.Sorting.IsNullOrWhiteSpace())
            {
                input.Sorting = nameof(ChiTietXuat.CreationTime);
            }
            var chiTietXuat = await _chiTietXuatRepository.GetListAsync(
                    input.SkipCount,
                    input.MaxResultCount,
                    input.Sorting,
                    input.Filter
                );
            var chiTietXuatDto = ObjectMapper.Map<List<ChiTietXuat>, List<ChiTietXuatDto>>(chiTietXuat);
            var stt = 1;
            foreach (var item in chiTietXuatDto)
            {
                item.Stt = stt++;
                var donViYTe = await _donViYTeRepository.FindAsync(item.DonViID);
                item.TenDonViYTe = donViYTe.TenDonViYTe;
                var vaccinetk = await _vaccineTonKhoRepository.FindAsync(item.VaccineTonKhoID);
                item.TenVaccineXuat = vaccinetk.TenVaccineTonKho;
            }
            var count = await _chiTietXuatRepository.GetCountAsync();
            return new PagedResultDto<ChiTietXuatDto>(
                    count,
                    chiTietXuatDto);
        }

        public async Task<ListResultDto<GetDonViYTeLookup>> GetDonViYTeLookup()
        {
            var donviyte = await _donViYTeRepository.GetListAsync();
            var donviyteDtos = ObjectMapper.Map<List<DonViYTe>, List<GetDonViYTeLookup>>(donviyte);
            return new ListResultDto<GetDonViYTeLookup>(
                    donviyteDtos);
        }

        public async Task<ListResultDto<GetVaccineTonKhoLookup>> GetVaccineTonKhoLookup()
        {
            var vaccinetk = await _vaccineTonKhoRepository.GetListAsync();
            var vaccineTKDtos = ObjectMapper.Map<List<VaccineTonKho>, List<GetVaccineTonKhoLookup>>(vaccinetk);
            return new ListResultDto<GetVaccineTonKhoLookup>(
                    vaccineTKDtos);
        }

        public async Task<ChiTietXuatDto> CreateAsync(CreateUpdateChiTietXuatDto input)
        {
            var chiTietXuat = ObjectMapper.Map<CreateUpdateChiTietXuatDto, ChiTietXuat>(input);
            await _chiTietXuatRepository.InsertAsync(chiTietXuat);
            return ObjectMapper.Map<ChiTietXuat, ChiTietXuatDto>(chiTietXuat);
        }

        public async Task<ChiTietXuatDto> UpdateAsync(Guid id, CreateUpdateChiTietXuatDto input)
        {
            var chiTietXuat = await _chiTietXuatRepository.FindAsync(id);
            chiTietXuat.DonViID = input.DonViID;
            chiTietXuat.TenDonViYTe = input.TenDonViYTe;
            chiTietXuat.VaccineTonKhoID = input.VaccineTonKhoID;
            chiTietXuat.TenVaccineXuat = input.TenVaccineXuat;
            chiTietXuat.SoLuongXuat = input.SoLuongXuat;
            await _chiTietXuatRepository.UpdateAsync(chiTietXuat);
            return ObjectMapper.Map<ChiTietXuat, ChiTietXuatDto>(chiTietXuat);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var chitietxuat = await _chiTietXuatRepository.FindAsync(id);
            /*
            var vaccine = await _vaccineTonKhoRepository.FindVaccineTonKhoByIdAsync(id);
            if (vaccine != null)
            {
                vaccine.SoLuongTonKho = vaccine.SoLuongTonKho - chitietnhap.SoLuongNhap;
                await _chiTietNhapRepository.DeleteAsync(chitietnhap);
                return true;
            }*/

            await _chiTietXuatRepository.DeleteAsync(chitietxuat);
            return true;
        }
    }
}
