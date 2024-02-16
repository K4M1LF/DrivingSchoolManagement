

public class Car : Vehicle
{
    public int NumberOfPassengers { get; set; }

    public Car(int id, string brand, string model, int year, double price, TechnicalCondition condition, string drivingLicenseCategory, int numberOfPassengers)
        : base(id, brand, model, year, price, condition, drivingLicenseCategory)
    {
        NumberOfPassengers = numberOfPassengers;
    }
}