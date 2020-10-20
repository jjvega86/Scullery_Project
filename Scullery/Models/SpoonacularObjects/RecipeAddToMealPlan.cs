
public class RecipeAddToMealPlan
{
    public int date { get; set; }
    public int slot { get; set; }
    public int position { get; set; }
    public string type { get; set; }
    public Value value { get; set; }
}

public class Value
{
    public int id { get; set; }
    public int servings { get; set; }
    public string title { get; set; }
    public string imageType { get; set; }
}

