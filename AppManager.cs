using System;
using System.Collections.Generic;

namespace DrivingSchoolManagement
{
    public class AppManager
    {
        private static AppManager instance;

        public List<Vehicle> Vehicles { get; set; }
        public List<Student> Students { get; set; }
        public List<Instructor> Instructors { get; set; }
        public DrivingSchedule Schedule { get; set; }

        public event EventHandler DataUpdated;

        public DatabaseManager databaseManager;

        private AppManager()
        {
            databaseManager = DatabaseManager.GetInstance("localhost","DrivingSchool", "postgres", "admin");
            Vehicles = databaseManager.GetVehiclesFromDatabase();
            Students = databaseManager.GetStudentsFromDatabase();
            Instructors = databaseManager.GetInstructorsFromDatabase();
            Schedule = DrivingSchedule.Instance;
        }

        public void UpdateData()
        {
            Vehicles = databaseManager.GetVehiclesFromDatabase();
            Students = databaseManager.GetStudentsFromDatabase();
            Instructors = databaseManager.GetInstructorsFromDatabase();
            Schedule.TheoreticalLectures = databaseManager.GetTheoreticalLecturesFromDatabase();
            Schedule.PracticalDrivingSchedule = databaseManager.GetPracticalDrivingsFromDatabase();
            OnDataUpdated();
        }

        protected virtual void OnDataUpdated()
        {
            DataUpdated?.Invoke(this, EventArgs.Empty);
        }

        public static AppManager GetInstance()
        {
            if (instance == null)
            {
                instance = new AppManager();
            }
            return instance;
            
        }

        public void AddInstructor(Instructor instructor)
        {
            Instructors.Add(instructor);
            databaseManager.AddInstructor(instructor);
            UpdateData();
        }

        public void AddVehicle(Vehicle vehicle)
        {
            Vehicles.Add(vehicle);
            databaseManager.AddVehicle(vehicle);    
            UpdateData();
        }

        public void AddStudent(Student student)
        {
            Students.Add(student);
            databaseManager.AddStudent(student);
            UpdateData();
        }

        public void ScheduleDriving(PracticalDriving practicalDriving)
        {
            DrivingScheduler.ScheduleDriving(Schedule, practicalDriving);
            databaseManager.AddPracticalDriving(practicalDriving);
            UpdateData();
        }

        public void AddIncident(int vehicleId, Incident incident)
        {
            foreach (Vehicle veh in Vehicles)
            {
                if (veh.Id == vehicleId)
                {
                    veh.AddIncident(incident);
                    databaseManager.AddIncident(vehicleId, incident);
                    UpdateData();
                    break;
                }
            }
        }

        public void UpdateVehicle(Vehicle vehicle)
        {
            for (int i = Vehicles.Count - 1; i >= 0; i--)
            {
                if (Vehicles[i].Id == vehicle.Id)
                {
                    Vehicles.RemoveAt(i);
                    Vehicles.Insert(i, vehicle);
                    databaseManager.UpdateVehicle(vehicle);
                    UpdateData();
                    break;
                }
            }
        }

        public void RemoveVehicle(int vehicleId)
        {
            Vehicles.RemoveAll(vehicle => vehicle.Id == vehicleId);
            databaseManager.RemoveVehicle(vehicleId);
            UpdateData();
        }

        public void UpdateStudent(Student student)
        {
            for (int i = Students.Count - 1; i >= 0; i--)
            {
                if (Students[i].Id == student.Id)
                {
                    Students.RemoveAt(i);
                    Students.Insert(i, student);
                    databaseManager.UpdateStudent(student);
                    UpdateData();
                    break;
                }
            }
        }

        public void RemoveStudent(int studentId)
        {
            Students.RemoveAll(student => student.Id == studentId);
            databaseManager.RemoveStudent(studentId);
            UpdateData();
        }

        public void UpdateInstructor(Instructor instructor)
        {
            for (int i = Instructors.Count - 1; i >= 0; i--)
            {
                if (Instructors[i].Id == instructor.Id)
                {
                    Instructors.RemoveAt(i);
                    Instructors.Insert(i, instructor);
                    databaseManager.UpdateInstructor(instructor);
                    UpdateData();
                    break;
                }
            }
        }

        public void RemoveInstructor(int instructorId)
        {
            Instructors.RemoveAll(instructor => instructor.Id == instructorId);
            databaseManager.RemoveInstructor(instructorId);
            UpdateData();
        }

        public void SaveDataToCsv()
        {
            CsvManager.SaveToCsv(Students, "students.csv");
            CsvManager.SaveToCsv(Instructors, "instructors.csv");
            CsvManager.SaveToCsv(Vehicles, "vehicles.csv");
            List<PracticalDriving> practicalDrivings = Schedule.PracticalDrivingSchedule.Values.SelectMany(x => x).ToList();
            CsvManager.SaveToCsv(practicalDrivings, "pracitcalDrivings.csv");
            CsvManager.SaveToCsv(Schedule.TheoreticalLectures, "theoreticalLectures.csv");
            MessageBox.Show("Zapisano dane do CSV.");
        }


    }
}
