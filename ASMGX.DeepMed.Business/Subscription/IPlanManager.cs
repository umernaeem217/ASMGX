using ASMGX.DeepMed.Model.General;
using ASMGX.DeepMed.Model.Subscription;

namespace ASMGX.DeepMed.Business.Subscription
{
    public interface IPlanManager
    {
        Task<IList<LookupDto>> GetPlans();
        Task<string> Create(CreateOrUpdatePlanDto createOrUpdatePlanDto);
        Task<string> Update(CreateOrUpdatePlanDto updatePlanDto);
        Task Delete(string planId);
        Task<CreateOrUpdatePlanDto> GetById(string planId);
        Task<PaginatedList<CreateOrUpdatePlanDto>> GetPaged(QueryParameters parameters);

    }
}
