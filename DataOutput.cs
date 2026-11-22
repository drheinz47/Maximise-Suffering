using System;
using System.Collections.Generic;
using System.Numerics;
using System.Linq;
using System.Text.Json;
using System.Globalization;
using System.Text.RegularExpressions;
using static System.Collections.IEnumerable;


namespace Maximise_Suffering;
public static class DataOutput
{
    public static string path = "Staffer.csv";
    public static void CreateEventStaffSchedule(string path)
    {
        //this assumes that each staff member has all his assigned jobs ordered by time
        using(StreamWriter writecsv = new StreamWriter(path))
        {
            foreach(Staff staffMember in DataInput.staffList)
            {
                int i = 0;
                List<Job> jobs = staffMember.personalSchedule.jobs;
                while(jobs.Any())
                {
                    DateTime timePointer = DataInput.startDateTime.AddMinutes(DataInput.timeUnitLengthInMin * (i - 1));
                    if(jobs[i].startDateTime == timePointer){ writecsv.WriteLine(jobs[i].title); i++; }
                }

            }
        }
    }
}
