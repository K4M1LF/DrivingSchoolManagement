using System;
using System.Collections.Generic;
using System.Diagnostics;

public class StudentActivity
{
   public int ActivityID;
   public  string InstructorFirstName;
   public  string InstructorLastName;
   public DateTime StartDate;
   public string ActivityType;

    public StudentActivity(int activityID, string instructorFirstName, string instructorLastName, DateTime startDate, string activityType)
    {
        ActivityID = activityID;
        InstructorFirstName = instructorFirstName;
        InstructorLastName = instructorLastName;
        StartDate = startDate;
        ActivityType = activityType;
    }
}
public class DrivingSchedule
{
    public Dictionary<Instructor, List<PracticalDriving>> PracticalDrivingSchedule { get; set; }
    public List<TheoreticalLecture> TheoreticalLectures { get; set; }
    private static DrivingSchedule instance;

    private DrivingSchedule()
    {
        PracticalDrivingSchedule = new Dictionary<Instructor, List<PracticalDriving>>();
        TheoreticalLectures = new List<TheoreticalLecture>();
    }

    public static DrivingSchedule Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new DrivingSchedule();
            }
            return instance;
        }
    }

    public void AddDriving(PracticalDriving driving)
    {
        if (!PracticalDrivingSchedule.ContainsKey(driving.Instructor))
        {
            PracticalDrivingSchedule[driving.Instructor] = new List<PracticalDriving>();
        }
        PracticalDrivingSchedule[driving.Instructor].Add(driving);
    }

    public void AddLecture(TheoreticalLecture lecture)
    {
        TheoreticalLectures.Add(lecture);
    }

    public List<StudentActivity> DisplayScheduleForStudent(Student student)
    {
        List<StudentActivity> studentActivities = new List<StudentActivity>();
        DisplayTheoreticalLectures(student, studentActivities);
        DisplayPracticalDrivings(student, studentActivities);
        return studentActivities;
    }

    private void DisplayPracticalDrivings(Student student, List<StudentActivity> studentActivities)
    {
        foreach (var (instructor, drivings) in PracticalDrivingSchedule)
        {
            foreach (var driving in drivings.Where(driving => driving.Student.Id == student.Id))
            {
                studentActivities.Add(new StudentActivity(driving.Id, driving.Instructor.FirstName, driving.Instructor.LastName, driving.StartTime, "Jazda Praktyczna"));
            }
        }
    }

    private void DisplayTheoreticalLectures(Student student, List<StudentActivity> studentActivities)
    {
        foreach (var lecture in TheoreticalLectures.Where(lecture => lecture.Participants.Contains(student)))
        {         
            for (int i = 1, dayCounter = 0; dayCounter < lecture.NumberOfLectures; i++)
            {
                DateTime lectureDay = lecture.StartDate.AddDays(i - 1);
                if (lectureDay.DayOfWeek != DayOfWeek.Saturday && lectureDay.DayOfWeek != DayOfWeek.Sunday)
                {
                    dayCounter++;
                    studentActivities.Add(new StudentActivity(lecture.Id, lecture.Instructor.FirstName, lecture.Instructor.LastName, lecture.StartDate.AddDays(dayCounter), "Wykład"));
                }
            }       
        }
    }
    public List<PracticalDriving> GetInstructorDrivings(Instructor instructor)
    {
        if (PracticalDrivingSchedule.ContainsKey(instructor))
        {
            return PracticalDrivingSchedule[instructor];
        }
        else
        {
            return new List<PracticalDriving>();
        }
    }
}
