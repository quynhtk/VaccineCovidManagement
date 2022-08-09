using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using VaccineCovidManagement.ChiTietNhaps;
using VaccineCovidManagement.NhaSanXuats;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

namespace VaccineCovidManagement.Web.Pages.ChiTietNhaps
{
    public class EditModalModel : VaccineCovidManagementPageModel
    {
        private readonly ChiTietNhapAppService _chiTietNhapAppService;
        private readonly NhaSanXuatAppService _nhaSanXuatAppService;

        public EditModalModel(
            ChiTietNhapAppService chiTietNhapAppService,
            NhaSanXuatAppService nhaSanXuatAppService)
        {
            _chiTietNhapAppService = chiTietNhapAppService;
            _nhaSanXuatAppService = nhaSanXuatAppService;
        }

        [BindProperty]
        public EditChiTietNhapViewModal EditChiTietNhaps { get; set; }
        [BindProperty]
        public List<SelectListItem> NhaSanXuats { get; set; }
        public async Task OnGetAsync(Guid id)
        {
            var chitietnhap = await _chiTietNhapAppService.GetChiTietNhapAsync(id);
            EditChiTietNhaps = ObjectMapper.Map<ChiTietNhapDto, EditChiTietNhapViewModal>(chitietnhap);
            var nhaSanXuatLookup = await _chiTietNhapAppService.GetNhaSanXuatLookupAsync();
            NhaSanXuats = nhaSanXuatLookup.Items
                .Select(n => new SelectListItem(n.TenNhaSX, n.Id.ToString()))
                .ToList();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (EditChiTietNhaps.SoLuongNhap > 0)
            {
                var createNhaSanXuat = new CreateUpdateNhaSanXuatDto();
                var vaccineSXDto = await _nhaSanXuatAppService.GetNhaSanXuatAsync(EditChiTietNhaps.NhaSxID);
                createNhaSanXuat.TenNhaSX = vaccineSXDto.TenNhaSX;
                createNhaSanXuat.TenVaccineSX = vaccineSXDto.TenVaccineSX;

                EditChiTietNhaps.HanSuDung += " Tháng";

                var chitietNhapVaccineDto = ObjectMapper.Map<EditChiTietNhapViewModal, CreateUpdateChiTietNhapDto>(EditChiTietNhaps);
                await _chiTietNhapAppService.UpdateAsync(EditChiTietNhaps.Id, chitietNhapVaccineDto);
            }
            else
            {
                throw new UserFriendlyException(L["Số lượng Vaccine nhập phải lớn hơn 0"]);
            }
            return RedirectToAction("Index", "VaccineTonKhos");
        }

        public class EditChiTietNhapViewModal
        {
            [HiddenInput]
            public Guid Id { get; set; }
            [SelectItems(nameof(NhaSanXuats))]
            [DisplayName("Nhà sản xuất")]
            public Guid NhaSxID { get; set; }/*
            [DisplayName("Tên Vaccine sản xuất")]
            public string TenVaccineSX { get; set; }*/
            [DisplayName("Ngày sản xuất")]
            public DateTime NgaySx { get; set; } = DateTime.Now;
            [DisplayName("Hạn sử dụng")]
            public string HanSuDung { get; set; }
            [DisplayName("Số Lượng Nhập")]
            public int SoLuongNhap { get; set; }
        }
    }
}
