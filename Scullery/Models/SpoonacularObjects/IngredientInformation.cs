
public class IngredientInformation
{
    public int id { get; set; }
    public string original { get; set; }
    public string originalName { get; set; }
    public string name { get; set; }
    public float amount { get; set; }
    public string unit { get; set; }
    public string unitShort { get; set; }
    public string unitLong { get; set; }
    public string[] possibleUnits { get; set; }
    public Estimatedcost estimatedCost { get; set; }
    public string consistency { get; set; }
    public string[] shoppingListUnits { get; set; }
    public string aisle { get; set; }
    public string image { get; set; }
    public object[] meta { get; set; }
    public Nutrition nutrition { get; set; }
    public string[] categoryPath { get; set; }
}

public class Estimatedcost
{
    public float value { get; set; }
    public string unit { get; set; }
}

public class Nutrition
{
    public Nutrient[] nutrients { get; set; }
    public Properties[] properties { get; set; }
    public Caloricbreakdown caloricBreakdown { get; set; }
    public Weightperserving weightPerServing { get; set; }
}

public class Caloricbreakdown
{
    public float percentProtein { get; set; }
    public float percentFat { get; set; }
    public float percentCarbs { get; set; }
}

public class Weightperserving
{
    public int amount { get; set; }
    public string unit { get; set; }
}

public class Nutrient
{
    public string title { get; set; }
    public float amount { get; set; }
    public string unit { get; set; }
    public float percentOfDailyNeeds { get; set; }
}

public class Properties
{
    public string title { get; set; }
    public float amount { get; set; }
    public string unit { get; set; }
}
