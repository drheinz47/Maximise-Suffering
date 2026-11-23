using System;
using System.Collections.Generic;
using System.Numerics;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Maximise_Suffering;
class Program
{
    public static List<int?> jobs = [null,7,null,8,null,9,58,74,0,null];
    static async Task Main(string[] args)
    {
        foreach (int? job in jobs)
        {
            if (job == null) { Console.Write("null,"); }
            else { Console.Write(job + ","); }
        }
        Console.WriteLine("");
        EliminateNulls();
        foreach(int? job in jobs)
        {
            Console.Write(job + ",");
        }
        //DataOutput.CreateEventStaffSchedule(DataOutput.path);
    }
    public static void EliminateNulls()
    {
        jobs.RemoveAll(i => i == null);
    }
}
