﻿using BugTrackerUI.Tests;
using System;
using System.IO;
using System.Linq;
using Xunit;

namespace M4_BugTrackerUI.Tests.WorkingWithServicesAndData
{
    public class M4_01_RegisterTheBugServiceTests
    {
        [Fact(DisplayName = "Register the BugService @register-bug-service")]
        public void RegisterTheBugServiceTest()
        {
            var filePath = $"{TestHelpers.GetRootString()}BugTrackerUI{Path.DirectorySeparatorChar}Program.cs";

            Assert.True(File.Exists(filePath), "`Program.cs` was not found.");

            string file;
            using (var streamReader = new StreamReader(filePath))
            {
                file = streamReader.ReadToEnd();
            }

            Assert.True(file.Contains("services.AddSingleton<IBugService, BugService>()"),
                "The `ConfigureServices` method in `Startup.cs` did not contain a call to `services.AddSingleton` with the type parametrs `<IBugService, BugService>`.");
        }
    }
}