using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using VaccineCovidManagement.ChiTietNhaps;
using VaccineCovidManagement.VaccineTonKhos;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

namespace VaccineCovidManagement.Web.Pages.VaccineTonKhos
{
    public class CreateModalModel : VaccineCovidManagementPageModel
    {
        private readonly VaccineTonKhoAppService _vaccineTonKhoAppService;
        private readonly ChiTietNhapAppService _chiTietNhapAppService;

        public CreateModalModel(
            VaccineTonKhoAppService vaccineTonKhoAppService,
            ChiTietNhapAppService chiTietNhapAppService)
        {
            _vaccineTonKhoAppService = vaccineTonKhoAppService;
            _chiTietNhapAppService = chiTietNhapAppService;
        }

        [BindProperty]
        public CreateVaccineTonKhoViewModal CreateVaccines { get; set; }
        [BindProperty]
        public List<SelectListItem> TenVaccine { get; set; }

        public async void OnGet()
        {
            CreateVaccines = new CreateVaccineTonKhoViewModal();
            var chitietnhapLookup = await _vaccineTonKhoAppService.GetChiTietNhapLookupAsync();
            TenVaccine = chitietnhapLookup.Items
                .Select(n => new SelectListItem(n.TenVaccineSX, n.Id.ToString()))
                .ToList();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var createChitietnhap = new CreateUpdateChiTietNhapDto();
            var vaccineSXDto = await _chiTietNhapAppService.GetChiTietNhapAsync(CreateVaccines.ChiTietNhapId);
            createChitietnhap.TenVaccineSX = vaccineSXDto.TenVaccineSX;

            CreateVaccines.SoLuongTonKho = CreateVaccines.SoLuongTonKho + vaccineSXDto.SoLuongNhap;

            var vaccineTonKhoDto = ObjectMapper.Map<CreateVaccineTonKhoViewModal, CreateUpdateVaccineTonKhoDto>(CreateVaccines);
            await _vaccineTonKhoAppService.CreateAsync(vaccineTonKhoDto);
            return NoContent();
        }

        public class CreateVaccineTonKhoViewModal
        {
            [Required]
            [SelectItems(nameof(TenVaccine))]
            [DisplayName("Tên Vaccine")]
            public Guid ChiTietNhapId { get; set; }
            [HiddenInput]
            [DisplayName("Số Lượng tồn kho")]
            public int SoLuongTonKho { get; set; }
        }
    }
}
