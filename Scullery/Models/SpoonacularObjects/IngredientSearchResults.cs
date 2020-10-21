
public class IngredientSearchResults
{
    public IngredientResult[] results { get; set; }
}

public class IngredientResult
{
    public string name { get; set; }
    public string image { get; set; }
    public int id { get; set; }
    public string aisle { get; set; }
    public string[] possibleUnits { get; set; }
}

