using System.Threading.Tasks;

namespace VaccineCovidManagement.Data;

public interface IVaccineCovidManagementDbSchemaMigrator
{
    Task MigrateAsync();
}
