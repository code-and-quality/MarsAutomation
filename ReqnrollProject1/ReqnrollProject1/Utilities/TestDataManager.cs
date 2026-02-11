using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsAutomation.Utilities
{
    public static class TestDataManager
    {
        public static List<string> LanguagesAdded = new List<string>();
        public static List<string> SkillsAdded = new List<string>();

        public static void AddLanguage(string language)
        {
            if (!LanguagesAdded.Contains(language))
                LanguagesAdded.Add(language);
        }

        public static void AddSkill(string skill)
        {
            if (!SkillsAdded.Contains(skill))
                SkillsAdded.Add(skill);
        }

        public static void Clear()
        {
            LanguagesAdded.Clear();
            SkillsAdded.Clear();
        }
    }

}
