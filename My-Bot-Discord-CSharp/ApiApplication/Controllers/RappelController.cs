﻿using ApiApplication.Repository;
using ApiApplication.Repository.Interface;
using AutoMapper;
using BotClassLibrary;
using Microsoft.AspNetCore.Mvc;
using ServiceClassLibrary.Interfaces;
using ServiceClassLibrary.Services;

namespace ApiApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RappelController : Controller
    {
        private readonly IMapper _mapper;

        private readonly ApplicationDbContext _context;

        private APIGenericRepository<Rappel> GenericRepository { get; set; }

        private ILoggerProject LoggerProject { get; init; }

        public RappelController(IMapper mapper, ApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
            GenericRepository = new APIGenericRepository<Rappel>(context);
            LoggerProject = new LoggerProject();
        }




        //// GET api/<ProjectController>/GetAll

        [HttpGet("all")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Rappel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IGenericRepository<Rappel>> GetAll()
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
                LoggerProject.WriteLogErrorLog($"Error GetAll() - Rappel ");
                return BadRequest(ex);
            }


        }


        // GET api/<ProjectController>/5
        [HttpGet("id")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Rappel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Rappel> Get([FromQuery] Guid id)
        {

            try
            {
                var p = GenericRepository.Get(id);

                if (p == null)
                    return NotFound();
                else
                {
                    var mapped = _mapper.Map<Rappel>(p);
                    return Ok(mapped);
                }


            }
            catch (Exception ex)
            {
                LoggerProject.WriteLogErrorLog($"Error Get() - Rappel-  id : {id.ToString()} ");
                return BadRequest();
            }

        }


        [HttpPost]
        public ActionResult Add(Rappel entity)
        {
            try
            {

                var mapped = _mapper.Map<Rappel>(entity);
                GenericRepository.Add(mapped);
                return Ok();

            }
            catch (Exception ex)
            {
                LoggerProject.WriteLogErrorLog($"Error Add() -  Rappel - {entity.ToString()} ");
                return BadRequest();
            }
        }


        [HttpPut]
        public ActionResult Update(Rappel entity)
        {
            try
            {
                var mapped = _mapper.Map<Rappel>(entity);

                GenericRepository.Update(mapped);
                return Ok();

            }
            catch (Exception ex)
            {
                LoggerProject.WriteLogErrorLog($"Error update() - Rappel - {entity.ToString()} ");
                return BadRequest();
            }

        }


        [HttpDelete]
        public ActionResult Delete(Rappel entity)
        {
            try
            {

                var mapped = _mapper.Map<Rappel>(entity);

                var result = GenericRepository.Delete(mapped);
                if (result == true)
                {
                    return Ok();
                }
                return BadRequest();

            }
            catch (Exception ex)
            {
                LoggerProject.WriteLogErrorLog($"Error delete() - Rappel - {entity.ToString()} ");
                return BadRequest();
            }
        }

    }
}
