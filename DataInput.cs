using System;
using System.Collections.Generic;
using System.Numerics;
using System.Linq;
using System.Text.Json;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Maximise_Suffering;
public static class DataInput
{
    public static DateTime startDateTime;
    public static int timeUnitLengthInMin;

    public static int staffCount;
    public static List<Staff> staffList = [];
    public static List<Job?> staffActivities = [];
    public static List<Activity> participantActivities = [];

    public static string eventSheetPath = "EventDataSheet.csv";
    public static string staffSheetPath = "StaffDataSheet.csv";
    public static string[] timeformats = { "h:mm:ss tt", "h:mm:ss" };

    public static void GetInfoFromCsv()
    {
        //checks that the file exists
        if (File.Exists(eventSheetPath)) { InterpretCsvFile(eventSheetPath); }
        else { Errors.BroadcastErrorMessage("The" + eventSheetPath + "file doesn't exist"); }
        if (File.Exists(staffSheetPath)) { InterpretCsvFile(staffSheetPath); }
        else { Errors.BroadcastErrorMessage("The" + staffSheetPath + "file doesn't exist"); }
    }
    public static void InterpretCsvFile(string path)
    {
        int i = 0;
        StreamReader reader = new(File.OpenRead(path));
        //treat each line as its own set of input and chop all their heads one by one
        while (!reader.EndOfStream)
        {
            string? line = reader.ReadLine();
            if (line == null) { return; }
            string[] values = line.Split(',');
            if (path == "StaffDataSheet.csv") { GetInfoFromPersonalSchedule(values); }
            else if (i == 0) { GetDateTimeFromLine(values); }
            else if (i == 1) { GetParticipantSchedule(values); }
            else { GetStaffSchedule(values, (i + 1) / 4); }
            i++;
        }
        reader.Close(); //the stream must be closed... FOREVER
    }
    public static void GetDateTimeFromLine(string[] values)
    {

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
            CreateJobInstances(values, false);
        }
    }
    public static void GetStaffSchedule(string[] values, int ind)
    {
        if (Regex.IsMatch(values[0], @"^StaffTasks 0*" + ind + @"$"))
        {
            CreateJobInstances(values, true);
        }
        else if (values[0] == "RequiredStaffCount")
        {
            DefineJobInstances(values, Section.STAFF_COUNT);
        }
        else if (values[0] == "TaskDifficulty")
        {
            DefineJobInstances(values, Section.TASK_DIFFICULTY);
        }
        else if (values[0] == "SkillsNeeded")
        {
            DefineJobInstances(values, Section.SKILLS_NEEDED);
        }
        else
        {
            Errors.BroadcastErrorMessage("something went wrong >:(");
        }
    }
    public static void CreateJobInstances(string[] values, bool isStaff)
    {
        for (int i = 1; i < values.Length; i++)
        {
            if (values[i] == "") { staffActivities.Add(null); continue; }
            DateTime currentDateTime = startDateTime.AddMinutes(timeUnitLengthInMin * (i - 1));
            Job job = new(values[i], currentDateTime);
            if (isStaff) { staffActivities.Add(job); }
            else { participantActivities.Add(job); }
        }
    }
    public static void DefineJobInstances(string[] values, Section section)
    {
        if (staffActivities == null) { return; }
        int temp;
        for (int i = 1; i < values.Length; i++)
        {
            if (staffActivities[i] == null) { continue; }
            else if (values[i] == "" && section != Section.TASK_FLEXIBILITY) { Errors.BroadcastErrorMessage("invalid Task declaration" + section.ToString()); return; }
            if (section == Section.SKILLS_NEEDED)
            {
                string[] skills = values[i].Split("/");
                foreach (string skill in skills)
                {
                    Skill skillNeeded = new(skill);
                    staffActivities[i]!.skillsNeeded.Add(skillNeeded);
                }
            }
            else if (section == Section.STAFF_COUNT)
            {
                if (values[i] == "All") { temp = staffCount; }
                else if (!int.TryParse(values[i], out temp)) { Errors.BroadcastErrorMessage("invalid Amount of Staff detected"); }
                staffActivities[i]!.staffCountNeeded = temp;
            }
            else if (section == Section.TASK_DIFFICULTY)
            {
                if (!int.TryParse(values[i], out temp)) { Errors.BroadcastErrorMessage("invalid Difficulty detected"); }
                staffActivities[i]!.difficultyRating = temp;
            }
            else if (section == Section.TASK_FLEXIBILITY)
            {
                if (values[i] == "" || values[i] == "none") { temp = staffCount; }
                else if (TimeOnly.TryParseExact(values[i], timeformats, CultureInfo.InvariantCulture, DateTimeStyles.None, out TimeOnly latestStartTime))
                { staffActivities[i]!.flexibilityInTimeUnit = (int)((latestStartTime.Ticks - startDateTime.Ticks) / TimeSpan.TicksPerMinute); }
            }
        }
    }
    public static void GetInfoFromPersonalSchedule(string[] values)
    {
        Staff staff = new Staff();
        for (int i = 1; i < values.Length; i++)
        {
            if (values[i] == "busy") { staff.personalSchedule.freeTimeUnits.Add(false); }
            else if (values[i] == "") { staff.personalSchedule.freeTimeUnits.Add(true); }
            else { Errors.BroadcastErrorMessage("invalid personal schedule, please fill it with either busy or leave it empty"); }
        }
        staffList.Add(staff);
    }
}
public enum Section{ STAFF_COUNT, TASK_DIFFICULTY, SKILLS_NEEDED, TASK_FLEXIBILITY}
