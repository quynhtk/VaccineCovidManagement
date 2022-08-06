using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace VaccineCovidManagement.Pages;

[Collection(VaccineCovidManagementTestConsts.CollectionDefinitionName)]
public class Index_Tests : VaccineCovidManagementWebTestBase
{
    [Fact]
    public async Task Welcome_Page()
    {
        var response = await GetResponseAsStringAsync("/");
        response.ShouldNotBeNull();
    }
}
