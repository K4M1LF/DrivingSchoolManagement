using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using System;

namespace DrivingSchoolManagement
{


    public class DatabaseManager
    {
        private static DatabaseManager instance;
        private readonly string connectionString;

        private DatabaseManager(string host, string database, string username, string password)
        {
            connectionString = $"Host={host};Database={database};Username={username};Password={password}";
        }

        public static DatabaseManager GetInstance(string host, string database, string username, string password)
        {
            if (instance == null)
            {
                instance = new DatabaseManager(host, database, username, password);
            }
            return instance;
        }

        public void ConnectAndDoSomething()
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    MessageBox.Show("Połączono z Bazą!");

                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Błąd połączenia z bazą danych: {ex.Message}");
                }
            }
        }

        public void AddInstructor(Instructor instructor)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                using (NpgsqlCommand cmd = new NpgsqlCommand())
                {
                    cmd.Connection = connection;
                    cmd.CommandText = "INSERT INTO Instructors (FirstName, LastName, DateOfBirth, PhoneNumber, Email, Salary)" +
                        " VALUES (@FirstName, @LastName, @DateOfBirth, @PhoneNumber, @Email, @Salary)";
                    cmd.Parameters.AddWithValue("@FirstName", instructor.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", instructor.LastName);
                    cmd.Parameters.AddWithValue("@DateOfBirth", instructor.DateOfBirth);
                    cmd.Parameters.AddWithValue("@PhoneNumber", instructor.PhoneNumber);
                    cmd.Parameters.AddWithValue("@Email", instructor.Email);
                    cmd.Parameters.AddWithValue("@Salary", instructor.Salary);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<Instructor> GetInstructorsFromDatabase()
        {
            List<Instructor> instructors = new List<Instructor>();

            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                using (NpgsqlCommand cmd = new NpgsqlCommand())
                {
                    cmd.Connection = connection;
                    cmd.CommandText = "SELECT * FROM Instructors";

                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = reader.GetInt32(0);
                            string firstName = reader.GetString(1);
                            string lastName = reader.GetString(2);
                            DateTime dateOfBirth = reader.GetDateTime(3);
                            string phoneNumber = reader.GetString(4);
                            string email = reader.GetString(5);
                            int salary = reader.GetInt32(6);

                            Instructor instructor = new Instructor(id, firstName, lastName, dateOfBirth, phoneNumber, email, salary);
                            instructors.Add(instructor);
                        }
                    }
                }
            }

            return instructors;
        }

        public void UpdateInstructor(Instructor instructor)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                using (NpgsqlCommand cmd = new NpgsqlCommand())
                {
                    cmd.Connection = connection;
                    cmd.CommandText = "UPDATE Instructors SET FirstName = @FirstName, LastName = @LastName, DateOfBirth" +
                        " = @DateOfBirth, PhoneNumber = @PhoneNumber, Email = @Email, Salary = @Salary WHERE Id = @Id";

                    cmd.Parameters.AddWithValue("@Id", instructor.Id);
                    cmd.Parameters.AddWithValue("@FirstName", instructor.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", instructor.LastName);
                    cmd.Parameters.AddWithValue("@DateOfBirth", instructor.DateOfBirth);
                    cmd.Parameters.AddWithValue("@PhoneNumber", instructor.PhoneNumber);
                    cmd.Parameters.AddWithValue("@Email", instructor.Email);
                    cmd.Parameters.AddWithValue("@Salary", instructor.Salary);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void RemoveInstructor(int instructorId)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                using (NpgsqlCommand cmd = new NpgsqlCommand())
                {
                    cmd.Connection = connection;
                    cmd.CommandText = "DELETE FROM Instructors WHERE Id = @Id";
                    cmd.Parameters.AddWithValue("@Id", instructorId);

                    cmd.ExecuteNonQuery();
                }
            }
        }


        public void AddStudent(Student student)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                using (NpgsqlCommand cmd = new NpgsqlCommand())
                {
                    cmd.Connection = connection;
                    cmd.CommandText = "INSERT INTO Students (FirstName, LastName, PhoneNumber, Email, DrivingLicenseCategory, FinishedCourse) VALUES (@FirstName, @LastName, @PhoneNumber, @Email, @DrivingLicenseCategory, @FinishedCourse)";
                    cmd.Parameters.AddWithValue("@FirstName", student.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", student.LastName);
                    cmd.Parameters.AddWithValue("@PhoneNumber", student.PhoneNumber);
                    cmd.Parameters.AddWithValue("@Email", student.Email);
                    cmd.Parameters.AddWithValue("@DrivingLicenseCategory", student.DrivingLicenseCategory);
                    cmd.Parameters.AddWithValue("@FinishedCourse", student.FinishedCourse);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<Student> GetStudentsFromDatabase()
        {
            List<Student> students = new List<Student>();

            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                using (NpgsqlCommand cmd = new NpgsqlCommand())
                {
                    cmd.Connection = connection;
                    cmd.CommandText = "SELECT * FROM Students";

                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = reader.GetInt32(0);
                            string firstName = reader.GetString(1);
                            string lastName = reader.GetString(2);
                            string phoneNumber = reader.GetString(3);
                            string email = reader.GetString(4);
                            string drivingLicenseCategory = reader.GetString(5);
                            bool finishedCourse = reader.GetBoolean(6);

                            Student student = new Student(id, firstName, lastName, phoneNumber, email, drivingLicenseCategory)
                            {
                                FinishedCourse = finishedCourse
                            };
                            students.Add(student);
                        }
                    }
                }
            }

            return students;
        }


        public void UpdateStudent(Student student)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                using (NpgsqlCommand cmd = new NpgsqlCommand())
                {
                    cmd.Connection = connection;
                    cmd.CommandText = "UPDATE Students SET FirstName = @FirstName, LastName = @LastName, PhoneNumber =" +
                        " @PhoneNumber, Email = @Email, DrivingLicenseCategory = @DrivingLicenseCategory, FinishedCourse = @FinishedCourse WHERE Id = @Id";

                    cmd.Parameters.AddWithValue("@Id", student.Id);
                    cmd.Parameters.AddWithValue("@FirstName", student.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", student.LastName);
                    cmd.Parameters.AddWithValue("@PhoneNumber", student.PhoneNumber);
                    cmd.Parameters.AddWithValue("@Email", student.Email);
                    cmd.Parameters.AddWithValue("@DrivingLicenseCategory", student.DrivingLicenseCategory);
                    cmd.Parameters.AddWithValue("@FinishedCourse", student.FinishedCourse);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void RemoveStudent(int studentId)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                using (NpgsqlCommand cmd = new NpgsqlCommand())
                {
                    cmd.Connection = connection;
                    cmd.CommandText = "DELETE FROM Students WHERE Id = @Id";
                    cmd.Parameters.AddWithValue("@Id", studentId);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void AddVehicle(Vehicle vehicle)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                using (NpgsqlCommand cmd = new NpgsqlCommand())
                {
                    cmd.Connection = connection;
                    cmd.CommandText = "INSERT INTO Vehicles (Brand, Model, Year, Price, DrivingLicenseCategory, Condition) VALUES (@Brand, @Model, @Year, @Price, @DrivingLicenseCategory, @Condition)";
                    cmd.Parameters.AddWithValue("@Brand", vehicle.Brand);
                    cmd.Parameters.AddWithValue("@Model", vehicle.Model);
                    cmd.Parameters.AddWithValue("@Year", vehicle.Year);
                    cmd.Parameters.AddWithValue("@Price", vehicle.Price);
                    cmd.Parameters.AddWithValue("@DrivingLicenseCategory", vehicle.DrivingLicenseCategory);
                    cmd.Parameters.AddWithValue("@Condition", (int)vehicle.Condition);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<Vehicle> GetVehiclesFromDatabase()
        {
            List<Vehicle> vehicles = new List<Vehicle>();

            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                using (NpgsqlCommand cmd = new NpgsqlCommand())
                {
                    cmd.Connection = connection;
                    cmd.CommandText = "SELECT * FROM Vehicles";

                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = reader.GetInt32(0);
                            string brand = reader.GetString(1);
                            string model = reader.GetString(2);
                            int year = reader.GetInt32(3);
                            double price = reader.GetDouble(4);
                            string drivingLicenseCategory = reader.GetString(5);
                            TechnicalCondition condition = (TechnicalCondition)Enum.Parse(typeof(TechnicalCondition), reader.GetString(6));

                            Vehicle vehicle = new Vehicle(id, brand, model, year, price, condition, drivingLicenseCategory);
                            vehicles.Add(vehicle);
                        }
                    }
                }
            }

            return vehicles;
        }

        public void UpdateVehicle(Vehicle vehicle)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                using (NpgsqlCommand cmd = new NpgsqlCommand())
                {
                    cmd.Connection = connection;
                    cmd.CommandText = "UPDATE Vehicles SET Brand = @Brand, Model = @Model, Year = @Year, Price = @Price, DrivingLicenseCategory = @DrivingLicenseCategory, Condition = @Condition WHERE Id = @Id";

                    cmd.Parameters.AddWithValue("@Id", vehicle.Id);
                    cmd.Parameters.AddWithValue("@Brand", vehicle.Brand);
                    cmd.Parameters.AddWithValue("@Model", vehicle.Model);
                    cmd.Parameters.AddWithValue("@Year", vehicle.Year);
                    cmd.Parameters.AddWithValue("@Price", vehicle.Price);
                    cmd.Parameters.AddWithValue("@DrivingLicenseCategory", vehicle.DrivingLicenseCategory);
                    cmd.Parameters.AddWithValue("@Condition", (int)vehicle.Condition);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void RemoveVehicle(int vehicleId)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                using (NpgsqlCommand cmd = new NpgsqlCommand())
                {
                    cmd.Connection = connection;
                    cmd.CommandText = "DELETE FROM Vehicles WHERE Id = @Id";
                    cmd.Parameters.AddWithValue("@Id", vehicleId);

                    cmd.ExecuteNonQuery();
                }
            }
        }


        public void AddIncident(int vehicleId, Incident incident)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                using (NpgsqlCommand cmd = new NpgsqlCommand())
                {
                    cmd.Connection = connection;
                    cmd.CommandText = "INSERT INTO Incidents (VehicleId, Date, Description, RepairCost, IsRepaired) " +
                                      "VALUES (@VehicleId, @Date, @Description, @RepairCost, @IsRepaired)";
                    cmd.Parameters.AddWithValue("@VehicleId", vehicleId);
                    cmd.Parameters.AddWithValue("@Date", incident.Date);
                    cmd.Parameters.AddWithValue("@Description", incident.Description);
                    cmd.Parameters.AddWithValue("@RepairCost", incident.RepairCost);
                    cmd.Parameters.AddWithValue("@IsRepaired", incident.IsRepaired);

                    cmd.ExecuteNonQuery();
                }
            }
        }
        public List<Incident> GetIncidentsForVehicle(int vehicleId)
        {
            List<Incident> incidents = new List<Incident>();

            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                using (NpgsqlCommand cmd = new NpgsqlCommand())
                {
                    cmd.Connection = connection;
                    cmd.CommandText = "SELECT * FROM Incidents WHERE VehicleId = @VehicleId";
                    cmd.Parameters.AddWithValue("@VehicleId", vehicleId);

                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = reader.GetInt32(0);
                            int vehId = reader.GetInt32(1);
                            DateTime date = reader.GetDateTime(2);
                            string description = reader.GetString(3);
                            int repairCost = reader.GetInt32(4);
                            bool isRepaired = reader.GetBoolean(5);


                            Incident incident = new Incident(id, vehId, date, description, repairCost, isRepaired);
                            incidents.Add(incident);
                        }
                    }
                }
            }

            return incidents;
        }



        public List<TheoreticalLecture> GetTheoreticalLecturesFromDatabase()
        {
            List<TheoreticalLecture> theoreticalLectures = new List<TheoreticalLecture>();

            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                using (NpgsqlCommand cmd = new NpgsqlCommand())
                {
                    cmd.Connection = connection;
                    cmd.CommandText = "SELECT * FROM TheoreticalLectures";

                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = reader.GetInt32(0);
                            int instructorId = reader.GetInt32(1);
                            DateTime startDate = reader.GetDateTime(2);
                            TimeSpan durationPerLecture = reader.GetTimeSpan(3);
                            int numberOfLectures = reader.GetInt32(4);
                            TimeSpan lectureStartTime = reader.GetTimeSpan(5);
                            TimeSpan lectureEndTime = reader.GetTimeSpan(6);

                            Instructor instructor;
                            foreach (Instructor ins in AppManager.GetInstance().Instructors)
                            {
                                if (ins.Id == instructorId)
                                {
                                    instructor = ins;
                                    TheoreticalLecture theoreticalLecture = new TheoreticalLecture(id, instructor, startDate);
                                    theoreticalLectures.Add(theoreticalLecture);
                                }
                            }
                        }
                    }
                }
            }

            return theoreticalLectures;
        }

        public Dictionary<Instructor, List<PracticalDriving>> GetPracticalDrivingsFromDatabase()
        {
            Dictionary<Instructor, List<PracticalDriving>> practicalDrivings = new Dictionary<Instructor, List<PracticalDriving>>();

            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                using (NpgsqlCommand cmd = new NpgsqlCommand())
                {
                    cmd.Connection = connection;
                    cmd.CommandText = "SELECT * FROM PracticalDrivings";

                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = reader.GetInt32(0);
                            int studentId = reader.GetInt32(1);
                            int instructorId = reader.GetInt32(2);
                            int vehicleId = reader.GetInt32(3);
                            DateTime startTime = reader.GetDateTime(4);

                            Student student = GetStudentById(studentId);
                            Instructor instructor = GetInstructorById(instructorId);
                            Vehicle vehicle = GetVehicleById(vehicleId);

                            PracticalDriving practicalDriving = PracticalDriving.Create(id, student, instructor, vehicle, startTime);

                            if (practicalDrivings.ContainsKey(instructor))
                            {
                                practicalDrivings[instructor].Add(practicalDriving);
                            }
                            else
                            {
                                List<PracticalDriving> instructorDrivings = new List<PracticalDriving> { practicalDriving };
                                practicalDrivings.Add(instructor, instructorDrivings);
                            }
                        }
                    }
                }
            }
            return practicalDrivings;
        }

        public void AddPracticalDriving(PracticalDriving practicalDriving)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                using (NpgsqlCommand cmd = new NpgsqlCommand())
                {
                    cmd.Connection = connection;
                    cmd.CommandText = "INSERT INTO PracticalDrivings (StudentId, InstructorId, VehicleId, StartTime, EndTime) " +
                                      "VALUES (@StudentId, @InstructorId, @VehicleId, @StartTime, @EndTime)";

                    cmd.Parameters.AddWithValue("@StudentId", practicalDriving.Student.Id);
                    cmd.Parameters.AddWithValue("@InstructorId", practicalDriving.Instructor.Id);
                    cmd.Parameters.AddWithValue("@VehicleId", practicalDriving.Vehicle.Id);
                    cmd.Parameters.AddWithValue("@StartTime", practicalDriving.StartTime);
                    cmd.Parameters.AddWithValue("@EndTime", practicalDriving.EndTime);

                    cmd.ExecuteNonQuery();
                }
            }
        }


        public Student GetStudentById(int studentId)
        {
            foreach (Student student in AppManager.GetInstance().Students)
            {
                if (student.Id == studentId)
                {
                    return student;
                }
            }
            return null;
        }

        public Instructor GetInstructorById(int instructorId)
        {
            foreach (Instructor instructor in AppManager.GetInstance().Instructors)
            {
                if (instructor.Id == instructorId)
                {
                    return instructor;
                }
            }
            return null;
        }

        public Vehicle GetVehicleById(int vehicleId)
        {
            foreach (Vehicle vehicle in AppManager.GetInstance().Vehicles)
            {
                if (vehicle.Id == vehicleId)
                {
                    return vehicle;
                }
            }
            return null;
        }

    }
}


