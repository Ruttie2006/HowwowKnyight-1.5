#region GlobalSettings
using System.Collections.Generic;

namespace HowwowKnyight.GlobalSettings
{
    public class GlobalSettingsClass
    {
        public Dictionary<string, string> UWUSimpleDict { get; set; } = 
            new()
            {
                { @"R", @"W" },
                { @"L", @"W" },
                { @"l", @"w" },
                { @"OU", @"UW" },
                { @"Ou", @"Uw" },
                { @"ou", @"uw" },
                { @"TH", @"D" },
                { @"Th", @"D" },
                { @"th", @"d" }
            };

        public Dictionary<string, string> UWURegexDict { get; set; } = 
            new()
            {
                { @"N([AEIOU])", @"NY$1" },
                { @"N([aeiou])", @"Ny$1" },
                { @"n([aeiou])", @"ny$1" },
                { @"(?<!<b)r(?!>)", @"w" },
                { @"T[Hh]\b", @"F" },
                { @"th\b", @"f" },
                { @"T[Hh]([UI][^sS])", @"F$1" },
                { @"th([ui][^sS])", @"f$1" },
                { @"OVE\b", @"UV" },
                { @"Ove\b", @"Uv" },
                { @"ove\b", @"uv" }
            };

        public List<string> UWUFaces { get; set; } = 
            new()
            {
                ">.>", "<.<", "UwU",
                "OwO", "OWO", "UWU",
                "uwu", "owo", ">w<",
                "^w^", "QwQ", "@w@",
                ">.<", "ÕwÕ",
            };

        public List<char> Seperators { get; set; } = 
            new()
            {
                '-',
                '.',
                ' '
            };

        public HowwowKnyight.OWOFlags Flags { get; set; }
    }
}
//This does not work fully yet, needs to be fixed. I have no idea how sadly

#endregion