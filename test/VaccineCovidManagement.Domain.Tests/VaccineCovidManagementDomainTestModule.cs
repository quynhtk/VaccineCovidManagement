using VaccineCovidManagement.MongoDB;
using Volo.Abp.Modularity;

namespace VaccineCovidManagement;

[DependsOn(
    typeof(VaccineCovidManagementMongoDbTestModule)
    )]
public class VaccineCovidManagementDomainTestModule : AbpModule
{

}
