using System;


    public class PracticalDriving
{
        public int Id { get; set; }
        public Student Student { get; set; }
        public Instructor Instructor { get; set; }
        public Vehicle Vehicle { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }


        public static PracticalDriving Create(int id, Student student, Instructor instructor, Vehicle vehicle, DateTime startTime)
        {
        var lesson = new PracticalDriving
        {
            Id = id,
            Student = student,
            Instructor = instructor,
            Vehicle = vehicle,
            StartTime = startTime,
            EndTime = startTime.AddHours(2),
         };
            return lesson;
        }
    }

