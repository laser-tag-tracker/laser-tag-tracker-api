using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using LaserTagTrackerApi.Model;
using LaserTagTrackerApi.Model.DTOs;
using LaserTagTrackerApi.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LaserTagTrackerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MatchesController : ControllerBase
    {

        private readonly IMapper mapper;
        private readonly IUserRepository userRepository;

        public MatchesController(IMapper mapper, IUserRepository userRepository)
        {
            this.mapper = mapper;
            this.userRepository = userRepository;
        }

        [HttpPost("{userId:guid}")]
        public async Task<ActionResult<Match>> CreateMatch([FromBody] CreateMatchDto dto, Guid userId)
        {
            var match = mapper.Map<Match>(dto);

            var user = await userRepository.FindUserByIdWithMatches(userId/*this.CurrentUserId*/);
            user.Matches.Add(match);
            await userRepository.UpdateUser(user);

            return match;
        }
        
        [HttpGet("{userId:guid}")]
        public async Task<ActionResult<List<Match>>> GetMatches(Guid userId)
        {
            var user = await userRepository.FindUserByIdWithMatches(userId/*this.CurrentUserId*/);
            return user.Matches;
        }

        //private Guid CurrentUserId => Guid.Parse(User.FindFirst(ClaimTypes.Name)?.Value);
    }
}