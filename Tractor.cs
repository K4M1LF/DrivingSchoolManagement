

public class Tractor : Vehicle
{
    public string Purpose { get; set; }


    public Tractor(int id, string brand, string model, int year, double price, TechnicalCondition condition, string drivingLicenseCategory, string purpose)
        : base(id, brand, model, year, price, condition, drivingLicenseCategory)
    {
        Purpose = purpose;
    }
}