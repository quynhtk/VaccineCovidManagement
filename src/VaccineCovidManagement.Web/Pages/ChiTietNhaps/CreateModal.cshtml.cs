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
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

namespace VaccineCovidManagement.Web.Pages.ChiTietNhaps
{
    public class CreateModalModel : VaccineCovidManagementPageModel
    {
        private readonly ChiTietNhapAppService _chiTietNhapAppService;
        private readonly NhaSanXuatAppService _nhaSanXuatAppService;

        public CreateModalModel(
            ChiTietNhapAppService chiTietNhapAppService,
            NhaSanXuatAppService nhaSanXuatAppService)
        {
            _chiTietNhapAppService = chiTietNhapAppService;
            _nhaSanXuatAppService = nhaSanXuatAppService;
        }

        [BindProperty]
        public CreateChiTietNhapViewModal CreateChiTietNhaps { get; set; }
        [BindProperty]
        public List<SelectListItem> NhaSanXuats { get; set; }

        public async void OnGet()
        {
            CreateChiTietNhaps = new CreateChiTietNhapViewModal();
            var nhaSanXuatLookup = await _chiTietNhapAppService.GetNhaSanXuatLookupAsync();
            NhaSanXuats = nhaSanXuatLookup.Items
                .Select(n => new SelectListItem(n.TenNhaSX, n.Id.ToString()))
                .ToList();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (CreateChiTietNhaps.SoLuongNhap > 0)
            {
                var createNhaSanXuat = new CreateUpdateNhaSanXuatDto();
                var vaccineSXDto = await _nhaSanXuatAppService.GetNhaSanXuatAsync(CreateChiTietNhaps.NhaSxID);
                createNhaSanXuat.TenNhaSX = vaccineSXDto.TenNhaSX;
                createNhaSanXuat.TenVaccineSX = vaccineSXDto.TenVaccineSX;

                CreateChiTietNhaps.HanSuDung += " Tháng";
/*
                var createVaccine = new CreateUpdateVaccineDto();
                createVaccine.SoLuongTonKho = createVaccine.SoLuongTonKho + NhapVaccine.SLNhap;
                await _vaccineAppService.UpdateAsync(vaccineSXDto.Id, createVaccine);
*/
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
            public Guid NhaSxID { get; set; }/*
            [DisplayName("Tên Vaccine sản xuất")]
            public string TenVaccineSX { get; set; }*/
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
