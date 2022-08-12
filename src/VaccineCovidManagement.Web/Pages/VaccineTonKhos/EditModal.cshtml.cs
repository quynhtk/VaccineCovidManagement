using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using VaccineCovidManagement.VaccineTonKhos;
using Volo.Abp;

namespace VaccineCovidManagement.Web.Pages.VaccineTonKhos
{
    public class EditModalModel : VaccineCovidManagementPageModel
    {
        private readonly VaccineTonKhoAppService _vaccineTonKhoAppService;

        public EditModalModel(
            VaccineTonKhoAppService vaccineTonKhoAppService)
        {
            _vaccineTonKhoAppService = vaccineTonKhoAppService;
        }

        [BindProperty]
        public EditVaccineTonKhoViewModal EditVaccineTonKhos { get; set; }
        public async Task OnGetAsync(Guid Id)
        {
            var editvaccine = await _vaccineTonKhoAppService.GetVaccineTonKhoAsync(Id);
            EditVaccineTonKhos = ObjectMapper.Map<VaccineTonKhoDto, EditVaccineTonKhoViewModal>(editvaccine);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _vaccineTonKhoAppService.UpdateAsync(
                EditVaccineTonKhos.Id,
                ObjectMapper.Map<EditVaccineTonKhoViewModal, CreateUpdateVaccineTonKhoDto>(EditVaccineTonKhos));
            return NoContent();
        }

        public class EditVaccineTonKhoViewModal
        {
            [HiddenInput]
            public Guid Id { get; set; }
            [DisplayName("Tên Vaccine")]
            public string TenVaccineTonKho { get; set; }
            [HiddenInput]
            [DisplayName("Số Lượng tồn kho")]
            public int SoLuongTonKho { get; set; } = 0;
        }
    }
}
