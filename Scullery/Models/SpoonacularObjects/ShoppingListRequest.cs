
public class ShoppingListRequest
{
    public Aisle[] aisles { get; set; }
    public float cost { get; set; }
    public int startDate { get; set; }
    public int endDate { get; set; }
}

public class Aisle
{
    public string aisle { get; set; }
    public Item[] items { get; set; }
}

public class Item
{
    public int id { get; set; }
    public string name { get; set; }
    public Measures[] measures { get; set; }
    public string[] usages { get; set; }
    public bool pantryItem { get; set; }
    public string aisle { get; set; }
    public float cost { get; set; }
    public int ingredientId { get; set; }
}





