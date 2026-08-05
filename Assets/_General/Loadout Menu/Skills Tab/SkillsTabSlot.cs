using System;

public class SkillsTabSlot : LoadoutSlot
{
    public override Type AcceptedType => typeof(SkillsTabDraggable);
    public Skill skill;
}
