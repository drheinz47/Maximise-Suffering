using System;
using System.Collections.Generic;
using System.Numerics;
using System.Linq;
using System.Text.Json;
using System.Globalization;
using System.Text.RegularExpressions;

public static class Data
{
    public static string path = "DataSheet.csv";
    public static DateTime startDateTime;
    public static int timeUnitLengthInMin;
    public static List<Activity> participantActivities;
    public static int staffCount;
    public static List<Job> staffActivities;
    public static void GetInfoFromCsv()
    {
        //checks that the file exists
        if (File.Exists(path))
        {
            //streamreader opens the file somhow
            StreamReader reader = new StreamReader(File.OpenRead(path));
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
                if (i == 0) { GetDateTimeFromLine(values); }
                else if (i == 1) { GetParticipantSchedule(values); }
                else if (i >= 2) { GetStaffSchedule(values, (i + 1) / 4); }
                Console.WriteLine("Hello {0}", values[0]); //test
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
    public static void GetDateTimeFromLine(string[] values)
    {
        string[] timeformats = { "h:mm:ss tt", "h:mm:ss" };
        if (DateOnly.TryParseExact(values[0], "d/M/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateOnly startDate)
        && TimeOnly.TryParseExact(values[1], timeformats, CultureInfo.InvariantCulture, DateTimeStyles.None, out TimeOnly startTime)
        && TimeOnly.TryParseExact(values[2], timeformats, CultureInfo.InvariantCulture, DateTimeStyles.None, out TimeOnly nextTime))
        {
            startDateTime = startDate.ToDateTime(startTime);
            timeUnitLengthInMin = (int)((nextTime.Ticks - startTime.Ticks) / TimeSpan.TicksPerMinute);
        }
        Errors.BroadcastErrorMessage("date or time wasn't inputted correctly, please use '=date(d,M,y)' and '=time(h,m,s)' in the appropriate cells");
    }
    public static void GetParticipantSchedule(string[] values)
    {
        if (values[0] == "Participants")
        {
            while ()
        }
    }
    public static void GetStaffSchedule(string[] values, int ind)
    {
        if (Regex.IsMatch(values[0], @"^StaffTasks 0*" + ind + @"$"))
        {
            int i = 0;
            foreach(string value in values)
            {
                DateTime currentDateTime = startDateTime.AddMinutes(timeUnitLengthInMin * i);
                Job job = new(value, currentDateTime);
                staffActivities.Add(job);
                i++;
            }

        }
        else if (values[0] == "RequiredStaffCount")
        {
            int i = 0;
            int neededStaff = 0;
            foreach(string value in values)
            {
                if (value == "All") { neededStaff = staffCount; }
                else if(!int.TryParse(value, out neededStaff)) { Errors.BroadcastErrorMessage("invalid Amount of Staff detected"); }
                staffActivities[i].staffCountNeeded = neededStaff;
                i++;
            }
        }
        else if (values[0] == "TaskDifficulty")
        {
            int i = 0;
            int difficulty = 0;
            foreach(string value in values)
            {
                if(!int.TryParse(value, out difficulty)){ Errors.BroadcastErrorMessage("invalid Task Difficulty detected");}
                staffActivities[i].staffCountNeeded = difficulty;
                i++;
            }
        }
        else
        {
            Errors.BroadcastErrorMessage("something went wrong >:(");
        }
    }
}
