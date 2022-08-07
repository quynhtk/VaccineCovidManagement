using System.Threading.Tasks;
using VaccineCovidManagement.Localization;
using VaccineCovidManagement.MultiTenancy;
using VaccineCovidManagement.Permissions;
using Volo.Abp.Identity.Web.Navigation;
using Volo.Abp.SettingManagement.Web.Navigation;
using Volo.Abp.TenantManagement.Web.Navigation;
using Volo.Abp.UI.Navigation;

namespace VaccineCovidManagement.Web.Menus;

public class VaccineCovidManagementMenuContributor : IMenuContributor
{
    public async Task ConfigureMenuAsync(MenuConfigurationContext context)
    {
        if (context.Menu.Name == StandardMenus.Main)
        {
            await ConfigureMainMenuAsync(context);
        }
    }

    private async Task ConfigureMainMenuAsync(MenuConfigurationContext context)
    {
        var administration = context.Menu.GetAdministration();
        var l = context.GetLocalizer<VaccineCovidManagementResource>();

        /*context.Menu.Items.Insert(
            0,
            new ApplicationMenuItem(
                VaccineCovidManagementMenus.Home,
                l["Menu:Home"],
                "~/",
                icon: "fas fa-home",
                order: 0
            )
        );*/

        if (await context.IsGrantedAsync(VaccineCovidManagementPermissions.NhaSanXuats.Default))
        {
            context.Menu.AddItem(
                new ApplicationMenuItem(
                        "VaccineCovidManagement.NhaSanXuats",
                        l["Menu:NhaSanXuats"],
                        url: "/NhaSanXuats")
            );
        }
        if (await context.IsGrantedAsync(VaccineCovidManagementPermissions.ChiTietNhaps.Default))
        {
            context.Menu.AddItem(
                new ApplicationMenuItem(
                        "VaccineCovidManagement.ChiTietNhaps",
                        l["Menu:ChiTietNhaps"],
                        url: "/ChiTietNhaps")
            );
        }
        if (await context.IsGrantedAsync(VaccineCovidManagementPermissions.VaccineTonKhos.Default))
        {
            context.Menu.AddItem(
                new ApplicationMenuItem(
                        "VaccineCovidManagement.VaccineTonKhos",
                        l["Menu:VaccineTonKhos"],
                        url: "/VaccineTonKhos")
            );
        }
        if (await context.IsGrantedAsync(VaccineCovidManagementPermissions.DonViYTes.Default))
        {
            context.Menu.AddItem(
                new ApplicationMenuItem(
                        "VaccineCovidManagement.DonViYTes",
                        l["Menu:DonViYTes"],
                        url: "/DonViYTes")
            );
        }


        if (MultiTenancyConsts.IsEnabled)
        {
            administration.SetSubItemOrder(TenantManagementMenuNames.GroupName, 1);
        }
        else
        {
            administration.TryRemoveMenuItem(TenantManagementMenuNames.GroupName);
        }

        administration.SetSubItemOrder(IdentityMenuNames.GroupName, 2);
        administration.SetSubItemOrder(SettingManagementMenuNames.GroupName, 3);
    }
}
