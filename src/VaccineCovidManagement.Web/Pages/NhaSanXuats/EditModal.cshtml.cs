using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using VaccineCovidManagement.NhaSanXuats;
using Volo.Abp;

namespace VaccineCovidManagement.Web.Pages.NhaSanXuats
{
    public class EditModalModel : VaccineCovidManagementPageModel
    {
        private readonly NhaSanXuatAppService _nhaSanXuatAppService;

        public EditModalModel(NhaSanXuatAppService nhaSanXuatAppService)
        {
            _nhaSanXuatAppService = nhaSanXuatAppService;
        }

        [BindProperty]
        public EditNhaSanXuatViewModal EditNhaSanXuats { get; set; }
        public async Task OnGetAsync(Guid Id)
        {
            var editnsx = await _nhaSanXuatAppService.GetNhaSanXuatAsync(Id);
            EditNhaSanXuats = ObjectMapper.Map<NhaSanXuatDto, EditNhaSanXuatViewModal>(editnsx);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _nhaSanXuatAppService.UpdateAsync(
                EditNhaSanXuats.Id,
                ObjectMapper.Map<EditNhaSanXuatViewModal, CreateUpdateNhaSanXuatDto>(EditNhaSanXuats));
            return NoContent();
        }

        public class EditNhaSanXuatViewModal
        {
            [HiddenInput]
            public Guid Id { get; set; }
            [DisplayName("Tên Nhà sản xuất")]
            public string TenNhaSX { get; set; }
            [DisplayName("Địa chỉ")]
            public string Diachi { get; set; }
            [DisplayName("Số điện thoại")]
            [RegularExpression("[0-9]{11}")]
            public string SDT { get; set; }
            [EmailAddress]
            public string Email { get; set; }
        }
    }
}
