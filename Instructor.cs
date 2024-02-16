
public class Instructor
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; } 
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public int Salary { get; set; }



    public Instructor(int id, string firstName, string lastName, DateTime dateOfBirth, string phoneNumber, string email, int salary)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        DateOfBirth = dateOfBirth;
        PhoneNumber = phoneNumber;
        Email = email;
        Salary = salary;
    }
}