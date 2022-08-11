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
using VaccineCovidManagement.NhaSanXuats;
using VaccineCovidManagement.VaccineTonKhos;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

namespace VaccineCovidManagement.Web.Pages.ChiTietNhaps
{
    public class CreateModalModel : VaccineCovidManagementPageModel
    {
        private readonly ChiTietNhapAppService _chiTietNhapAppService;
        private readonly NhaSanXuatAppService _nhaSanXuatAppService;
        private readonly VaccineTonKhoAppService _vaccineTonKhoAppService;

        public CreateModalModel(
            ChiTietNhapAppService chiTietNhapAppService,
            NhaSanXuatAppService nhaSanXuatAppService,
            VaccineTonKhoAppService vaccineTonKhoAppService)
        {
            _chiTietNhapAppService = chiTietNhapAppService;
            _nhaSanXuatAppService = nhaSanXuatAppService;
            _vaccineTonKhoAppService = vaccineTonKhoAppService;
        }

        [BindProperty]
        public CreateChiTietNhapViewModal CreateChiTietNhaps { get; set; }
        [BindProperty]
        public List<SelectListItem> NhaSanXuats { get; set; }
        [BindProperty]
        public List<SelectListItem> Vaccines { get; set; }

        public async void OnGet()
        {
            CreateChiTietNhaps = new CreateChiTietNhapViewModal();
            var nhaSanXuatLookup = await _chiTietNhapAppService.GetNhaSanXuatLookupAsync();
            NhaSanXuats = nhaSanXuatLookup.Items
                .Select(n => new SelectListItem(n.TenNhaSX, n.Id.ToString()))
                .ToList();

            var vaccineLookup = await _chiTietNhapAppService.GetVaccineTonKhoLookup();
            Vaccines = vaccineLookup.Items
                .Select(n => new SelectListItem(n.TenVaccineTonKho, n.Id.ToString()))
                .ToList();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (CreateChiTietNhaps.SoLuongNhap > 0)
            {
                var createNhaSanXuat = new CreateUpdateNhaSanXuatDto();
                var nhaSXDto = await _nhaSanXuatAppService.GetNhaSanXuatAsync(CreateChiTietNhaps.NhaSxID);
                createNhaSanXuat.TenNhaSX = nhaSXDto.TenNhaSX;

                CreateChiTietNhaps.HanSuDung += " Tháng";
                if (CreateChiTietNhaps.HanSuDung == "0 Tháng")
                {
                    throw new UserFriendlyException(L["Hạn sử dụng phải lớn hơn 0"]);
                }

                var createVaccine = new CreateUpdateVaccineTonKhoDto();
                var vaccineSXDto = await _vaccineTonKhoAppService.GetVaccineTonKhoAsync(CreateChiTietNhaps.VaccineTonKhoID);
                createVaccine.TenVaccineTonKho = vaccineSXDto.TenVaccineTonKho;
                createVaccine.SoLuongTonKho = vaccineSXDto.SoLuongTonKho + CreateChiTietNhaps.SoLuongNhap;
                await _vaccineTonKhoAppService.UpdateAsync(vaccineSXDto.Id, createVaccine);

                var chitietNhapVaccineDto = ObjectMapper.Map<CreateChiTietNhapViewModal, CreateUpdateChiTietNhapDto>(CreateChiTietNhaps);
                await _chiTietNhapAppService.CreateAsync(chitietNhapVaccineDto);
            }
            else
            {
                throw new UserFriendlyException(L["Số lượng Vaccine nhập phải lớn hơn 0"]);
            }
            return RedirectToAction("Index", "VaccineTonKhos");
        }

        public class CreateChiTietNhapViewModal
        {
            [Required]
            [SelectItems(nameof(NhaSanXuats))]
            [DisplayName("Nhà sản xuất")]
            public Guid NhaSxID { get; set; }
            [Required]
            [SelectItems(nameof(Vaccines))]
            [DisplayName("Tên Vaccine")]
            public Guid VaccineTonKhoID { get; set; }
            [Required]
            [DisplayName("Ngày sản xuất")]
            public DateTime NgaySx { get; set; } = DateTime.Now;
            [Required]
            [DisplayName("Hạn sử dụng")]
            public string HanSuDung { get; set; }
            [Required]
            [DisplayName("Số Lượng Nhập")]
            public int SoLuongNhap { get; set; }
        }
    }
}
