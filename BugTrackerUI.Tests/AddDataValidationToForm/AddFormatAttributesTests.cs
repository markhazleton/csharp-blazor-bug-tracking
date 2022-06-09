using BugTrackerUI.Tests;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using Xunit;

namespace M5_BugTrackerUI.Tests.AddDataValidationToForm
{
    public class M5_02_AddFormatAttributesTests
    {
        [Fact(DisplayName = "Add Format Attributes @add-format-attributes")]
        public void AddFormatAttributesTest()
        {
            var filePath = $"{TestHelpers.GetRootString()}BugTrackerUI{Path.DirectorySeparatorChar}Services/Bug.cs";

            Assert.True(File.Exists(filePath), "`Services/Bug.cs` should exist");

            var bug = TestHelpers.GetClassType("BugTrackerUI.Services.Bug");

            var descriptionAttributes = bug.GetProperty("Description").GetCustomAttributesData();
            var descriptionRequired = descriptionAttributes.FirstOrDefault(x => x.AttributeType == typeof(MinLengthAttribute));
            Assert.True(descriptionRequired != null, "The `Description` property of the `Bug` class should be marked with the `[MinLength]` attribute.");

            var priorityAttributes = bug.GetProperty("Priority").GetCustomAttributesData();
            var priorityRequired = priorityAttributes.FirstOrDefault(x => x.AttributeType == typeof(RangeAttribute));
            Assert.True(priorityRequired != null, "The `Priority` property of the `Bug` class should be marked with the `[Range]` attribute.");
        }
    }
}
