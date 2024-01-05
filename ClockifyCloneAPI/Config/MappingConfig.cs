using AutoMapper;
using ClockifyCloneAPI.Entities;
using ClockifyCloneAPI.Models.Category;
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
                config.CreateMap<PostUserRequest, User>().ReverseMap();
                config.CreateMap<GetUserResponse, User>().ReverseMap();
                config.CreateMap<UpdateUserRequest, User>()
                    .ForAllMembers(opt =>
                        opt.Condition((src, dest, srcMember, destMember) =>
                            VerifyNullMemberAndNullForeignKeyCondition(srcMember)
                        ));

                config.CreateMap<GetCompanyResponse, Company>().ReverseMap();
                config.CreateMap<UpdateCompanyRequest, Company>()
                    .ForAllMembers(opt =>
                        opt.Condition((src, dest, srcMember, destMember) =>
                            VerifyNullMemberCondition(srcMember)
                        )
                    );

                config.CreateMap<PostCategoryRequest, Category>().ReverseMap();
                config.CreateMap<GetCategoryResponse, Category>().ReverseMap();
                config.CreateMap<GetAllCategoryResponse, Category>().ReverseMap();
                config.CreateMap<UpdateCategoryRequest, Category>()
                    .ForAllMembers(opt =>
                        opt.Condition((src, dest, srcMember, destMember) =>
                            VerifyNullMemberAndNullForeignKeyCondition(srcMember)
                        ));

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
