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
using VaccineCovidManagement.VaccineTonKhos;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

namespace VaccineCovidManagement.Web.Pages.ChiTietNhaps
{
    public class EditModalModel : VaccineCovidManagementPageModel
    {
        private readonly ChiTietNhapAppService _chiTietNhapAppService;
        private readonly NhaSanXuatAppService _nhaSanXuatAppService;
        private readonly VaccineTonKhoAppService _vaccineTonKhoAppService;

        public EditModalModel(
            ChiTietNhapAppService chiTietNhapAppService,
            NhaSanXuatAppService nhaSanXuatAppService,
            VaccineTonKhoAppService vaccineTonKhoAppService)
        {
            _chiTietNhapAppService = chiTietNhapAppService;
            _nhaSanXuatAppService = nhaSanXuatAppService;
            _vaccineTonKhoAppService = vaccineTonKhoAppService;
        }

        [BindProperty]
        public EditChiTietNhapViewModal EditChiTietNhaps { get; set; }
        [BindProperty]
        public List<SelectListItem> NhaSanXuats { get; set; }
        [BindProperty]
        public List<SelectListItem> Vaccines { get; set; }
        public async Task OnGetAsync(Guid id)
        {
            var chitietnhap = await _chiTietNhapAppService.GetChiTietNhapAsync(id);
            EditChiTietNhaps = ObjectMapper.Map<ChiTietNhapDto, EditChiTietNhapViewModal>(chitietnhap);
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
            if (EditChiTietNhaps.SoLuongNhap > 0)
            {
                var createNhaSanXuat = new CreateUpdateNhaSanXuatDto();
                var nhaSXDto = await _nhaSanXuatAppService.GetNhaSanXuatAsync(EditChiTietNhaps.NhaSxID);
                createNhaSanXuat.TenNhaSX = nhaSXDto.TenNhaSX;

                EditChiTietNhaps.HanSuDung += " Tháng";

                var createVaccine = new CreateUpdateVaccineTonKhoDto();
                var vaccineSXDto = await _vaccineTonKhoAppService.GetVaccineTonKhoAsync(EditChiTietNhaps.VaccineTonKhoID);
                createVaccine.TenVaccineTonKho = vaccineSXDto.TenVaccineTonKho;
                createVaccine.SoLuongTonKho = createVaccine.SoLuongTonKho + EditChiTietNhaps.SoLuongNhap;
                await _vaccineTonKhoAppService.UpdateAsync(vaccineSXDto.Id, createVaccine);

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
            public Guid NhaSxID { get; set; }
            [SelectItems(nameof(Vaccines))]
            [DisplayName("Tên Vaccine")]
            public Guid VaccineTonKhoID { get; set; }
            [DisplayName("Ngày sản xuất")]
            public DateTime NgaySx { get; set; } = DateTime.Now;
            [DisplayName("Hạn sử dụng")]
            public string HanSuDung { get; set; }
            [DisplayName("Số Lượng Nhập")]
            public int SoLuongNhap { get; set; }
        }
    }
}
