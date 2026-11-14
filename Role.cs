using System;
using System.Collections.Generic;
using System.Numerics;
using System.Linq;
using System.Text.Json;
using System.Globalization;
using System.Text.RegularExpressions;



public abstract class Role
{
    Schedule schedule = new();
}

public class Schedule
{
    List<Activity> activities = new();
    public Schedule()
    {
        // will do it later
    }
}

public class PersonalSchedule : Schedule
{
    List<bool> freeTimeUnits = new();
    public PersonalSchedule() : base()
    {
        // will do it later too
    }
}

public class Event
{
    DateTime startOfEvent;
    DateTime endOfEvent;
    int timeUnitLengthInMin;
    int totalEventLength;
    public Event()
    {
        // get date from csv and then get start time from the first active collumn and end time and date can be calculated
        // fill startOfEvent and endOfEvent
    }

}

public class Activity
{
    string title;
    DateTime startDateTime;
    int durationInTimeUnit;
    int flexibilityInMin;
    int difficultyRating;
    int amountOfPeopleNeeded;
}

public static class Errors
{
    public static void BroadcastErrorMessage(string message)
    {
        Console.WriteLine(message);
    }
}

public static class Data
{
    public static string path = "ggg";
    public static List<Activity> participantActivities;
    public static List<Activity> staffActivities;
    public static void GetInfoFromCsv()
    {
        //checks that the file exists
        if (File.Exists(path))
        {
            //streamreader opens the file somhow
            StreamReader reader = new StreamReader(File.OpenRead(path));
            int timeUnitLengthInMin = 0;
            int i = 0;
            //loop to make sure we loop through each line of the file until we get to the end
            while (!reader.EndOfStream)
            {
                //access each line as it's read in
                string? line = reader.ReadLine();
                if (line == null)
                {
                    break;
                }
                string[] values = line.Split(',');
                if (i == 0) { GetDateTimeFromLine(values, timeUnitLengthInMin); }
                else if (i == 1) { GetParticipantSchedule(values); }
                else if (i == 2) { GetStaffSchedule(values, (i + 1) / 3); }
                List<string[]> doc = new();
                Console.WriteLine("Hello {0}", values[0]);
                i++;
            }
            //the stream must be closed... FOREVER
            reader.Close();
        }
        else
        {
            Console.WriteLine("The file doesn't exist");
        }
    }
    public static DateTime? GetDateTimeFromLine(string[] values, int )
    {
        if (DateOnly.TryParseExact(values[0],"d/M/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateOnly startDate)
        &&  TimeOnly.TryParseExact(values[1],"h:mm:ss tt",CultureInfo.InvariantCulture,DateTimeStyles.None, out TimeOnly startTime)
        &&  TimeOnly.TryParseExact(values[1],"h:mm:ss tt",CultureInfo.InvariantCulture,DateTimeStyles.None, out TimeOnly nextTime))
        {
            DateTime startDateTime = startDate.ToDateTime(startTime);
            int duration = (int)((nextTime.Ticks - startTime.Ticks)/ TimeSpan.TicksPerMinute);
            return startDateTime;
        }
        Errors.BroadcastErrorMessage("date or time wasn't inputted correctly, please use '=date(d,M,y)' and '=time(h,m,s)' in the appropriate cells");
        return null;
    }
    public static void GetParticipantSchedule(string[] values)
    {
        if (values[0] == "Participants")
        {
            while()
        }
    }
    public static void GetStaffSchedule(string[] values, int i)
    {
        if (Regex.IsMatch(values[0], @"^StaffTasks 0*" + i + @"$"))
        {

        }
        else if (values[0] == "RequiredStaffCount")
        {

        }
        else if (values[0] == "TaskDifficulty")
        {

        }
        else
        {
            Errors.BroadcastErrorMessage("date wasn't inputted correctly, please use '=date(d,M,y)' on the A1 square in the gsheet");
        }
    }
}
