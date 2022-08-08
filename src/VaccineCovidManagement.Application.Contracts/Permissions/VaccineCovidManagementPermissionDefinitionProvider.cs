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

        var chiTietNhapPermission = vaccineCovidGroup.AddPermission(VaccineCovidManagementPermissions.ChiTietNhaps.Default, L("Permission:ChiTietNhaps"));
        chiTietNhapPermission.AddChild(VaccineCovidManagementPermissions.ChiTietNhaps.Create, L("Permission:ChiTietNhaps.Create"));
        chiTietNhapPermission.AddChild(VaccineCovidManagementPermissions.ChiTietNhaps.Edit, L("Permission:ChiTietNhaps.Edit"));
        chiTietNhapPermission.AddChild(VaccineCovidManagementPermissions.ChiTietNhaps.Delete, L("Permission:ChiTietNhaps.Delete"));

        var vaccineTonKhoPermission = vaccineCovidGroup.AddPermission(VaccineCovidManagementPermissions.VaccineTonKhos.Default, L("Permission:VaccineTonKhos"));

        var donViYTePermission = vaccineCovidGroup.AddPermission(VaccineCovidManagementPermissions.DonViYTes.Default, L("Permission:DonViYTes"));
        donViYTePermission.AddChild(VaccineCovidManagementPermissions.DonViYTes.Create, L("Permission:DonViYTes.Create"));
        donViYTePermission.AddChild(VaccineCovidManagementPermissions.DonViYTes.Edit, L("Permission:DonViYTes.Edit"));
        donViYTePermission.AddChild(VaccineCovidManagementPermissions.DonViYTes.Delete, L("Permission:DonViYTes.Delete"));

        var chiTietXuatPermission = vaccineCovidGroup.AddPermission(VaccineCovidManagementPermissions.ChiTietXuats.Default, L("Permission:ChiTietXuats"));
        chiTietXuatPermission.AddChild(VaccineCovidManagementPermissions.ChiTietXuats.Create, L("Permission:ChiTietXuats.Create"));
        chiTietXuatPermission.AddChild(VaccineCovidManagementPermissions.ChiTietXuats.Edit, L("Permission:ChiTietXuats.Edit"));
        chiTietXuatPermission.AddChild(VaccineCovidManagementPermissions.ChiTietXuats.Delete, L("Permission:ChiTietXuats.Delete"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<VaccineCovidManagementResource>(name);
    }
}
