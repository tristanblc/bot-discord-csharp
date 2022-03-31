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
    public class ShapeController : Controller
    {
        private readonly IMapper _mapper;

        private readonly ApplicationDbContext _context;

        private APIGenericRepository<Shape> genericRepository { get; set; }

        public ShapeController(IMapper mapper, ApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
            this.genericRepository = new APIGenericRepository<Shape>(context);
        }




        //// GET api/<ProjectController>/GetAll

        [HttpGet("all")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ShapeDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IGenericRepository<ShapeDto>> GetAll()
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ShapeDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<ShapeDto> Get([FromQuery] Guid id)
        {

            try
            {
                var p = genericRepository.Get(id);

                if (p == null)
                    return NotFound();
                else
                {
                    var mapped = _mapper.Map<Shape>(p);
                    return Ok(mapped);
                }


            }
            catch (Exception ex)
            {
                return BadRequest();
            }

        }


        [HttpPost]
        public ActionResult Add(ShapeDto entity)
        {
            try
            {

                var mapped = _mapper.Map<Shape>(entity);
                genericRepository.Add(mapped);
                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }


        [HttpPut]
        public ActionResult Update(ShapeDto entity)
        {
            try
            {
                var mapped = _mapper.Map<Shape>(entity);

                genericRepository.Update(mapped);
                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest();
            }

        }


        [HttpDelete]
        public ActionResult Delete(ShapeDto entity)
        {
            try
            {

                var mapped = _mapper.Map<Shape>(entity);

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
