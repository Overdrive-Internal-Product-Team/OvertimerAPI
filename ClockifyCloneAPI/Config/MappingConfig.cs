using AutoMapper;
using ClockifyCloneAPI.Entities;
using ClockifyCloneAPI.Models.Auth;
using ClockifyCloneAPI.Models.Company;
using ClockifyCloneAPI.Models.User;

namespace PersonalizeFIT.ExerciseAPI.Config
{
    public class MappingConfig
    {

        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<PostUserRequest, UserEntity>().ReverseMap();
                config.CreateMap<GetUserDataResponse, UserEntity>().ReverseMap();

                config.CreateMap<GetCompanyResponse, CompanyEntity>().ReverseMap();
                config.CreateMap<PutCompanyRequest, CompanyEntity>()
                    .ForAllMembers(opt => opt.Condition((src, dest, srcMember, destMember) =>
                    {
                        return srcMember != null;
                    }));

            });
            return mappingConfig;
        }



    }
}
