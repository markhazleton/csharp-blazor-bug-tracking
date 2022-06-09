using BugTrackerUI.Tests;
using System;
using System.IO;
using Xunit;

namespace M2_BugTrackerUI.Tests.CreatingNavigationAndComponents
{
    public class M2_06_CreateNewBugComponentTests
    {
        [Fact(DisplayName = "Create the NewBug Component @create-newbug-component")]
        public void CreateNewBugComponentTest()
        {
            var filePath = $"{TestHelpers.GetRootString()}BugTrackerUI{Path.DirectorySeparatorChar}Pages{Path.DirectorySeparatorChar}NewBug.razor";

            Assert.True(File.Exists(filePath), "`NewBug.razor` should exist in the Pages folder.");
        }
    }
}
