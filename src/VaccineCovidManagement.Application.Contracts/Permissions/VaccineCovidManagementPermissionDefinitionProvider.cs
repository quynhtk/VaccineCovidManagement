using VaccineCovidManagement.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace VaccineCovidManagement.Permissions;

public class VaccineCovidManagementPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        //Define your own permissions here. Example:
        //myGroup.AddPermission(VaccineCovidManagementPermissions.MyPermission1, L("Permission:MyPermission1"));

        var vaccineCovidGroup = context.AddGroup(VaccineCovidManagementPermissions.GroupName, L("Permission:VaccineCovidManagement"));

        var nhaSanXuatPermission = vaccineCovidGroup.AddPermission(VaccineCovidManagementPermissions.NhaSanXuats.Default, L("Permission:NhaSanXuats"));
        nhaSanXuatPermission.AddChild(VaccineCovidManagementPermissions.NhaSanXuats.Create, L("Permission:NhaSanXuats.Create"));
        nhaSanXuatPermission.AddChild(VaccineCovidManagementPermissions.NhaSanXuats.Edit, L("Permission:NhaSanXuats.Edit"));
        nhaSanXuatPermission.AddChild(VaccineCovidManagementPermissions.NhaSanXuats.Delete, L("Permission:NhaSanXuats.Delete"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<VaccineCovidManagementResource>(name);
    }
}
