using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VaccineCovidManagement.ChiTietNhaps;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace VaccineCovidManagement.NhaSanXuats
{
    public class NhaSanXuatAppService : ApplicationService, INhaSanXuatAppService
    {
        private readonly INhaSanXuatRepository _nhaSanXuatRepository;
        private readonly IChiTietNhapRepository _chiTietNhapRepository;

        public NhaSanXuatAppService(
            INhaSanXuatRepository nhaSanXuatRepository,
            IChiTietNhapRepository chiTietNhapRepository)
        {
            _nhaSanXuatRepository = nhaSanXuatRepository;
            _chiTietNhapRepository = chiTietNhapRepository;
        }

        public async Task<PagedResultDto<NhaSanXuatDto>> GetListAsync(GetNhaSanXuatInput input)
        {
            if (input.Sorting.IsNullOrWhiteSpace())
            {
                input.Sorting = nameof(NhaSanXuat.TenNhaSX);
            }
            var nhaSX = await _nhaSanXuatRepository.GetListAsync(
                    input.SkipCount,
                    input.MaxResultCount,
                    input.Sorting,
                    input.Filter
                );
            var nhasanxuatDto = ObjectMapper.Map<List<NhaSanXuat>, List<NhaSanXuatDto>>(nhaSX);
            var count = await _nhaSanXuatRepository.GetCountAsync();
            var stt = 1;
            foreach (var item in nhasanxuatDto)
            {
                item.Stt = stt;
                stt++;
            }
            return new PagedResultDto<NhaSanXuatDto>(
                    count,
                    nhasanxuatDto);
        }

        public async Task<NhaSanXuatDto> GetNhaSanXuatAsync(Guid Id)
        {
            var nhasanxuat = await _nhaSanXuatRepository.FindAsync(Id);
            return ObjectMapper.Map<NhaSanXuat, NhaSanXuatDto>(nhasanxuat);
        }

        public async Task<NhaSanXuatDto> CreateAsync(CreateUpdateNhaSanXuatDto input)
        {
            var nhaSanXuat = ObjectMapper.Map<CreateUpdateNhaSanXuatDto, NhaSanXuat>(input);
            await _nhaSanXuatRepository.InsertAsync(nhaSanXuat);
            return ObjectMapper.Map<NhaSanXuat, NhaSanXuatDto>(nhaSanXuat);
        }

        public async Task<NhaSanXuatDto> UpdateAsync(Guid id, CreateUpdateNhaSanXuatDto input)
        {
            var nhaSX = await _nhaSanXuatRepository.FindAsync(id);
            nhaSX.TenNhaSX = input.TenNhaSX;
            nhaSX.Diachi = input.Diachi;
            nhaSX.Email = input.Email;
            nhaSX.SDT = input.SDT;
            await _nhaSanXuatRepository.UpdateAsync(nhaSX);
            return ObjectMapper.Map<NhaSanXuat, NhaSanXuatDto>(nhaSX);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var noiSX = await _nhaSanXuatRepository.FindAsync(id);
            var vaccineNhap = await _chiTietNhapRepository.FindByIdNoiSanXuatAsync(id);
            if (vaccineNhap != null)
            {
                return false;
            }
            await _nhaSanXuatRepository.DeleteAsync(noiSX);
            return true;
        }

        public async Task<bool> CheckTenNhaSanXuatExist(string noiSx)
        {
            var tenNhaSanXuatExist = await _nhaSanXuatRepository.FindByNoiSanXuatAsync(noiSx);
            if (tenNhaSanXuatExist != null)
            {
                return true;
            }
            return false;
        }
    }
}
