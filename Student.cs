using System;

    public class Student
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string DrivingLicenseCategory { get; set; }
        public bool FinishedCourse { get; set; }

        public Student(int id, string firstName, string lastName, string phoneNumber, string email, string drivingLicenseCategory)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            PhoneNumber = phoneNumber;
            Email = email;
            DrivingLicenseCategory = drivingLicenseCategory;
            FinishedCourse = false; 
        }

        public void FinishCourse()
        {
            FinishedCourse = true;
        }
    }

