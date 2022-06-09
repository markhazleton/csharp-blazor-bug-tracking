namespace BugTrackerUI.Services;

public interface IBugService
{
    List<Bug> GetBugs();

    void AddBug(Bug newBug);
}
