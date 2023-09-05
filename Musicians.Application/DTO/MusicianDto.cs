using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Musicians.Application.DTO
{
    public class MusicianDto
    {
        public string Name { get; set; } = string.Empty;
        public string Genre { get; set; } = string.Empty;
        public string PerformAs { get; set; } = string.Empty;
        public DateTime IntroDate { get; set; }
        public string? Instrument { get; set; }
    }
}
