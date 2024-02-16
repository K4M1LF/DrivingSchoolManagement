public class DrivingScheduler
{
    public static void ScheduleDriving(DrivingSchedule schedule, PracticalDriving driving)
    {
        var instructor = driving.Instructor;
        if(instructor != null && !schedule.PracticalDrivingSchedule.ContainsKey(driving.Instructor))
        {
            schedule.AddDriving(driving);
            return;
        }

        if (instructor != null && CanAddDriving(schedule.PracticalDrivingSchedule[instructor], driving))
        {
            var plannedStartTime = GetNextPlannedStartTime(instructor, driving.StartTime);
            driving.StartTime = plannedStartTime;
            driving.EndTime = plannedStartTime.AddHours(2);
            schedule.AddDriving(driving);
        }
        else
        {
            MessageBox.Show("Instruktor zajęty!");
        }
    }

    private static DateTime GetNextPlannedStartTime(Instructor instructor, DateTime plannedStartTime)
    {
        var workDayStart = new DateTime(plannedStartTime.Year, plannedStartTime.Month, plannedStartTime.Day, 8, 0, 0);
        var workDayEnd = new DateTime(plannedStartTime.Year, plannedStartTime.Month, plannedStartTime.Day, 16, 0, 0);

        while (plannedStartTime >= workDayEnd)
        {
            plannedStartTime = plannedStartTime.AddDays(1);
            workDayStart = new DateTime(plannedStartTime.Year, plannedStartTime.Month, plannedStartTime.Day, 8, 0, 0);
            workDayEnd = new DateTime(plannedStartTime.Year, plannedStartTime.Month, plannedStartTime.Day, 16, 0, 0);
        }

        return plannedStartTime < workDayStart ? workDayStart : plannedStartTime;
    }

    private static bool CanAddDriving(List<PracticalDriving> instructorSchedule, PracticalDriving newDriving)
    {
        return !instructorSchedule.Any(driving => newDriving.StartTime < driving.EndTime && newDriving.EndTime > driving.StartTime);
    }
}
