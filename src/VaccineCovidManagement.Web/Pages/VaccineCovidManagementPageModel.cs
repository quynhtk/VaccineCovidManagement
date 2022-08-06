using VaccineCovidManagement.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace VaccineCovidManagement.Web.Pages;

/* Inherit your PageModel classes from this class.
 */
public abstract class VaccineCovidManagementPageModel : AbpPageModel
{
    protected VaccineCovidManagementPageModel()
    {
        LocalizationResourceType = typeof(VaccineCovidManagementResource);
    }
}
