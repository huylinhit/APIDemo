using AutoMapper;

namespace APIDemo.Profiles
{
    public class PointOfInterestProfile : Profile
    {
        public PointOfInterestProfile()
        {
            CreateMap<Entities.PointOfInterest, Models.PointOfInterestDTO>();
            CreateMap<Models.PointOfInterestCreationDTO, Entities.PointOfInterest>();
            CreateMap<Models.PointOfInterestUpdateDTO, Entities.PointOfInterest>();
            CreateMap<Entities.PointOfInterest, Models.PointOfInterestUpdateDTO>();
        }
    }
}
