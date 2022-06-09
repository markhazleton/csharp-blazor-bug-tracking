namespace BugTrackerUI.Services;

public class BugService : IBugService
{
    private readonly List<Bug> Bugs = new();

    public void AddBug(Bug newBug)
    {
        newBug.Id = Bugs.Count + 1;
        Bugs.Add(newBug);
    }

    public List<Bug> GetBugs()
    {
        return Bugs;
    }
}
