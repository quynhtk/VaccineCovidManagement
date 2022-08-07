using AutoMapper;
using VaccineCovidManagement.NhaSanXuats;

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
    }
}
