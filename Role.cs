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
    string title;
    DateTime startDateTime;
    int durationInTimeUnit;
    int flexibilityInTimeUnit;
}

public class Task : Activity
{
    int difficultyRating;
    int amountOfPeopleNeeded;
    Skill[] skillsNeeded = new Skill[7];
}

public class Skill
{
    string name;
    int level;

}

public static class Errors
{
    public static void BroadcastErrorMessage(string message)
    {
        Console.WriteLine(message);
    }
}
