using AutoMapper;
using VaccineCovidManagement.ChiTietNhaps;
using VaccineCovidManagement.ChiTietXuats;
using VaccineCovidManagement.DonViYTes;
using VaccineCovidManagement.NhaSanXuats;
using VaccineCovidManagement.VaccineTonKhos;

namespace VaccineCovidManagement;

public class VaccineCovidManagementApplicationAutoMapperProfile : Profile
{
    public VaccineCovidManagementApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */
        CreateMap<NhaSanXuat, NhaSanXuatDto>();
        CreateMap<CreateUpdateNhaSanXuatDto, NhaSanXuat>();

        CreateMap<ChiTietNhap, ChiTietNhapDto>();
        CreateMap<NhaSanXuat, GetNhaSanXuatLookup>();
        CreateMap<CreateUpdateChiTietNhapDto, ChiTietNhap>();

        CreateMap<VaccineTonKho, VaccineTonKhoDto>();

        CreateMap<DonViYTe, DonViYTeDto>();
        CreateMap<CreateUpdateDonViYTeDto, DonViYTe>();

        CreateMap<ChiTietXuat, ChiTietXuatDto>();
        CreateMap<DonViYTe, GetDonViYTeLookup>();
        CreateMap<VaccineTonKho, GetVaccineTonKhoLookup>();
        CreateMap<CreateUpdateChiTietXuatDto, ChiTietXuat>();
    }
}
