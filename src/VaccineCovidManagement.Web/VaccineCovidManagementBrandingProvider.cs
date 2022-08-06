using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;

namespace VaccineCovidManagement.Web;

[Dependency(ReplaceServices = true)]
public class VaccineCovidManagementBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "VaccineCovidManagement";
}
