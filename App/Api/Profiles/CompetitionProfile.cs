using AutoMapper;
using FairDraw.App.Core.Entities;
using FairDraw.App.Core.Requests;
using FairDraw.App.Core.Responses;

namespace FairDraw.App.Api.Profiles
{
    public class CompetitionProfile : Profile
    {
        public CompetitionProfile()
        {
            CreateMap<NewCompetitionRequest, CompetitionEntity>();
            CreateMap<CompetitionEntity, GetCompetitionResponse>();
        }
    }
}
