using BugTrackerUI.Tests;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using Xunit;

namespace M5_BugTrackerUI.Tests.AddDataValidationToForm
{
    public class M5_01_AddRequiredAttributesTests
    {
        [Fact(DisplayName = "Add Required Attributes @add-required-attributes")]
        public void AddRequiredAttributesTest()
        {
            var filePath = $"{TestHelpers.GetRootString()}BugTrackerUI{Path.DirectorySeparatorChar}/Services/Bug.cs";

            Assert.True(File.Exists(filePath), "/Services/Bug.cs should exist.");

            var bug = TestHelpers.GetClassType("BugTrackerUI.Services.Bug");

            var titleAttributes = bug.GetProperty("Title").GetCustomAttributesData();
            var titleRequired = titleAttributes.FirstOrDefault(x => x.AttributeType == typeof(RequiredAttribute));
            Assert.True(titleRequired != null, "The `Title` property of the `Bug` class should be marked with the `[Required]` attribute.");

            var descriptionAttributes = bug.GetProperty("Description").GetCustomAttributesData();
            var descriptionRequired = descriptionAttributes.FirstOrDefault(x => x.AttributeType == typeof(RequiredAttribute));
            Assert.True(descriptionRequired != null, "The `Description` property of the `Bug` class should be marked with the `[Required]` attribute.");

            var priorityAttributes = bug.GetProperty("Priority").GetCustomAttributesData();
            var priorityRequired = priorityAttributes.FirstOrDefault(x => x.AttributeType == typeof(RequiredAttribute));
            Assert.True(priorityRequired != null, "The `Priority` property of the `Bug` class should be marked with the `[Required]` attribute.");
        }
    }
}
