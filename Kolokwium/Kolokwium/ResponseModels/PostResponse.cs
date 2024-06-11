namespace Kolokwium.ResponseModels;

public class PostResponse
{
    public List<Dane> data { get; set; }


    public class Dane
    {
        public int slotId { get; set; }
        public int itemId { get; set; }
        public int characterId { get; set; }
    }
}