using VaccineCovidManagement.MongoDB;
using Volo.Abp.Autofac;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Modularity;

namespace VaccineCovidManagement.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(VaccineCovidManagementMongoDbModule),
    typeof(VaccineCovidManagementApplicationContractsModule)
    )]
public class VaccineCovidManagementDbMigratorModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpBackgroundJobOptions>(options => options.IsJobExecutionEnabled = false);
    }
}
