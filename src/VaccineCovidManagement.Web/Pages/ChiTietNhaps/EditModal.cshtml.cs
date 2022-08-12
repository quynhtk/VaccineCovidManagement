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
        public ValueOld valueOld { get; set; }
        [BindProperty]
        public EditChiTietNhapViewModal EditChiTietNhaps { get; set; }
        [BindProperty]
        public List<SelectListItem> NhaSanXuats { get; set; }
        [BindProperty]
        public List<SelectListItem> Vaccines { get; set; }
        
        public async Task OnGetAsync(Guid id)
        {
            valueOld = new ValueOld();

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

            valueOld.VaccineIDOld = EditChiTietNhaps.VaccineTonKhoID;
            valueOld.SoLuongTonKhoOld = EditChiTietNhaps.SoLuongNhap;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var checkId = (valueOld.VaccineIDOld == EditChiTietNhaps.VaccineTonKhoID);
            var resultChangeSoLuong = EditChiTietNhaps.SoLuongNhap - valueOld.SoLuongTonKhoOld;
            var createVaccine = new CreateUpdateVaccineTonKhoDto();
            var createNhaSanXuat = new CreateUpdateNhaSanXuatDto();
            var nhaSXDto = await _nhaSanXuatAppService.GetNhaSanXuatAsync(EditChiTietNhaps.NhaSxID);
            if (EditChiTietNhaps.SoLuongNhap > 0)
            {
                switch (checkId)
                {
                    case true:
                        var vaccine = await _vaccineTonKhoAppService.GetVaccineTonKhoAsync(EditChiTietNhaps.VaccineTonKhoID);
                        vaccine.SoLuongTonKho += resultChangeSoLuong;
                        createVaccine.TenVaccineTonKho = vaccine.TenVaccineTonKho;
                        createVaccine.SoLuongTonKho = vaccine.SoLuongTonKho;
                        await _vaccineTonKhoAppService.UpdateAsync(vaccine.Id,createVaccine);
                        break;
                    case false:
                        var quanLyVaccineOld = await _vaccineTonKhoAppService.GetVaccineTonKhoAsync(valueOld.VaccineIDOld);
                        createVaccine.TenVaccineTonKho = quanLyVaccineOld.TenVaccineTonKho;
                        createVaccine.SoLuongTonKho = quanLyVaccineOld.SoLuongTonKho - valueOld.SoLuongTonKhoOld;
                        await _vaccineTonKhoAppService.UpdateAsync(quanLyVaccineOld.Id, createVaccine);

                        var quanLyVaccineNew = await _vaccineTonKhoAppService.GetVaccineTonKhoAsync(EditChiTietNhaps.VaccineTonKhoID);
                        quanLyVaccineNew.SoLuongTonKho += EditChiTietNhaps.SoLuongNhap;
                        createVaccine.TenVaccineTonKho = quanLyVaccineNew.TenVaccineTonKho;
                        createVaccine.SoLuongTonKho = quanLyVaccineNew.SoLuongTonKho;
                        await _vaccineTonKhoAppService.UpdateAsync(quanLyVaccineNew.Id, createVaccine);

                        break;
                }
                createNhaSanXuat.TenNhaSX = nhaSXDto.TenNhaSX;
                if (EditChiTietNhaps.HanSuDung <= 0)
                {
                    throw new UserFriendlyException(L["Hạn sử dụng phải lớn hơn 0 tháng"]);
                }

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
            public int HanSuDung { get; set; }
            [DisplayName("Số Lượng Nhập")]
            public int SoLuongNhap { get; set; }
        }

        public class ValueOld
        {
            [HiddenInput]
            public Guid VaccineIDOld { get; set; }
            [HiddenInput]
            public int SoLuongTonKhoOld { get; set; }
        }
    }
}
