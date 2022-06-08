using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace BugTrackerUI
{
    public class Bug
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }

        [Required]
        [MinLength(10)]
        public string Description { get; set; }

        [Required]
        [Range(1, 5)]
        public int Priority { get; set; }
    }
}
