using AutoMapper;
using LMMWebAPI.DataAccess;

namespace LMMWebAPI.Models
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            /*            CreateMap<Movie, MoviewDTO>()
                            .ForMember(second => second.Description,
                            map => map.MapFrom(
                                first => first.Assignemnt.Description
                                ));*/

            CreateMap<UserLogin, User>();
        }
    }
}
