using MongoDB.Driver;
using VaccineCovidManagement.NhaSanXuats;
using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace VaccineCovidManagement.MongoDB;

[ConnectionStringName("Default")]
public class VaccineCovidManagementMongoDbContext : AbpMongoDbContext
{
    /* Add mongo collections here. Example:
     * public IMongoCollection<Question> Questions => Collection<Question>();
     */

    public IMongoCollection<NhaSanXuat> NhaSanXuats => Collection<NhaSanXuat>();
    /*public IMongoCollection<DonViYTe> DonViYTes => Collection<DonViYTe>();
    public IMongoCollection<ChiTietNhap> ChiTietNhaps => Collection<ChiTietNhap>();
    public IMongoCollection<ChiTietXuat> ChiTietXuats => Collection<ChiTietXuat>();
    public IMongoCollection<VaccineTonKho> VaccineTonKhos => Collection<VaccineTonKho>();*/

    protected override void CreateModel(IMongoModelBuilder modelBuilder)
    {
        base.CreateModel(modelBuilder);

        //modelBuilder.Entity<YourEntity>(b =>
        //{
        //    //...
        //});
    }
}
