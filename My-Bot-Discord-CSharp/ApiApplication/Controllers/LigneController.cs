using ApiApplication;
using ApiApplication.Repository;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BusClassLibrary;
using BotDTOClassLibrary;
using ApiApplication.Repository.Interface;

namespace ApiApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LigneController : Controller
    {
        private readonly IMapper _mapper;


        private readonly APIGenericRepository<Ligne> genericRepository;

        public LigneController(IMapper mapper, ApplicationDbContext context)
        {
            _mapper = mapper;
            this.genericRepository = new APIGenericRepository<Ligne>(context);
        }

        //// GET api/<ProjectController>/GetAll

        [HttpGet("all")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(LigneDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IGenericRepository<LigneDto>> GetAll()
        {

            try
            {
                var p = genericRepository.GetAll();

                
                if (p == null)
                    return NotFound();
                else
                {
                    var mapped = _mapper.Map<IEnumerable<LigneDto>>(p);
                    return Ok(p);
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }


        }


        // GET api/<ProjectController>/id
        [HttpGet("id")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(LigneDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<LigneDto> Get([FromQuery] Guid id)
        {

            try
            {
                var p = genericRepository.Get(id);

                if (p == null)
                    return NotFound();
                else
                {
                    var mapped = _mapper.Map<LigneDto>(p);
                    return Ok(mapped);
                }


            }
            catch (Exception ex)
            {
                return BadRequest();
            }

        }


        [HttpPost]
        public ActionResult Add(LigneDto entity)
        {
         

                var mapped = _mapper.Map<Ligne>(entity);
                genericRepository.Add(mapped);
                return Ok();

          
        }

        [HttpPut]
        public ActionResult Update(ArretDto entity)
        {
            try
            {
                var mapped = _mapper.Map<Ligne>(entity);

                genericRepository.Update(mapped);
                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest();
            }

        }

        [HttpDelete]
        public ActionResult Delete(ArretDto entity)
        {
            try
            {

                var mapped = _mapper.Map<Ligne>(entity);

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
