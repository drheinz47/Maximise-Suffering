
namespace Maximise_Suffering;
public static class DataOutput
{
    public static string path = "Staffer.csv";
    public static void CreateEventStaffSchedule(string path)
    {
        DebugExampleJobFullStaffList();
        //this assumes that each staff member has all his assigned jobs ordered by time
        using (StreamWriter writecsv = new StreamWriter(path))
        {

            foreach (Staff staffMember in DataInput.staffList)
            {
                writecsv.Write(staffMember.name + ",");
                int i = 0,j = 0;
                staffMember.personalSchedule.SortByDateTime();
                List<Job> jobs = staffMember.personalSchedule.jobs;
                while (j < jobs.Count - 1 && jobs.Count != 0)
                {
                    DateTime timePointer = DataInput.startDateTime.AddMinutes(DataInput.timeUnitLengthInMin * i);
                    if (jobs[j].startDateTime == timePointer) { writecsv.Write(jobs[j].title + ","); j++; } i++;
                }
                writecsv.WriteLine(jobs[j].title);
            }
        }
    }
    public static void DebugExampleJobFullStaffList()
    {
        DataInput.staffList.Add(new Staff());
        DataInput.staffList.Add(new Staff());
        DataInput.staffList.Add(new Staff());
        Job job;
        for (int j = 0; j < 9; j++)
        {
            job = new("great" + j.ToString(), DataInput.startDateTime.AddMinutes(DataInput.timeUnitLengthInMin * (j)));
            DataInput.staffList[0].personalSchedule.jobs.Add(job);
            Console.WriteLine(job.title);

            job = new("decent" + j.ToString(), DataInput.startDateTime.AddMinutes(DataInput.timeUnitLengthInMin * (j)));
            DataInput.staffList[1].personalSchedule.jobs.Add(job);
            Console.WriteLine(job.title);

            job = new("bad" + j.ToString(), DataInput.startDateTime.AddMinutes(DataInput.timeUnitLengthInMin * (j)));
            DataInput.staffList[2].personalSchedule.jobs.Add(job);
            Console.WriteLine(job.title);
        }
    }
}
