using ApiApplication.Model;
using ApiApplication.Repository;
using ApiApplication.Repository.Interface;
using AutoMapper;
using BotDTOClassLibrary;
using BusClassLibrary;
using Microsoft.AspNetCore.Mvc;

namespace ApiApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArretController : Controller
    {
            private readonly IMapper _mapper;

            private readonly  MyContext _context;

            private readonly APIGenericRepository<Arret> genericRepository;

            public ArretController(IMapper mapper, MyContext context, APIGenericRepository<Arret>  genericRepository)
            {
                _mapper = mapper;
                _context = context;
                this.genericRepository = genericRepository;
            }




            //// GET api/<ProjectController>/GetAll

            [HttpGet]
            [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ArretDto))]
            [ProducesResponseType(StatusCodes.Status404NotFound)]
            public ActionResult<IGenericRepository<ArretDto>> GetAll()
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
            [HttpGet]
            [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ArretDto))]
            [ProducesResponseType(StatusCodes.Status404NotFound)]
            public ActionResult<ArretDto> Get([FromQuery] Guid id)
            {

                try
                {
                    var p = genericRepository.Get(id);

                    if (p == null)
                        return NotFound();
                    else
                    {
                        var mapped = _mapper.Map<ArretDto>(p);
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

            }catch(Exception ex)
            {
                return BadRequest();
            }
        }

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
                return BadRequest();
            }

        }

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
                return BadRequest();
            }
        }
    }
}
