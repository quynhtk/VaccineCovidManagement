using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using VaccineCovidManagement.DonViYTes;
using Volo.Abp;

namespace VaccineCovidManagement.Web.Pages.DonViYTes
{
    public class EditModalModel : VaccineCovidManagementPageModel
    {
        private readonly DonViYTeAppService _donViYTeAppService;

        public EditModalModel(DonViYTeAppService donViYTeAppService)
        {
            _donViYTeAppService = donViYTeAppService;
        }

        [BindProperty]
        public EditDonViYTeViewModal EditDonViYTes { get; set; }
        public async Task OnGetAsync(Guid id)
        {
            var editdvyt = await _donViYTeAppService.GetDonViYTeAsync(id);
            EditDonViYTes = ObjectMapper.Map<DonViYTeDto, EditDonViYTeViewModal>(editdvyt);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _donViYTeAppService.UpdateAsync(
                EditDonViYTes.Id,
                ObjectMapper.Map<EditDonViYTeViewModal, CreateUpdateDonViYTeDto>(EditDonViYTes));
            return NoContent();
        }

        public class EditDonViYTeViewModal
        {
            [HiddenInput]
            public Guid Id { get; set; }
            [DisplayName("Tên Đơn vị Y tế")]
            public string TenDonViYTe { get; set; }
            [DisplayName("Địa chỉ")]
            public string DiaChi { get; set; }
            [DisplayName("Số điện thoại")]
            [RegularExpression("[0-9]{11}")]
            public string SDT { get; set; }
        }
    }
}
