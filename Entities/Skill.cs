using System;
using System.Collections.Generic;

#nullable disable

namespace MyApi.Entities
{
    public partial class Skill
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
