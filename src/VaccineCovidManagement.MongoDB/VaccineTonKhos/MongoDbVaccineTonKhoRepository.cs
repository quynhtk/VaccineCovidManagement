using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VaccineCovidManagement.MongoDB;
using Volo.Abp.Domain.Repositories.MongoDB;
using Volo.Abp.MongoDB;

namespace VaccineCovidManagement.VaccineTonKhos
{
    public class MongoDbVaccineTonKhoRepository
        : MongoDbRepository<VaccineCovidManagementMongoDbContext, VaccineTonKho, Guid>, IVaccineTonKhoRepository
    {
        public MongoDbVaccineTonKhoRepository(IMongoDbContextProvider<VaccineCovidManagementMongoDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        public async Task<List<VaccineTonKho>> GetListAsync(
            int skipCount,
            int maxResultCount,
            string sorting,
            string filter)
        {

            var queryable = await GetMongoQueryableAsync();
            return await queryable
                .WhereIf<VaccineTonKho, IMongoQueryable<VaccineTonKho>>(
                    !filter.IsNullOrWhiteSpace(),
                    vaccineTonKho => vaccineTonKho.TenVaccineTonKho.Contains(filter))
                .OrderByDescending(x => x.CreationTime)
                .As<IMongoQueryable<VaccineTonKho>>()
                .Skip(skipCount)
                .Take(maxResultCount)
                .ToListAsync();
        }

        public async Task<VaccineTonKho> FindVaccineTonKhoByIdAsync(Guid id)
        {
            var queryable = await GetMongoQueryableAsync();
            return await queryable.FirstOrDefaultAsync(x => x.ChiTietNhapId == id);
        }
    }
}
