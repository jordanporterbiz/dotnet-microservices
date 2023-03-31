using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlatformService.Data;
using PlatformService.Dtos;


namespace PlatformService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlatformsController : ControllerBase
    {
        private readonly IPlatformRepo _repo;
        private readonly IMapper _mapper;

        public PlatformsController(IPlatformRepo repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<PlatformReadDto>> GetPlatforms()
        {
            Console.WriteLine("--> Getting Platforms...");

            var platformItems = _repo.GetAllPlatforms();

            return Ok(_mapper.Map<IEnumerable<PlatformReadDto>>(platformItems));
        }

        [HttpGet("{id}", Name = "GetPlatformById")]
        public ActionResult<PlatformReadDto> GetPlatformById(int id)
        {
            var platformItem = _repo.GetPlatformById(id);
            if (platformItem != null)
            {
                return Ok(_mapper.Map<PlatformReadDto>(platformItem));
            }
            return NotFound();
        }

        [HttpPost]
        public ActionResult<PlatformReadDto> CreatePlatform(PlatformCreateDto platformCreateDto)
        {
            var platformModel = _mapper.Map<Models.Platform>(platformCreateDto);
            _repo.CreatePlatform(platformModel);
            _repo.SaveChanges();

            var platformReadDto = _mapper.Map<PlatformReadDto>(platformModel);

            return CreatedAtRoute(nameof(GetPlatformById), new { Id = platformReadDto.Id }, platformReadDto);
        }

        // TODO: Implement UpdatePlatform
        // [HttpPut("{id}")]
        // public ActionResult UpdatePlatform(int id, PlatformUpdateDto platformUpdateDto)
        // {
        //     var platformModelFromRepo = _repo.GetPlatformById(id);
        //     if (platformModelFromRepo == null)
        //     {
        //         return NotFound();
        //     }

        //     _mapper.Map(platformUpdateDto, platformModelFromRepo);

        //     _repo.UpdatePlatform(platformModelFromRepo);

        //     _repo.SaveChanges();

        //     return NoContent();
        // }

        // TODO: Implement PartialPlatformUpdate
        // public ActionResult PartialPlatformUpdate(int id, JsonPatchDocument<PlatformUpdateDto> patchDoc)
        // {
        //     var platformModelFromRepo = _repo.GetPlatformById(id);
        //     if (platformModelFromRepo == null)
        //     {
        //         return NotFound();
        //     }

        //     var platformToPatch = _mapper.Map<PlatformUpdateDto>(platformModelFromRepo);
        //     patchDoc.ApplyTo(platformToPatch, ModelState);

        //     if (!TryValidateModel(platformToPatch))
        //     {
        //         return ValidationProblem(ModelState);
        //     }

        //     _mapper.Map(platformToPatch, platformModelFromRepo);

        //     _repo.UpdatePlatform(platformModelFromRepo);

        //     _repo.SaveChanges();

        //     return NoContent();
        // }

        // TODO: Implement DeletePlatform
        // [HttpDelete("{id}")]
        // public ActionResult DeletePlatform(int id)
        // {
        //     var platformModelFromRepo = _repo.GetPlatformById(id);
        //     if (platformModelFromRepo == null)
        //     {
        //         return NotFound();
        //     }

        //     _repo.DeletePlatform(platformModelFromRepo);
        //     _repo.SaveChanges();

        //     return NoContent();
        // }


    }
}