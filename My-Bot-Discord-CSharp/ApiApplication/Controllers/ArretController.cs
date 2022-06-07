using ApiApplication;
using ApiApplication.Repository;
using ApiApplication.Repository.Interface;
using AutoMapper;
using BotDTOClassLibrary;
using BusClassLibrary;
using Microsoft.AspNetCore.Mvc;
using ServiceClassLibrary.Interfaces;
using ServiceClassLibrary.Services;

namespace ApiApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArretController : Controller
    {
            private readonly IMapper _mapper;

            private readonly  ApplicationDbContext _context;

            private APIGenericRepository<Arret> genericRepository { get; set; }


            private ILoggerProject  LoggerProject { get; init; }

            public ArretController(IMapper mapper, ApplicationDbContext context, LoggerProject loggerProject)
            {
                _mapper = mapper;
                _context = context;
                LoggerProject = loggerProject;
                this.genericRepository = new APIGenericRepository<Arret>(context);
                
            }




            //// GET api/<ProjectController>/GetAll

            [HttpGet("all")]
            [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ArretDto))]
            [ProducesResponseType(StatusCodes.Status404NotFound)]
            public ActionResult<IGenericRepository<ArretDto>> GetAll()
            {

                try
                {
                    var p = genericRepository.GetAll();
                    if (p == null)
                     {
                         LoggerProject.WriteLogWarningLog($"Not found ");
                         return NotFound();
                     }
                      
                    else
                    {
                        return Ok(p);
                    }

                }
                catch (Exception ex)
                {
                      LoggerProject.WriteLogErrorLog($"Error GetAll() - Arret "); 
                       return BadRequest(ex);
                }


            }


            // GET api/<ProjectController>/5
            [HttpGet("id")]
            [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ArretDto))]
            [ProducesResponseType(StatusCodes.Status404NotFound)]
            public ActionResult<ArretDto> Get([FromQuery] Guid id)
            {

                try
                {
                    var p = genericRepository.Get(id);

                    if (p == null)
                    {
                         LoggerProject.WriteLogWarningLog($"Not found Id = {id} - Get Arret ");
                        return NotFound();
                    }
                     
                    else
                    {
                        var mapped = _mapper.Map<ArretDto>(p);
                        return Ok(mapped);
                    }


                }
                catch (Exception ex)
                {
                 LoggerProject.WriteLogErrorLog($"Error Get() - Arret ");
                 return BadRequest();
                }

            }


        [HttpPost]
        public ActionResult Add(ArretDto entity)
        {
            try
            {

                var mapped = _mapper.Map<Arret>(entity);
                genericRepository.Add(mapped);
               return Ok();

            }catch(Exception ex)
            {
                LoggerProject.WriteLogErrorLog($"Error Add() - Arret - {entity.ToString()} ");
                return BadRequest();
            }
        }


        [HttpPut]
        public ActionResult Update(ArretDto entity)
        {
            try
            {
                var mapped = _mapper.Map<Arret>(entity);

                genericRepository.Update(mapped);
                return Ok();

            }
            catch (Exception ex)
            {
                LoggerProject.WriteLogErrorLog($"Error Update() - Arret ");
                return BadRequest();
            }

        }


        [HttpDelete]
        public ActionResult Delete(ArretDto entity)
        {
            try
            {

               var mapped = _mapper.Map<Arret>(entity);

               var result =  genericRepository.Delete(mapped);

               if(result == true)
                {
                    return Ok();
                }
                return BadRequest();

            }
            catch (Exception ex)
            {
                LoggerProject.WriteLogErrorLog($"Error Delete() - Arret ");
                return BadRequest();
            }
        }
    }
}
