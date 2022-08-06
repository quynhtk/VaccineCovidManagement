using Volo.Abp.Modularity;

namespace VaccineCovidManagement;

[DependsOn(
    typeof(VaccineCovidManagementApplicationModule),
    typeof(VaccineCovidManagementDomainTestModule)
    )]
public class VaccineCovidManagementApplicationTestModule : AbpModule
{

}
