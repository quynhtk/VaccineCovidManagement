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
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

namespace VaccineCovidManagement.Web.Pages.VaccineTonKhos
{
    public class CreateModalModel : VaccineCovidManagementPageModel
    {
        private readonly VaccineTonKhoAppService _vaccineTonKhoAppService;

        public CreateModalModel(
            VaccineTonKhoAppService vaccineTonKhoAppService)
        {
            _vaccineTonKhoAppService = vaccineTonKhoAppService;
        }

        [BindProperty]
        public CreateVaccineTonKhoViewModal CreateVaccineTonKhos { get; set; }

        public void OnGet()
        {
            CreateVaccineTonKhos = new CreateVaccineTonKhoViewModal();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var tenVaccineExist = await _vaccineTonKhoAppService.CheckTenVaccineExist(CreateVaccineTonKhos.TenVaccineTonKho);
            if (tenVaccineExist == false)
            {
                await _vaccineTonKhoAppService.CreateAsync(
                    ObjectMapper.Map<CreateVaccineTonKhoViewModal, CreateUpdateVaccineTonKhoDto>(CreateVaccineTonKhos));
            }
            else
            {
                throw new UserFriendlyException(L["Vaccine " + CreateVaccineTonKhos.TenVaccineTonKho + " đã tồn tại"]);
            }
            return NoContent();
        }

        public class CreateVaccineTonKhoViewModal
        {
            [Required]
            [DisplayName("Tên Vaccine")]
            public string TenVaccineTonKho { get; set; }
            [HiddenInput]
            [DisplayName("Số Lượng tồn kho")]
            public int SoLuongTonKho { get; set; } = 0;
        }
    }
}
