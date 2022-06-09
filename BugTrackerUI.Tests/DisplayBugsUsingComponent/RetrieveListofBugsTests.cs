using BugTrackerUI.Tests;
using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Xunit;

namespace M6_BugTrackerUI.Tests.DisplayBugsUsingComponent
{
    public class M6_04_RetrieveListofBugsTests
    {
        [Fact(DisplayName = "Retrieve list of bugs @retrieve-buglist")]
        public void RetrieveListofBugsTest()
        {
            var filePath = $"{TestHelpers.GetRootString()}BugTrackerUI{Path.DirectorySeparatorChar}Components{Path.DirectorySeparatorChar}BugList.razor";

            Assert.True(File.Exists(filePath), "`BugList.razor` should exist in the Pages folder.");

            string file;
            using (var streamReader = new StreamReader(filePath))
            {
                file = streamReader.ReadToEnd();
            }

            var pattern = @"\s*?protected\s*?override\s*?void\s*?\s*?OnInitialized[(][)]\s*?[{]\s*?Bugs\s*?=\s*?BugService.GetBugs[(][)].OrderBy[(]x => x.Priority[)].ToList[(][)];\s*?}\s*?";
            var pattern2 = @"\s*?protected\s*?override\s*?void\s*?\s*?OnInitialized[(][)]\s*?[{]\s*?Bugs\s*?=\s*?BugService.GetBugs[(][)];\s*?}\s*?";
            var rgx = new Regex(pattern);
            var rgx2 = new Regex(pattern2);
            Assert.True(rgx.IsMatch(file) || rgx2.IsMatch(file), "`BugList.razor` was found, but does not contain a `protected void` method called `OnInitialized` that retrieves the list of Bugs.");
        }
    }
}
