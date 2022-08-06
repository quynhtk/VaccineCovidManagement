using VaccineCovidManagement.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace VaccineCovidManagement.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class VaccineCovidManagementController : AbpControllerBase
{
    protected VaccineCovidManagementController()
    {
        LocalizationResource = typeof(VaccineCovidManagementResource);
    }
}
