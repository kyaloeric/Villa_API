using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Net.NetworkInformation;
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
        [ProducesResponseType(StatusCodes.Status200OK)]

        public ActionResult< IEnumerable<VillaDTO>> GetVillas()
        {
            return Ok(VillaStore.villaList);
          

        }

        [HttpGet("{id:int}", Name ="GetVilla")]
        // also you can explicitly state that the id is an integer like  
        //[HttpGet("{id:int}")]

        //to make the responses associated with status codes get documented


        //[ProducesResponseType(200)]
        //[ProducesResponseType(404)]
        //[ProducesResponseType(400)]

        //to avoid hardcoding the status codes 
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        //you can also add the type to be returned here [ProducesResponseType(200, Type =typeof(villaDTO))] and remove it at the actionresult 
        public ActionResult< VillaDTO> GetVilla(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var villa = VillaStore.villaList.FirstOrDefault(u => u.Id == id);

            if (villa == null)
            {
                return NotFound();
            }
            return Ok(villa);


        }


        [HttpPost]

        //response types
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<VillaDTO> CreateVilla([FromBody]VillaDTO villaDTO)
        {
            //add validations

            //make sure villa name is unique
            if(VillaStore.villaList.FirstOrDefault(u => u.Name.ToLower() == villaDTO.Name.ToLower())!= null ) 
            {
                ModelState.AddModelError("CustomError", "Villa already Exists!");
                return BadRequest(ModelState);
            
            }

             
            if (villaDTO == null)
            {
                return BadRequest(villaDTO);
            }
            if (villaDTO.Id > 0) 
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            
            
            }
            //retrieve max id and incrememnt by 1
            villaDTO.Id = VillaStore.villaList.OrderByDescending(u=>u.Id).FirstOrDefault().Id+1;
            // add to our data store
            VillaStore.villaList.Add(villaDTO);

            //if everything okay invoke the route it was created at using createdatroute and pass the VillaDTO that was created
            return CreatedAtRoute("GetVilla", new { id = villaDTO.Id }, villaDTO);
        }




       // [ProducesResponseType(StatusCodes.Status204NoContent)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        [HttpDelete("{id:int}", Name = "DeleteVilla")]

        public IActionResult DeleteVilla(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var villa = VillaStore.villaList.FirstOrDefault(u => u.Id == id);
            if (villa == null)
            {
                return NotFound();
            }
            VillaStore.villaList.Remove(villa);

            return NoContent();
        }



        [HttpPut("{id:int}", Name = "UpdateVilla")]

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateVilla(int id, [FromBody]VillaDTO villaDTO)
        {
            if (villaDTO == null || id != villaDTO.Id )
            {
                return BadRequest();
            }
            var villa = VillaStore.villaList.FirstOrDefault(u => u.Id == id);
            villa.Name =villaDTO.Name;
            villa.Sqft = villaDTO.Sqft;
            villa.Occupancy = villaDTO.Occupancy;

            return NoContent();



        }



        [HttpPatch("{id:int}", Name = "UpdatePartiaVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]


        public IActionResult UpdatePartialVilla(int id, JsonPatchDocument<VillaDTO> patchDTO) 
        {
            if (patchDTO == null || id== 0)
            {
                return BadRequest();
            }
            var villa = VillaStore.villaList.FirstOrDefault(u => u.Id == id);
            if (villa == null)
            {
                return BadRequest();
            }
            patchDTO.ApplyTo(villa, ModelState);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return NoContent();

        
        
        }


        public IActionResult UpdatePartialVi(int id, JsonPatchDocument<VillaDTO> patchDTO)
        {
            if (patchDTO == null || id == 0)
            {
                return BadRequest();
            }
            var villa = VillaStore.villaList.FirstOrDefault(u => u.Id == id);
            if (villa == null)
            {
                return BadRequest();
            }
            patchDTO.ApplyTo(villa, ModelState);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return NoContent();



        }



    }
}
  