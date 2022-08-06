using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace VaccineCovidManagement.Data;

/* This is used if database provider does't define
 * IVaccineCovidManagementDbSchemaMigrator implementation.
 */
public class NullVaccineCovidManagementDbSchemaMigrator : IVaccineCovidManagementDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
