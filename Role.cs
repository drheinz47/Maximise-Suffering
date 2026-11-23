using System;
using System.Collections.Generic;
using System.Numerics;
using System.Linq;
using System.Text.Json;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Maximise_Suffering;
public abstract class Role
{
    public string name = "";
    public PersonalSchedule personalSchedule = new();
}
public class Staff : Role
{}
public class Schedule
{
    public List<Activity> activities = new();
    public Schedule()
    {
        // will do it later
    }
}

public class PersonalSchedule
{
    public List<Job> jobs = new();
    public List<bool> freeTimeUnits = new(); //true means free
    public PersonalSchedule() : base()
    {

    }
    public void SortByDateTime()
    {
        jobs.Sort((x1, x2) => DateTime.Compare(x1.startDateTime, x2.startDateTime));
    }
}

public class Event
{
    public DateTime startOfEvent;
    public DateTime endOfEvent;
    public int timeUnitLengthInMin;
    public int totalEventLength;
    public Event()
    {
        // get date from csv and then get start time from the first active collumn and end time and date can be calculated
        // fill startOfEvent and endOfEvent
    }

}

public class Activity
{
    public string title;
    public DateTime startDateTime;
    //int durationInTimeUnit; possibly add it
    public int flexibilityInTimeUnit;
    public Activity(string title, DateTime startDateTime)
    {
        this.title = title;
        this.startDateTime = startDateTime;
    }
}

public class Job : Activity
{
    public int difficultyRating;
    public int staffCountNeeded;
    public List<Skill> skillsNeeded = new();
    public Job(string title, DateTime startDateTime) : base(title, startDateTime){}
}

public class Skill
{
    string name;
    int level;
    public Skill(string name)
    {
        this.name = name;
        level = 0;
    }
    public Skill(string name, int level)
    {
        this.name = name;
        this.level = level;
    }
}

public static class Errors
{
    public static void BroadcastErrorMessage(string message)
    {
        Console.WriteLine(message);
    }
}
