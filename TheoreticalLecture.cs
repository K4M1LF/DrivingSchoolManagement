public class TheoreticalLecture
{
    public int Id { get; set; }
    public Instructor Instructor { get; set; }
    private DateTime _startDate;
    public DateTime StartDate
    {
        get { return _startDate; }
        set { _startDate = GetNextMonday(value); }
    }
    public List<Student> Participants { get; set; } = new List<Student>();
    public TimeSpan DurationPerLecture { get; set; }
    public int NumberOfLectures { get; private set; }
    public TimeSpan LectureStartTime { get; set; }
    public TimeSpan LectureEndTime { get; private set; }

    public TheoreticalLecture()
    {
        DurationPerLecture = TimeSpan.FromHours(2);
        NumberOfLectures = 15;
        LectureStartTime = new TimeSpan(16, 0, 0); 
        LectureEndTime = LectureStartTime.Add(DurationPerLecture);
    }

    public TheoreticalLecture(int id, Instructor instructor, DateTime startDate)
        : this()
    {
        Id = id;
        Instructor = instructor;
        StartDate = startDate;
    }

    public void AddParticipant(Student student)
    {
        Participants.Add(student);
    }

    public void DisplayAllLectureDates()
    {
        DateTime currentDate = StartDate;
        int lectureCounter = 1;

        while (lectureCounter <= NumberOfLectures)
        {
            if (currentDate.DayOfWeek != DayOfWeek.Saturday && currentDate.DayOfWeek != DayOfWeek.Sunday)
            {
                Console.WriteLine($"{lectureCounter}. {currentDate.ToShortDateString()}, {LectureStartTime} - {LectureEndTime}");
                lectureCounter++;
            }

            currentDate = currentDate.AddDays(1);
        }
    }

    private DateTime GetNextMonday(DateTime date)
    {
        int daysUntilMonday = ((int)DayOfWeek.Monday - (int)date.DayOfWeek + 7) % 7;
        return date.AddDays(daysUntilMonday);
    }
}
