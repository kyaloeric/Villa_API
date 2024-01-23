using VillaAPI.Models;

namespace VillaAPI.Data
{
    public static class VillaStore
    {
        public static List<VillaDTO> villaList = new List<VillaDTO>   {
                new VillaDTO { Id = 1, Name="Pool View", Sqft=200,Occupancy=5},
                new VillaDTO { Id = 2, Name="Mind View", Sqft=100, Occupancy=4}
            };

    }
}
