using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using VaccineCovidManagement.DonViYTes;
using Volo.Abp;

namespace VaccineCovidManagement.Web.Pages.DonViYTes
{
    public class CreateModalModel : VaccineCovidManagementPageModel
    {
        private readonly DonViYTeAppService _donViYTeAppService;

        public CreateModalModel(DonViYTeAppService donViYTeAppService)
        {
            _donViYTeAppService = donViYTeAppService;
        }

        [BindProperty]
        public CreateDonViYTeViewModal CreateDonViYTes { get; set; }

        public void OnGet()
        {
            CreateDonViYTes = new CreateDonViYTeViewModal();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var donviyteExist = await _donViYTeAppService.CheckDonViYTeExist(CreateDonViYTes.TenDonViYTe);
            if (donviyteExist == false)
            {
                await _donViYTeAppService.CreateAsync(
                    ObjectMapper.Map<CreateDonViYTeViewModal, CreateUpdateDonViYTeDto>(CreateDonViYTes));
            }
            else
            {
                throw new UserFriendlyException(L["Đơn vị Y tế " + CreateDonViYTes.TenDonViYTe + " đã tồn tại"]);
            }
            return NoContent();
        }

        public class CreateDonViYTeViewModal
        {
            [Required]
            [DisplayName("Tên Đơn vị Y tế")]
            public string TenDonViYTe { get; set; }
            [Required]
            [DisplayName("Địa chỉ")]
            public string DiaChi { get; set; }
            [Required]
            [DisplayName("Số điện thoại")]
            [RegularExpression("[0-9]{11}")]
            public string SDT { get; set; }
        }
    }
}
