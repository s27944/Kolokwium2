namespace Kolokwium.ResponseModels;

public class Response
{
    public string firstName { get; set; }
    public string lastName { get; set; }
    public int currentWeight { get; set; }
    public int maxWeight { get; set; }
    public int money { get; set; }
    
    public List<Backpack> backpack { get; set; }
    public List<Tytuly> tytuly { get; set; }


    public class Backpack
    {
        public int slotId { get; set; }
        public string itemName { get; set; }
        public int itemWeight { get; set; }
    }

    public class Tytuly
    {
        public string title { get; set; }
        public DateTime aquireAt { get; set; }
    }
}