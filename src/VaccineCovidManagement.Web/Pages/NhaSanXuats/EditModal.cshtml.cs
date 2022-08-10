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
            var nhaSanXuatExist = await _nhaSanXuatAppService.CheckTenNhaSanXuatExist(EditNhaSanXuats.TenNhaSX);
            if (nhaSanXuatExist == false)
            {
                await _nhaSanXuatAppService.CreateAsync(
                    ObjectMapper.Map<EditNhaSanXuatViewModal, CreateUpdateNhaSanXuatDto>(EditNhaSanXuats));
            }
            else
            {
                throw new UserFriendlyException(L["Nhà sản xuất " + EditNhaSanXuats.TenNhaSX + " đã tồn tại"]);
            }
            return NoContent();
        }

        public class EditNhaSanXuatViewModal
        {
            [HiddenInput]
            public Guid Id { get; set; }
            [DisplayName("Tên Nhà sản xuất")]
            public string TenNhaSX { get; set; }
            [DisplayName("Tên Vaccine sản xuất")]
            public string TenVaccineSX { get; set; }
            [DisplayName("Địa chỉ")]
            public string Diachi { get; set; }
            [DisplayName("Số điện thoại")]
            [RegularExpression("[0-9]{10}")]
            public string SDT { get; set; }
            [EmailAddress]
            public string Email { get; set; }
        }
    }
}
