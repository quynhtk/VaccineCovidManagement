using AutoMapper;
using VaccineCovidManagement.ChiTietNhaps;
using VaccineCovidManagement.DonViYTes;
using VaccineCovidManagement.NhaSanXuats;

namespace VaccineCovidManagement.Web;

public class VaccineCovidManagementWebAutoMapperProfile : Profile
{
    public VaccineCovidManagementWebAutoMapperProfile()
    {
        //Define your AutoMapper configuration here for the Web project.
        CreateMap<Pages.NhaSanXuats.CreateModalModel.CreateNhaSanXuatViewModal, CreateUpdateNhaSanXuatDto>();
        CreateMap<NhaSanXuatDto, Pages.NhaSanXuats.EditModalModel.EditNhaSanXuatViewModal>();
        CreateMap<Pages.NhaSanXuats.EditModalModel.EditNhaSanXuatViewModal, CreateUpdateNhaSanXuatDto>();

        CreateMap<Pages.ChiTietNhaps.CreateModalModel.CreateChiTietNhapViewModal, CreateUpdateChiTietNhapDto>();
        CreateMap<ChiTietNhapDto, Pages.ChiTietNhaps.EditModalModel.EditChiTietNhapViewModal>();
        CreateMap<Pages.ChiTietNhaps.EditModalModel.EditChiTietNhapViewModal, CreateUpdateChiTietNhapDto>();

        CreateMap<Pages.DonViYTes.CreateModalModel.CreateDonViYTeViewModal, CreateUpdateDonViYTeDto>();
        CreateMap<DonViYTeDto, Pages.DonViYTes.EditModalModel.EditDonViYTeViewModal>();
        CreateMap<Pages.DonViYTes.EditModalModel.EditDonViYTeViewModal, CreateUpdateDonViYTeDto>();
    }
}
