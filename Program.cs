using System;
using System.Collections.Generic;
using System.Numerics;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Maximise_Suffering;
class Program
{
    static async Task Main(string[] args)
    {
        DataOutput.CreateEventStaffSchedule(DataOutput.path);
    }
}
