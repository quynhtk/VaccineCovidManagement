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

namespace VaccineCovidManagement.ChiTietNhaps
{
    public class MongoDbChiTietNhapRepository
        : MongoDbRepository<VaccineCovidManagementMongoDbContext, ChiTietNhap, Guid>, IChiTietNhapRepository
    {
        public MongoDbChiTietNhapRepository(IMongoDbContextProvider<VaccineCovidManagementMongoDbContext> dbContextProvider) 
            : base(dbContextProvider)
        {
        }

        public async Task<List<ChiTietNhap>> GetListAsync(
            int skipCount,
            int maxResultCount,
            string sorting,
            string filter)
        {
            var queryable = await GetMongoQueryableAsync();
            return await queryable
                .WhereIf<ChiTietNhap, IMongoQueryable<ChiTietNhap>>(
                    !filter.IsNullOrWhiteSpace(),
                    chiTietNhap => chiTietNhap.TenVaccineSX.Contains(filter))
                .OrderByDescending(x => x.CreationTime)
                .As<IMongoQueryable<ChiTietNhap>>()
                .Skip(skipCount)
                .Take(maxResultCount)
                .ToListAsync();
        }

        public async Task<ChiTietNhap> FindByIdNoiSanXuatAsync(Guid id)
        {
            var queryable = await GetMongoQueryableAsync();
            return await queryable.FirstOrDefaultAsync(c => c.NhaSxID == id);
        }
    }
}
