using AutoMapper;
using VaccineCovidManagement.ChiTietNhaps;
using VaccineCovidManagement.ChiTietXuats;
using VaccineCovidManagement.DonViYTes;
using VaccineCovidManagement.NhaSanXuats;
using VaccineCovidManagement.VaccineTonKhos;

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

        CreateMap<Pages.ChiTietXuats.CreateModalModel.CreateChiTietXuatViewModal, CreateUpdateChiTietXuatDto>();
        CreateMap<ChiTietXuatDto, Pages.ChiTietXuats.EditModalModel.EditChiTietXuatViewModal>();
        CreateMap<Pages.ChiTietXuats.EditModalModel.EditChiTietXuatViewModal, CreateUpdateChiTietXuatDto>();

        CreateMap<Pages.VaccineTonKhos.CreateModalModel.CreateVaccineTonKhoViewModal, CreateUpdateVaccineTonKhoDto>();
    }
}
