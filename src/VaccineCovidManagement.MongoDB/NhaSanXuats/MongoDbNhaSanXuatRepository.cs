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

namespace VaccineCovidManagement.NhaSanXuats
{
    public class MongoDbNhaSanXuatRepository
        : MongoDbRepository<VaccineCovidManagementMongoDbContext, NhaSanXuat, Guid>, INhaSanXuatRepository
    {
        public MongoDbNhaSanXuatRepository(IMongoDbContextProvider<VaccineCovidManagementMongoDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        public async Task<List<NhaSanXuat>> GetListAsync(
            int skipCount,
            int maxResultCount,
            string sorting,
            string filter)
        {
            var queryable = await GetMongoQueryableAsync();
            return await queryable
                .WhereIf<NhaSanXuat, IMongoQueryable<NhaSanXuat>>(
                    !filter.IsNullOrWhiteSpace(),
                    nhaSanXuat => nhaSanXuat.TenNhaSX.Contains(filter))
                .OrderByDescending(x => x.CreationTime)
                .As<IMongoQueryable<NhaSanXuat>>()
                .Skip(skipCount)
                .Take(maxResultCount)
                .ToListAsync();
        }

        public async Task<NhaSanXuat> FindByNoiSanXuatAsync(string noiSX)
        {
            var queryable = await GetMongoQueryableAsync();
            return await queryable.FirstOrDefaultAsync(c => c.TenNhaSX == noiSX);
        }

        public async Task<NhaSanXuat> FindNhaSanXuatByIdAsync(Guid id)
        {
            var queryable = await GetMongoQueryableAsync();
            return await queryable.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
