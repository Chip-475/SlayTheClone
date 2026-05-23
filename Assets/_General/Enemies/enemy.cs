using UnityEngine;
using System.Collections.Generic;

// Enemy base class
public class enemy : MonoBehaviour
{
    public EnemyStats stats;
    public List<Skill> skillList = new List<Skill>();
}
