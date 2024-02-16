
public class Motorcycle : Vehicle
{
    public string Type { get; set; }


    public Motorcycle(int id, string brand, string model, int year, double price, TechnicalCondition condition, string drivingLicenseCategory, string type)
        : base(id, brand, model, year, price, condition, drivingLicenseCategory)
    {
        Type = type;
    }
}