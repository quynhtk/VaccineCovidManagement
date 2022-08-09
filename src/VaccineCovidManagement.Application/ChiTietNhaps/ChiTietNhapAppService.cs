using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VaccineCovidManagement.NhaSanXuats;
using VaccineCovidManagement.VaccineTonKhos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace VaccineCovidManagement.ChiTietNhaps
{
    public class ChiTietNhapAppService : ApplicationService, IChiTietNhapAppService
    {
        private readonly IChiTietNhapRepository _chiTietNhapRepository;
        private readonly INhaSanXuatRepository _nhaSanXuatRepository;
        private readonly IVaccineTonKhoRepository _vaccineTonKhoRepository;

        public ChiTietNhapAppService(
            IChiTietNhapRepository chiTietNhapRepository,
            INhaSanXuatRepository nhaSanXuatRepository,
            IVaccineTonKhoRepository vaccineTonKhoRepository)
        {
            _chiTietNhapRepository = chiTietNhapRepository;
            _nhaSanXuatRepository = nhaSanXuatRepository;
            _vaccineTonKhoRepository = vaccineTonKhoRepository;
        }

        public async Task<PagedResultDto<ChiTietNhapDto>> GetListAsync(GetChiTietNhapInput input)
        {
            if (input.Sorting.IsNullOrWhiteSpace())
            {
                input.Sorting = nameof(ChiTietNhap.TenNhaSX);
            }
            var chiTietNhap = await _chiTietNhapRepository.GetListAsync(
                    input.SkipCount,
                    input.MaxResultCount,
                    input.Sorting,
                    input.Filter
                );
            var chiTietNhapDto = ObjectMapper.Map<List<ChiTietNhap>, List<ChiTietNhapDto>>(chiTietNhap);
            var stt = 1;
            foreach (var item in chiTietNhapDto)
            {
                item.Stt = stt++;
                var noiSX = await _nhaSanXuatRepository.FindAsync(item.NhaSxID);
                item.TenNhaSX = noiSX.TenNhaSX;
                item.TenVaccineSX = noiSX.TenVaccineSX;
            }
            var count = await _chiTietNhapRepository.GetCountAsync();
            return new PagedResultDto<ChiTietNhapDto>(
                    count,
                    chiTietNhapDto);
        }

        public async Task<ChiTietNhapDto> GetChiTietNhapAsync(Guid id)
        {
            var chitietnhap = await _chiTietNhapRepository.FindAsync(id);
            return ObjectMapper.Map<ChiTietNhap, ChiTietNhapDto>(chitietnhap);
        }

        public async Task<ListResultDto<GetNhaSanXuatLookup>> GetNhaSanXuatLookupAsync()
        {
            var nhaSX = await _nhaSanXuatRepository.GetListAsync();
            var nhaSanXuatDtos = ObjectMapper.Map<List<NhaSanXuat>, List<GetNhaSanXuatLookup>>(nhaSX);
            return new ListResultDto<GetNhaSanXuatLookup>(
                    nhaSanXuatDtos);
        }

        public async Task<ChiTietNhapDto> CreateAsync(CreateUpdateChiTietNhapDto input)
        {
            var chiTietNhap = ObjectMapper.Map<CreateUpdateChiTietNhapDto, ChiTietNhap>(input);
            await _chiTietNhapRepository.InsertAsync(chiTietNhap);
            return ObjectMapper.Map<ChiTietNhap, ChiTietNhapDto>(chiTietNhap);
        }

        public async Task<ChiTietNhapDto> UpdateAsync(Guid id, CreateUpdateChiTietNhapDto input)
        {
            var chiTietNhap = await _chiTietNhapRepository.FindAsync(id);
            chiTietNhap.NhaSxID = input.NhaSxID;
            chiTietNhap.TenNhaSX = input.TenNhaSX;
            chiTietNhap.TenVaccineSX = input.TenVaccineSX;
            chiTietNhap.NgaySx = input.NgaySx;
            chiTietNhap.HanSuDung = input.HanSuDung;
            chiTietNhap.SoLuongNhap = input.SoLuongNhap;
            await _chiTietNhapRepository.UpdateAsync(chiTietNhap);
            return ObjectMapper.Map<ChiTietNhap, ChiTietNhapDto>(chiTietNhap);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var chitietnhap = await _chiTietNhapRepository.FindAsync(id);
            var vaccine = await _vaccineTonKhoRepository.FindVaccineTonKhoByIdAsync(id);
            if (vaccine != null)
            {
                return false;
            }
            vaccine.SoLuongTonKho = vaccine.SoLuongTonKho - chitietnhap.SoLuongNhap;
            await _chiTietNhapRepository.DeleteAsync(chitietnhap);
            return true;
        }
    }
}
