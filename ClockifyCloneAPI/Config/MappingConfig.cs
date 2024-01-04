using AutoMapper;
using ClockifyCloneAPI.Entities;
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
                config.CreateMap<GetUserResponse, UserEntity>().ReverseMap();
                config.CreateMap<UpdateUserRequest, UserEntity>()
                    .ForAllMembers(opt =>
                        opt.Condition((src, dest, srcMember, destMember) =>
                            VerifyNullMemberAndNullForeignKeyCondition(srcMember)
                        ));

                config.CreateMap<GetCompanyResponse, CompanyEntity>().ReverseMap();
                config.CreateMap<UpdateCompanyRequest, CompanyEntity>()
                    .ForAllMembers(opt =>
                        opt.Condition((src, dest, srcMember, destMember) =>
                            VerifyNullMemberCondition(srcMember)
                        )
                    );
            });
            return mappingConfig;
        }

        private static bool VerifyNullMemberCondition(object srcMember)
        {
            return srcMember != null;
        }

        private static bool VerifyNullMemberAndNullForeignKeyCondition(object srcMember)
        {
            return srcMember != null && !srcMember.Equals(default(int));
        }





    }
}
