using ASMGX.DeepMed.Infrastructure.Models;
using ASMGX.DeepMed.Model.Authentication;
using AutoMapper;

namespace ASMGX.DeepMed.Mapping.Authentication
{
    public class Authentication : Profile
    {
        public Authentication()
        {
            CreateMap<User, SignupDto>().ReverseMap();
        }
    }
}
