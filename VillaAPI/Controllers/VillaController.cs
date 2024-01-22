using Microsoft.AspNetCore.Mvc;
using VillaAPI.Data;
using VillaAPI.Models;

namespace VillaAPI.Controllers

{
    //also you can use [Route("api/[controller]")]
    [Route("api/VillaAPI")]
    [ApiController]
    public class VillaController : ControllerBase
    {
        [HttpGet]
        public ActionResult< IEnumerable<VillaDTO>> GetVillas()
        {
            return Ok(VillaStore.villaList);
          

        }

        [HttpGet("{id:int}")]
        // also you can explicitly state that the id is an integer like  
        //[HttpGet("{id:int}")]

        public ActionResult< VillaDTO> GetVilla(int id)
        {
            return Ok(VillaStore.villaList.FirstOrDefault(u => u.Id == id));


        }
    }
}
  