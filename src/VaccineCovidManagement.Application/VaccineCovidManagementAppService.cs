using System;
using System.Collections.Generic;
using System.Text;
using VaccineCovidManagement.Localization;
using Volo.Abp.Application.Services;

namespace VaccineCovidManagement;

/* Inherit your application services from this class.
 */
public abstract class VaccineCovidManagementAppService : ApplicationService
{
    protected VaccineCovidManagementAppService()
    {
        LocalizationResource = typeof(VaccineCovidManagementResource);
    }
}
