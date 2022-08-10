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

namespace VaccineCovidManagement.ChiTietXuats
{
    public class MongoDbChiTietXuatRepository
        : MongoDbRepository<VaccineCovidManagementMongoDbContext, ChiTietXuat, Guid>, IChiTietXuatRepository
    {
        public MongoDbChiTietXuatRepository(IMongoDbContextProvider<VaccineCovidManagementMongoDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        public async Task<List<ChiTietXuat>> GetListAsync(
            int skipCount,
            int maxResultCount,
            string sorting,
            string filter)
        {
            var queryable = await GetMongoQueryableAsync();
            return await queryable
                .WhereIf<ChiTietXuat, IMongoQueryable<ChiTietXuat>>(
                    !filter.IsNullOrWhiteSpace(),
                    chitietxuat => chitietxuat.TenVaccineXuat.Contains(filter))
                .OrderByDescending(x => x.CreationTime)
                .As<IMongoQueryable<ChiTietXuat>>()
                .Skip(skipCount)
                .Take(maxResultCount)
                .ToListAsync();
        }

        public async Task<ChiTietXuat> FindByIdDonViYTeAsync(Guid id)
        {
            var queryable = await GetMongoQueryableAsync();
            return await queryable.FirstOrDefaultAsync(x => x.DonViID == id);
        }

        public async Task<ChiTietXuat> FindByIdVaccineTonKhoAsync(Guid id)
        {
            var queryable = await GetMongoQueryableAsync();
            return await queryable.FirstOrDefaultAsync(x => x.VaccineTonKhoID == id);
        }
    }
}
