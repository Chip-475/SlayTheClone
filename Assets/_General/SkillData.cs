using System.Collections.Generic;

public class SkillData
{
    public struct LoadoutEntryData
    {
        public int id;
        public int loadoutID;
    }

    public Dictionary<int, int> equippedSkills = new();
    public Dictionary<int, LoadoutEntryData> loadoutPanel_entriesState = new();
}
