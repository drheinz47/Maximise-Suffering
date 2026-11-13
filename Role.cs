using System;
using System.Collections.Generic;
using System.Numerics;
using System.Linq;
using System.Text.Json;



public abstract class Role
{
    Schedule schedule = new(7);
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

}

public static class Data
{
    public static string path = "ggg";
    public static void GetInfoFromCsv()
    {
        //check that the file exists
        if (File.Exists(path))
        {
            //use the streamreader to open the file
            StreamReader reader = new StreamReader(File.OpenRead(path));

            //loop to make sure we loop through each line of the file until we get to the end
            while (!reader.EndOfStream)
            {
                //here is where we can access each line as it's read in
                string? line = reader.ReadLine();
                if(line == null)
                {
                    break;
                }
                string[] values = line.Split(',');
                Console.WriteLine("Hello {0}", values[0]);
            }
            //closed up the stream to the file
            reader.Close();
        }
        else
        {
            Console.WriteLine("The file doesn't exist");
        }
    }
}
