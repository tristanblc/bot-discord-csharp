using ApiApplication.Model;
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

        private readonly MyContext _context;

        private readonly APIGenericRepository<Ligne> genericRepository;

        public LigneController(IMapper mapper, MyContext context, APIGenericRepository<Ligne> genericRepository)
        {
            _mapper = mapper;
            _context = context;
            this.genericRepository = genericRepository;
        }




        //// GET api/<ProjectController>/GetAll

        [HttpGet]
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


        // GET api/<ProjectController>/5
        [HttpGet]
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



        public ActionResult Add(ArretDto entity)
        {
            try
            {

                var mapped = _mapper.Map<Arret>(entity);
                genericRepository.Add(mapped);
                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

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
