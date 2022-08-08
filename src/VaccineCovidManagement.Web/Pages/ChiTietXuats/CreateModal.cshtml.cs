using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using VaccineCovidManagement.ChiTietXuats;
using VaccineCovidManagement.DonViYTes;
using VaccineCovidManagement.VaccineTonKhos;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

namespace VaccineCovidManagement.Web.Pages.ChiTietXuats
{
    public class CreateModalModel : VaccineCovidManagementPageModel
    {
        private readonly ChiTietXuatAppService _chiTietXuatAppService;
        private readonly DonViYTeAppService _donViYTeAppService;
        private readonly VaccineTonKhoAppService _vaccineTonKhoAppService;

        public CreateModalModel(
            ChiTietXuatAppService chiTietXuatAppService,
            DonViYTeAppService donViYTeAppService,
            VaccineTonKhoAppService vaccineTonKhoAppService)
        {
            _chiTietXuatAppService = chiTietXuatAppService;
            _donViYTeAppService = donViYTeAppService;
            _vaccineTonKhoAppService = vaccineTonKhoAppService;
        }
        [BindProperty]
        public CreateChiTietXuatViewModal CreateChiTietXuat { get; set; }
        [BindProperty]
        public List<SelectListItem> DonViYTes { get; set; }
        [BindProperty]
        public List<SelectListItem> Vaccines { get; set; }
        public async void OnGet()
        {
            CreateChiTietXuat = new CreateChiTietXuatViewModal();
            var donViYTeLookup = await _chiTietXuatAppService.GetDonViYTeLookup();
            DonViYTes = donViYTeLookup.Items
                .Select(d => new SelectListItem(d.TenDonViYTe, d.Id.ToString()))
                .ToList();

            var vaccineLookup = await _chiTietXuatAppService.GetVaccineTonKhoLookup();
            Vaccines = vaccineLookup.Items
                .Select(v => new SelectListItem(v.TenVaccineTonKho, v.Id.ToString()))
                .ToList();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (CreateChiTietXuat.SoLuongXuat > 0)
            {
                var vaccineDto = await _vaccineTonKhoAppService.GetVaccineTonKhoAsync(CreateChiTietXuat.VaccineTonKhoID);
                var VaccineTonKhoDto = new CreateUpdateVaccineTonKhoDto();
                var mess = "Số vaccine " + '"' + vaccineDto.TenVaccineTonKho + '"' + " trong kho còn " + vaccineDto.SoLuongTonKho + " (liều) không đủ để xuất";
                if (vaccineDto.SoLuongTonKho < CreateChiTietXuat.SoLuongXuat)
                {
                    throw new UserFriendlyException(L[mess]);
                }
                VaccineTonKhoDto.SoLuongTonKho = vaccineDto.SoLuongTonKho - CreateChiTietXuat.SoLuongXuat;
                await _vaccineTonKhoAppService.UpdateAsync(vaccineDto.Id, VaccineTonKhoDto);
                var upDateChiTiet = ObjectMapper.Map<CreateChiTietXuatViewModal, CreateUpdateChiTietXuatDto>(CreateChiTietXuat);
                await _chiTietXuatAppService.CreateAsync(upDateChiTiet);
            }
            else
            {
                throw new UserFriendlyException(L["Số lượng Vaccine xuất phải lớn hơn 0"]);
            }
            return NoContent();
        }
        public class CreateChiTietXuatViewModal
        {
            [Required]
            [SelectItems(nameof(DonViYTes))]
            [DisplayName("Đơn vị y tế")]
            public Guid DonViID { get; set; }
            [Required]
            [SelectItems(nameof(Vaccines))]
            [DisplayName("Vaccine")]
            public Guid VaccineTonKhoID { get; set; }
            [Required]
            public int SoLuongXuat { get; set; }
        }
    }
}
