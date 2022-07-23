using ASMGX.DeepMed.Infrastructure.Models.Subscription;
using ASMGX.DeepMed.Model.General;
using ASMGX.DeepMed.Model.Subscription;
using AutoMapper;

namespace ASMGX.DeepMed.Mapping.Subscription
{
    public class SubscriptionProfile: Profile
    {
        public SubscriptionProfile()
        {
            CreateMap<Plan, CreateOrUpdatePlanDto>().ReverseMap();
            CreateMap<PaginatedList<Plan>, PaginatedList<CreateOrUpdatePlanDto>>().ReverseMap();
        }
    }
}
