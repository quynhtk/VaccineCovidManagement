using System;
using Volo.Abp.Data;
using Volo.Abp.Modularity;

namespace VaccineCovidManagement.MongoDB;

[DependsOn(
    typeof(VaccineCovidManagementTestBaseModule),
    typeof(VaccineCovidManagementMongoDbModule)
    )]
public class VaccineCovidManagementMongoDbTestModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var stringArray = VaccineCovidManagementMongoDbFixture.ConnectionString.Split('?');
        var connectionString = stringArray[0].EnsureEndsWith('/') +
                                   "Db_" +
                               Guid.NewGuid().ToString("N") + "/?" + stringArray[1];

        Configure<AbpDbConnectionOptions>(options =>
        {
            options.ConnectionStrings.Default = connectionString;
        });
    }
}
