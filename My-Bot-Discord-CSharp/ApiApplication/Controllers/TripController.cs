using ApiApplication.Repository;
using ApiApplication.Repository.Interface;
using AutoMapper;
using BotDTOClassLibrary;
using BusClassLibrary;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TripController : Controller
    {
        private readonly IMapper _mapper;

        private readonly ApplicationDbContext _context;

        private APIGenericRepository<Trip> genericRepository { get; set; }

        public TripController(IMapper mapper, ApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
            this.genericRepository = new APIGenericRepository<Trip>(context);
        }




        //// GET api/<ProjectController>/GetAll

        [HttpGet("all")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TripDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IGenericRepository<TripDto>> GetAll()
        {

            try
            {
                var p = genericRepository.GetAll();
                if (p == null)
                    return NotFound();
                else
                {
                    return Ok(p);
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }


        }


        // GET api/<ProjectController>/5
        [HttpGet("id")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TripDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<TripDto> Get([FromQuery] Guid id)
        {

            try
            {
                var p = genericRepository.Get(id);

                if (p == null)
                    return NotFound();
                else
                {
                    var mapped = _mapper.Map<Trip>(p);
                    return Ok(mapped);
                }


            }
            catch (Exception ex)
            {
                return BadRequest();
            }

        }


        [HttpPost]
        public ActionResult Add(TripDto entity)
        {
            try
            {

                var mapped = _mapper.Map<Trip>(entity);
                genericRepository.Add(mapped);
                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }


        [HttpPut]
        public ActionResult Update(TripDto entity)
        {
            try
            {
                var mapped = _mapper.Map<Trip>(entity);

                genericRepository.Update(mapped);
                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest();
            }

        }


        [HttpDelete]
        public ActionResult Delete(TripDto entity)
        {
            try
            {

                var mapped = _mapper.Map<Trip>(entity);

                var result = genericRepository.Delete(mapped);
                if (result == true)
                {
                    return Ok();
                }
                return BadRequest();

            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
    }
}
