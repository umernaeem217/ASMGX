using ASMGX.DeepMed.Application.Shared.Domain.Interfaces;
using ASMGX.DeepMed.Infrastructure.Models.Subscription;
using ASMGX.DeepMed.Model.General;
using ASMGX.DeepMed.Model.Subscription;
using ASMGX.DeepMed.Shared.Exceptions.Concrete;
using AutoMapper;

namespace ASMGX.DeepMed.Business.Subscription
{
    public class PlanManager : IPlanManager
    {
        private readonly IRepository<Plan> _planRepository;
        private readonly IMapper _mapper;

        public PlanManager(IRepository<Plan> planRepository, IMapper mapper)
        {
            _planRepository = planRepository;
            _mapper = mapper;
        }

        public async Task<string> Create(CreateOrUpdatePlanDto createOrUpdatePlanDto)
        {
            var plan = _mapper.Map<Plan>(createOrUpdatePlanDto);
            plan.Id = Guid.NewGuid().ToString();
            _planRepository.Add(plan);
            await _planRepository.SaveChangesAsync();
            return plan.Id;
        }

        public async Task Delete(string planId)
        {
            var plan = await _planRepository.GetByIdAsync(planId);
            if (plan == null)
                throw new UserFriendlyException("No plan found with this id.");
            _planRepository.Remove(plan);
            await _planRepository.SaveChangesAsync();
        }

        public async Task<CreateOrUpdatePlanDto> GetById(string planId)
        {
            return _mapper.Map<CreateOrUpdatePlanDto>(await _planRepository.GetByIdAsync(planId) 
                ?? throw new UserFriendlyException("No plan found with this id."));
        }

        public async Task<PaginatedList<CreateOrUpdatePlanDto>> GetPaged(QueryParameters parameters)
        {
            var plan = _planRepository.GetIQueryable();
            if (!string.IsNullOrEmpty(parameters.Filter))
            {
                plan = plan.Where(x => x.Name.ToLower() == parameters.Filter);
            }
            var pagedResult = await PaginatedList<Plan>.CreateAsync(plan, parameters.PageNumber,parameters.PageSize);
            return _mapper.Map<PaginatedList<CreateOrUpdatePlanDto>>(pagedResult);
        }

        public async Task<IList<LookupDto>> GetPlans()
        {
            var plans = await _planRepository.GetAllAsync();
            return plans.Select(x => new LookupDto()
            {
                Name = x.Name,
                Value = x.Id
            }).ToList();
        }

        public async Task<string> Update(CreateOrUpdatePlanDto updatePlanDto)
        {
            var updatedPlan = _mapper.Map<Plan>(updatePlanDto);
            _planRepository.Edit(updatedPlan);
            await _planRepository.SaveChangesAsync();
            return updatedPlan.Id;
        }
    }
}
