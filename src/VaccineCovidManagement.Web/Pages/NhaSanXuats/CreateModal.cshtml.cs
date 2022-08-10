using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using VaccineCovidManagement.NhaSanXuats;
using Volo.Abp;

namespace VaccineCovidManagement.Web.Pages.NhaSanXuats
{
    public class CreateModalModel : VaccineCovidManagementPageModel
    {
        private readonly NhaSanXuatAppService _nhaSanXuatAppService;

        public CreateModalModel(NhaSanXuatAppService nhaSanXuatAppService)
        {
            _nhaSanXuatAppService = nhaSanXuatAppService;
        }

        [BindProperty]
        public CreateNhaSanXuatViewModal CreateNhaSanXuats { get; set; }
        public void OnGet()
        {
            CreateNhaSanXuats = new CreateNhaSanXuatViewModal();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var nhaSanXuatExist = await _nhaSanXuatAppService.CheckTenNhaSanXuatExist(CreateNhaSanXuats.TenNhaSX);
            if (nhaSanXuatExist == false)
            {
                await _nhaSanXuatAppService.CreateAsync(
                    ObjectMapper.Map<CreateNhaSanXuatViewModal, CreateUpdateNhaSanXuatDto>(CreateNhaSanXuats));
            }
            else
            {
                throw new UserFriendlyException(L["Nhà sản xuất " + CreateNhaSanXuats.TenNhaSX + " đã tồn tại"]);
            }
            return NoContent();
        }

        public class CreateNhaSanXuatViewModal
        {
            [Required]
            [DisplayName("Tên Nhà sản xuất")]
            public string TenNhaSX { get; set; }
            [Required]
            [DisplayName("Địa chỉ")]
            public string Diachi { get; set; }
            [Required]
            [DisplayName("Số điện thoại")]
            [RegularExpression("[0-9]{11}")]
            public string SDT { get; set; }
            [Required]
            [EmailAddress]
            public string Email { get; set; }
        }
    }
}
