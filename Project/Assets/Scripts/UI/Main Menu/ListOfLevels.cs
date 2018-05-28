using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.UIElements;

public class ListOfLevels : MonoBehaviour
{
    public List<Level> m_ListOfLevels;
    public List<GameObject> m_ListOfLevelTiles;
    
    // Use this for initialization
    void Start ()
    {
        m_ListOfLevels = new List<Level>();
        m_ListOfLevels.Add(new Level(1, "1", false, 0, false));
        m_ListOfLevels.Add(new Level(2, "2", false, 0, false));
        m_ListOfLevels.Add(new Level(3, "3", false, 0, false));
        m_ListOfLevels.Add(new Level(4, "4", false, 0, false));
        m_ListOfLevels.Add(new Level(5, "5", false, 0, false));
        m_ListOfLevels.Add(new Level(6, "6", false, 0, false));
        m_ListOfLevels.Add(new Level(7, "7", false, 0, false));
        m_ListOfLevels.Add(new Level(8, "8", false, 0, false));
        m_ListOfLevels.Add(new Level(9, "9", false, 0, false));
        
        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject levelTile = transform.GetChild(i).gameObject;
            m_ListOfLevelTiles.Add(levelTile);

            LevelTile tile = levelTile.GetComponent<LevelTile>();
            if (tile != null)
            {
                tile.m_Level = m_ListOfLevels[i];
                tile.InitTile();
            }
        }

    }
}
