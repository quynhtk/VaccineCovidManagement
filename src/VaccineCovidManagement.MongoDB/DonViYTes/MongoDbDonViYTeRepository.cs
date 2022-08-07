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

namespace VaccineCovidManagement.DonViYTes
{
    public class MongoDbDonViYTeRepository
        : MongoDbRepository<VaccineCovidManagementMongoDbContext, DonViYTe, Guid>, IDonViYTeRepository
    {
        public MongoDbDonViYTeRepository(IMongoDbContextProvider<VaccineCovidManagementMongoDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        public async Task<List<DonViYTe>> GetListAsync(
            int skipCount, 
            int maxResultCount, 
            string sorting, 
            string filter)
        {
            var queryable = await GetMongoQueryableAsync();
            return await queryable
                .WhereIf<DonViYTe, IMongoQueryable<DonViYTe>>(
                    !filter.IsNullOrWhiteSpace(),
                    nhaSanXuat => nhaSanXuat.TenDonViYTe.Contains(filter))
                .OrderByDescending(x => x.CreationTime)
                .As<IMongoQueryable<DonViYTe>>()
                .Skip(skipCount)
                .Take(maxResultCount)
                .ToListAsync();
        }

        public async Task<DonViYTe> FindByDonViYTeAsync(string donViYTe)
        {
            var queryable = await GetMongoQueryableAsync();
            return await queryable.FirstOrDefaultAsync(x => x.TenDonViYTe == donViYTe);
        }
    }
}
