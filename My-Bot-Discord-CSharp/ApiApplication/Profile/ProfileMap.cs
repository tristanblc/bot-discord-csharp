using BotDTOClassLibrary;
using BusClassLibrary;

namespace ApiApplication.Profile
{
    public class ProfileMap : AutoMapper.Profile
    {
        public ProfileMap()
        {
            CreateMap<Arret,ArretDto>().ReverseMap();
            CreateMap<Ligne,LigneDto>().ReverseMap();

            CreateMap<StopTimes, StopTimesDto>().ReverseMap();
            CreateMap<Shape, ShapeDto>().ReverseMap();
        }
    }
}
