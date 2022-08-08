namespace VaccineCovidManagement.Permissions;

public static class VaccineCovidManagementPermissions
{
    public const string GroupName = "VaccineCovidManagement";

    //Add your own permission names. Example:
    //public const string MyPermission1 = GroupName + ".MyPermission1";

    public static class NhaSanXuats
    {
        public const string Default = GroupName + ".NhaSanXuats";
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
    }

    public static class ChiTietNhaps
    {
        public const string Default = GroupName + ".ChiTietNhaps";
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
    }

    public static class VaccineTonKhos
    {
        public const string Default = GroupName + ".VaccineTonKhos";
    }

    public static class DonViYTes
    {
        public const string Default = GroupName + ".DonViYTes";
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
    }

    public static class ChiTietXuats
    {
        public const string Default = GroupName + ".ChiTietXuats";
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
    }
}
