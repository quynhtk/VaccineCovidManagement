using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace VaccineCovidManagement.DonViYTes
{
    public class DonViYTeAppService : ApplicationService, IDonViYTeAppService
    {
        private readonly IDonViYTeRepository _donViYTeRepository;

        public DonViYTeAppService(IDonViYTeRepository donViYTeRepository)
        {
            _donViYTeRepository = donViYTeRepository;
        }

        public async Task<PagedResultDto<DonViYTeDto>> GetListAsync(GetDonViYTeInput input)
        {
            if (input.Sorting.IsNullOrWhiteSpace())
            {
                input.Sorting = nameof(DonViYTe.TenDonViYTe);
            }
            var donViYTe = await _donViYTeRepository.GetListAsync(
                    input.SkipCount,
                    input.MaxResultCount,
                    input.Sorting,
                    input.Filter
                );
            var donviyteDto = ObjectMapper.Map<List<DonViYTe>, List<DonViYTeDto>>(donViYTe);
            var count = await _donViYTeRepository.GetCountAsync();
            var stt = 1;
            foreach (var item in donviyteDto)
            {
                item.Stt = stt;
                stt++;
            }
            return new PagedResultDto<DonViYTeDto>(
                    count,
                    donviyteDto);
        }

        public async Task<DonViYTeDto> GetDonViYTeAsync(Guid id)
        {
            var donViYTe = await _donViYTeRepository.FindAsync(id);
            return ObjectMapper.Map<DonViYTe, DonViYTeDto>(donViYTe);
        }

        public async Task<DonViYTeDto> CreateAsync(CreateUpdateDonViYTeDto input)
        {
            var donviyte = ObjectMapper.Map<CreateUpdateDonViYTeDto, DonViYTe>(input);
            await _donViYTeRepository.InsertAsync(donviyte);
            return ObjectMapper.Map<DonViYTe, DonViYTeDto>(donviyte);
        }

        public async Task<DonViYTeDto> UpdateAsync(Guid id, CreateUpdateDonViYTeDto input)
        {
            var donviyte = await _donViYTeRepository.FindAsync(id);
            donviyte.TenDonViYTe = input.TenDonViYTe;
            donviyte.SDT = input.SDT;
            donviyte.DiaChi = input.DiaChi;
            await _donViYTeRepository.UpdateAsync(donviyte);
            return ObjectMapper.Map<DonViYTe, DonViYTeDto>(donviyte);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var donviyte = await _donViYTeRepository.FindAsync(id);
            await _donViYTeRepository.DeleteAsync(donviyte);
            return true;
        }
        
        public async Task<bool> CheckDonViYTeExist(string donViYTe)
        {
            var tenDonViYTe = await _donViYTeRepository.FindByDonViYTeAsync(donViYTe);
            if(tenDonViYTe != null)
            {
                return true;
            }
            return false;
        }
    }
}
