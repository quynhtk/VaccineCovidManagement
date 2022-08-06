using VaccineCovidManagement.MongoDB;
using Xunit;

namespace VaccineCovidManagement;

[CollectionDefinition(VaccineCovidManagementTestConsts.CollectionDefinitionName)]
public class VaccineCovidManagementWebCollection : VaccineCovidManagementMongoDbCollectionFixtureBase
{

}
