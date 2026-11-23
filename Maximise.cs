
namespace Maximise_Suffering;

public class Maximise
{
    public List<Staff> staffList = DataInput.staffList;
    public List<Job?> jobs = DataInput.staffActivities;
    public void EliminateNulls()
    {
        jobs.RemoveAll(i => i == null);
    }
    public void DistributeJobs()
    {
        EliminateNulls();
        foreach (Job job in jobs!)
        {

        }
    }
    public void FindCandidate(Job job)
    {
        int min;
        foreach (Staff staff in staffList)
        {

        }
    }
    public void SortStaffList(List<Staff> staffList)
    {

    }
}
