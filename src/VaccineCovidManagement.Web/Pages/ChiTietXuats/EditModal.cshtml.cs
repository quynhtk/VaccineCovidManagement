﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using VaccineCovidManagement.ChiTietXuats;
using VaccineCovidManagement.DonViYTes;
using VaccineCovidManagement.VaccineTonKhos;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

namespace VaccineCovidManagement.Web.Pages.ChiTietXuats
{
    public class EditModalModel : VaccineCovidManagementPageModel
    {
        private readonly ChiTietXuatAppService _chiTietXuatAppService;
        private readonly DonViYTeAppService _donViYTeAppService;
        private readonly VaccineTonKhoAppService _vaccineTonKhoAppService;

        public EditModalModel(
            ChiTietXuatAppService chiTietXuatAppService,
            DonViYTeAppService donViYTeAppService,
            VaccineTonKhoAppService vaccineTonKhoAppService)
        {
            _chiTietXuatAppService = chiTietXuatAppService;
            _donViYTeAppService = donViYTeAppService;
            _vaccineTonKhoAppService = vaccineTonKhoAppService;
        }
        [BindProperty]
        public EditChiTietXuatViewModal EditChiTietXuat { get; set; }
        [BindProperty]
        public List<SelectListItem> DonViYTes { get; set; }
        [BindProperty]
        public List<SelectListItem> Vaccines { get; set; }
        public async Task OnGetAsync(Guid id)
        {
            var chitietxuat = await _chiTietXuatAppService.GetChiTietXuatAsync(id);
            EditChiTietXuat = ObjectMapper.Map<ChiTietXuatDto, EditChiTietXuatViewModal>(chitietxuat);

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
            if (EditChiTietXuat.SoLuongXuat > 0)
            {
                var vaccineDto = await _vaccineTonKhoAppService.GetVaccineTonKhoAsync(EditChiTietXuat.VaccineTonKhoID);
                var VaccineTonKhoDto = new CreateUpdateVaccineTonKhoDto();
                var mess = "Số vaccine " + '"' + vaccineDto.TenVaccineTonKho + '"' + " trong kho còn " + vaccineDto.SoLuongTonKho + " (liều) không đủ để xuất";
                if (vaccineDto.SoLuongTonKho < EditChiTietXuat.SoLuongXuat)
                {
                    throw new UserFriendlyException(L[mess]);
                }
                /*VaccineTonKhoDto.SoLuongTonKho = vaccineDto.SoLuongTonKho - CreateChiTietXuat.SoLuongXuat;
                await _vaccineTonKhoAppService.UpdateAsync(vaccineDto.Id, VaccineTonKhoDto);*/
                var upDateChiTiet = ObjectMapper.Map<EditChiTietXuatViewModal, CreateUpdateChiTietXuatDto>(EditChiTietXuat);
                await _chiTietXuatAppService.CreateAsync(upDateChiTiet);
            }
            else
            {
                throw new UserFriendlyException(L["Số lượng Vaccine xuất phải lớn hơn 0"]);
            }
            return NoContent();
        }
        public class EditChiTietXuatViewModal
        {
            [HiddenInput]
            public Guid Id { get; set; }
            [SelectItems(nameof(DonViYTes))]
            [DisplayName("Đơn vị y tế")]
            public Guid DonViID { get; set; }
            [SelectItems(nameof(Vaccines))]
            [DisplayName("Vaccine")]
            public Guid VaccineTonKhoID { get; set; }
            public int SoLuongXuat { get; set; }
        }
    }
}