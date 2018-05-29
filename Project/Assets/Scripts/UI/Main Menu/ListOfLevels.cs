using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ListOfLevels : MonoBehaviour
{
    public List<World> m_ListOfWorlds;
    public int WorldID;
    
    void Start ()
    {
        // List Of World
        m_ListOfWorlds = new List<World>();
        m_ListOfWorlds.Add(new World(1, "First World", false, 0, 22, false));

        // First World Levels
        m_ListOfWorlds[0].AddLevel(new Level(1, "1", false, 0, false));
        m_ListOfWorlds[0].AddLevel(new Level(2, "2", false, 0, false));
        m_ListOfWorlds[0].AddLevel(new Level(3, "3", false, 0, false));
        m_ListOfWorlds[0].AddLevel(new Level(4, "4", false, 0, false));
        m_ListOfWorlds[0].AddLevel(new Level(5, "5", false, 0, false));
        m_ListOfWorlds[0].AddLevel(new Level(6, "6", false, 0, false));
        m_ListOfWorlds[0].AddLevel(new Level(7, "7", false, 0, false));
        m_ListOfWorlds[0].AddLevel(new Level(8, "8", false, 0, false));
        m_ListOfWorlds[0].AddLevel(new Level(9, "9", false, 0, false));

        // Second World Levels
        // Third World Levels

        // Init current world
        World currentWorld = m_ListOfWorlds[WorldID - 1];
        GameObject title = transform.GetChild(0).gameObject;
        GameObject levels = transform.GetChild(1).gameObject;

        title.GetComponentInChildren<Text>().text = currentWorld.WorldName;

        for (int i = 0; i < currentWorld.ListOfLevels.Count; i++)
        {
            GameObject levelTile = levels.transform.GetChild(i).gameObject;

            LevelTile tile = levelTile.GetComponent<LevelTile>();
            if (tile != null)
            {
                tile.m_Level = m_ListOfWorlds[0].ListOfLevels[i];
                tile.InitTile();
            }
        }

    }
}
