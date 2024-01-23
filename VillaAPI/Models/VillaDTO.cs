using System.ComponentModel.DataAnnotations;

namespace VillaAPI.Models
{
    public class VillaDTO
    {
        public int Id { get; set; }


        //add a required data annotation NB: annotations are added at the model class


        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        public DateTime CreatedDate { get; set; }

        public int Occupancy { get; set; }

        public int Sqft { get; set;}

    }
}
