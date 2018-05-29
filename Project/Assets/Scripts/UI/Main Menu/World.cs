using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World
{
    public int Id { get; set; }
    public string WorldName { get; set; }
    public bool Completed { get; set; }
    public int Stars { get; set; }
    public int MinStarsToComplete { get; set; }
    public bool Locked { get; set; }

    public List<Level> ListOfLevels { get; set; }

    public World(int i_Id, string i_WorldLevel, bool i_Completed, int i_Stars, int i_MinStarsToComplete, bool i_Locked)
    {
        Id = i_Id;
        WorldName = i_WorldLevel;
        Completed = i_Completed;
        Stars = i_Stars;
        MinStarsToComplete = i_MinStarsToComplete;
        Locked = i_Locked;

        ListOfLevels = new List<Level>();
    }

    public void AddLevel(Level i_NewLevel)
    {
        ListOfLevels.Add(i_NewLevel);
    }
}
