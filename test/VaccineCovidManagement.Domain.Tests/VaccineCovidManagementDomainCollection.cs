using VaccineCovidManagement.MongoDB;
using Xunit;

namespace VaccineCovidManagement;

[CollectionDefinition(VaccineCovidManagementTestConsts.CollectionDefinitionName)]
public class VaccineCovidManagementDomainCollection : VaccineCovidManagementMongoDbCollectionFixtureBase
{

}
