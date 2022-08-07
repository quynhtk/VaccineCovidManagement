using MongoDB.Driver;
using VaccineCovidManagement.ChiTietNhaps;
using VaccineCovidManagement.DonViYTes;
using VaccineCovidManagement.NhaSanXuats;
using VaccineCovidManagement.VaccineTonKhos;
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
    public IMongoCollection<ChiTietNhap> ChiTietNhaps => Collection<ChiTietNhap>();
    public IMongoCollection<VaccineTonKho> VaccineTonKhos => Collection<VaccineTonKho>();
    public IMongoCollection<DonViYTe> DonViYTes => Collection<DonViYTe>();
    /*public IMongoCollection<ChiTietXuat> ChiTietXuats => Collection<ChiTietXuat>();*/

    protected override void CreateModel(IMongoModelBuilder modelBuilder)
    {
        base.CreateModel(modelBuilder);

        //modelBuilder.Entity<YourEntity>(b =>
        //{
        //    //...
        //});
    }
}
