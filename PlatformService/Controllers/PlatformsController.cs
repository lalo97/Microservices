﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PaltformService.Data;
using PaltformService.DTOs;
using PaltformService.Models;

namespace PaltformService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlatformsController : ControllerBase
    {
        private readonly IPlatformRepo _repository;
        private readonly IMapper _mapper;

        public PlatformsController(IPlatformRepo repository, IMapper mapper) { 
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<PlatformReadDTO>> GetPlatforms()
        {
            Console.WriteLine("---> Getting Platforms....");

            var platforms = _repository.GetAllPlatforms();

            return Ok(_mapper.Map<IEnumerable<PlatformReadDTO>>(platforms));
        }

        [HttpGet("{id}", Name = "GetPlatformById")]
        public ActionResult<PlatformReadDTO> GetPlatformById(int id) {
            var platform = _repository.GetPlatformById(id);

            if (platform == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<PlatformReadDTO>(platform));
        }

        [HttpPost]
        public ActionResult<PlatformReadDTO> CreatePlatform(PlatformCreateDTO platform) {
            
            if (platform == null)
            {
                return ValidationProblem("you cannot sent an empty object");
            }

            var platformModel = _mapper.Map<Platform>(platform);

            _repository.CreatePlatform(platformModel);

            _repository.SaveChanges();

            var platformReadDto = _mapper.Map<PlatformReadDTO>(platformModel);

            return CreatedAtRoute(nameof(GetPlatformById), new { Id = platformReadDto.Id }, platformReadDto);

        }
    }
}
