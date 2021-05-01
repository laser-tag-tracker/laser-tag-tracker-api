using AutoMapper;
using LaserTagTrackerApi.Model;
using LaserTagTrackerApi.Model.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace LaserTagTrackerApi.Mapper
{
    public class MatchMapper : Profile
    {
        public MatchMapper()
        {
            CreateMap<Match, CreateMatchDto>().ReverseMap();
        }
    }
}