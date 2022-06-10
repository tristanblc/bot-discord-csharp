using ApiApplication.Repository.Interface;
using Microsoft.AspNetCore.Mvc;

namespace ApiApplication.Controllers.Interfaces
{
    public interface IGenericController<Tdo> where Tdo : class
    {
        ActionResult<Tdo> Get([FromQuery] Guid id);
        ActionResult Update(Tdo dto);
        ActionResult Add(Tdo addressDto);

        ActionResult Delete([FromQuery] Guid id);

    }
}
