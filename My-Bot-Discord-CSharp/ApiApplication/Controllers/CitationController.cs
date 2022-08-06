using ApiApplication.Controllers.Interfaces;

using ApiApplication.Repository.Interface;

using AutoMapper;
using BotClassLibrary;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceClassLibrary.Interfaces;
using ServiceClassLibrary.Services;

namespace ApiApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
 
    public class CitationController : ControllerBase
    {

        private readonly IMapper Mapper;

        private readonly ApplicationDbContext Context;

        private readonly IGenericRepository<Citation> CitationRepository;
        private ILoggerProject LoggerProject { get; init; }

        public CitationController(IMapper mapper, ApplicationDbContext context, IGenericRepository<Citation> citationRepository)
        {
            Mapper = mapper;
            Context = context;
            CitationRepository = citationRepository;
            LoggerProject = new LoggerProject();
        }

        [Authorize]
        [HttpDelete]
        public ActionResult<Citation> Delete([FromQuery] Guid id)
        {
            try
            {

                CitationRepository.Delete(id);


                Context.SaveChanges();
                return Ok();

            }
            catch (Exception ex)
            {
                LoggerProject.WriteLogErrorLog(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult<Citation> Get([FromQuery] Guid id)
        {
            try
            {
                var p = CitationRepository.Get(id);
                if (p == null)
                {

                    return NotFound();
                }

                else
                    return Ok();

            }
            catch (Exception ex)
            {
                LoggerProject.WriteLogErrorLog(ex.Message);
                return NotFound();
            }
        }

        [Authorize]
        [HttpPost]
        public ActionResult<Citation> Add(Citation citationDto)
        {
            try
            {
                

 

                if (citationDto == null)
                    return NotFound();
                else
                {

                    CitationRepository.Add(citationDto);

                    return Ok();
                }

            }
            catch (Exception ex)
            {
                LoggerProject.WriteLogErrorLog(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPut]
        public ActionResult<Citation> Update(Citation dto)
        {
            try
            {


                var p = CitationRepository.Get(dto.Id); 
                if (p == null)
                {
                    return BadRequest();
                }

                p = Mapper.Map<Citation>(dto);

                CitationRepository.Update(p);

                Context.SaveChanges();

                return Ok();

            }
            catch (Exception ex)
            {
                LoggerProject.WriteLogErrorLog(ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}