﻿using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace VaccineCovidManagement.MongoDB;

[ConnectionStringName("Default")]
public class VaccineCovidManagementMongoDbContext : AbpMongoDbContext
{
    /* Add mongo collections here. Example:
     * public IMongoCollection<Question> Questions => Collection<Question>();
     */

    protected override void CreateModel(IMongoModelBuilder modelBuilder)
    {
        base.CreateModel(modelBuilder);

        //modelBuilder.Entity<YourEntity>(b =>
        //{
        //    //...
        //});
    }
}
