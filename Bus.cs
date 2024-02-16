

public class Bus : Vehicle
{
    public int PassengerCapacity { get; set; }


    public Bus(int id, string brand, string model, int year, double price, TechnicalCondition condition, string drivingLicenseCategory, int passengerCapacity)
        : base(id, brand, model, year, price, condition, drivingLicenseCategory)
    {
        PassengerCapacity = passengerCapacity;
    }
}