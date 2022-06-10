using ApiApplication.Controllers.Interfaces;
using ApiApplication.Repository;
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
    public class TicketController : Controller, IGenericController<Ticket>
    {
        private readonly IMapper _mapper;

        private readonly ApplicationDbContext _context;

        private APIGenericRepository<Ticket> GenericRepository { get; set; }

        private ILoggerProject LoggerProject { get; init; }

        public TicketController(IMapper mapper, ApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
            GenericRepository = new APIGenericRepository<Ticket>(context);
            LoggerProject = new LoggerProject();
        }




        //// GET api/<ProjectController>/GetAll
        [Authorize]
        [HttpGet("all")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Ticket))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IGenericRepository<Ticket>> GetAll()
        {

            try
            {
                var p = GenericRepository.GetAll();
                if (p == null)
                    return NotFound();
                else
                {
                    return Ok(p);
                }

            }
            catch (Exception ex)
            {
                LoggerProject.WriteLogErrorLog($"Error GetAll() -Ticket ");
                return BadRequest(ex);
            }


        }

        [Authorize]
        // GET api/<ProjectController>/5
        [HttpGet("id")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Ticket))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Ticket> Get([FromQuery] Guid id)
        {

            try
            {
                var p = GenericRepository.Get(id);

                if (p == null)
                    return NotFound();
                else
                {
                    var mapped = _mapper.Map<Ticket>(p);
                    return Ok(mapped);
                }


            }
            catch (Exception ex)
            {
                LoggerProject.WriteLogErrorLog($"Error Get() - Ticket -  id : {id.ToString()} ");
                return BadRequest();
            }

        }

        [Authorize]
        [HttpPost]
        public ActionResult Add(Ticket entity)
        {
            try
            {

                var mapped = _mapper.Map<Ticket>(entity);
                GenericRepository.Add(mapped);
                return Ok();

            }
            catch (Exception ex)
            {
                LoggerProject.WriteLogErrorLog($"Error Add() -Ticket - {entity.ToString()} ");
                return BadRequest();
            }
        }

        [Authorize]
        [HttpPut]
        public ActionResult Update(Ticket entity)
        {
            try
            {
                var mapped = _mapper.Map<Ticket>(entity);

                GenericRepository.Update(mapped);
                return Ok();

            }
            catch (Exception ex)
            {
                LoggerProject.WriteLogErrorLog($"Error update() - Ticket - {entity.ToString()} ");
                return BadRequest();
            }

        }

        [Authorize]
        [HttpDelete]
        public ActionResult Delete([FromQuery] Guid id)
        {
            try
            {
   

               

                var result = GenericRepository.Delete(id);
                if (result == true)
                {
                    return Ok();
                }
                return BadRequest();

            }
            catch (Exception ex)
            {
                LoggerProject.WriteLogErrorLog($"Error delete() - delete  ");
                return BadRequest();
            }
        }
    }
}
