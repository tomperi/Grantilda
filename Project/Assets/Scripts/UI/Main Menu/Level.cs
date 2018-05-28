using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level
{
    public int Id { get; set; }
    public string LevelName { get; set; }
    public bool Completed { get; set; }
    public int Stars { get; set; }
    public bool Locked { get; set; }

    public Level(int i_Id, string i_LevelName, bool i_Completed, int i_Stars, bool i_Locked)
    {
        Id = i_Id;
        LevelName = i_LevelName;
        Completed = i_Completed;
        Stars = i_Stars;
        Locked = i_Locked;
    }

    public void Complete()
    {
        Completed = true;
    }

    public void Complete(int i_Stars)
    {
        Stars = i_Stars;
    }

    public void Lock()
    {
        Locked = true;
    }

    public void Unlock()
    {
        Locked = false;
    }
}
