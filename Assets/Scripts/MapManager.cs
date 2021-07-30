using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{

    public Vector2Int mapSize;

    [SerializeField]
    TileInteraction _interactMngr;

    /// <summary>
    /// Insert selected tile on map
    /// </summary>
    void InsertTile(int tile, Vector2Int coord)
    {
        Transform t = Instantiate<GameObject>(_interactMngr.availableTiles[tile]).transform;
        t.position = (Vector2)coord;
    }

    /// <summary>
    /// Erases all tiles and replace all of them with empty tiles.
    /// </summary>
    void Clear()
    {
        Transform[] blob = GetComponentsInChildren<Transform>();
        Vector2Int currentCoord;
        for (int i = 0; i < blob.Length; i++)
        {
            Destroy(blob[i].gameObject);
        }
        for (int i = 0; i < mapSize.x; i++)
        {
            for (int j = 0; j < mapSize.y; j++)
            {
                currentCoord = new Vector2Int(i, j);
                InsertTile(0, currentCoord);
            }
        }
    }

    /// <summary>
    /// This function will check the size of the current map and can result in two cases:
    /// 1 - If the map's new size is bigger than the current one, call the function to populate
    ///     the current map with empty tiles [will be defined later].
    /// 2 - If the map's new size is smaller than the current one, call the function to truncate
    ///     the current map and delete the oversized tiles [will be defined later].
    /// Returns the new map's size.
    /// </summary>
    /// <param name="size"></param>
    /// <returns></returns>
    Vector2Int Resize(Vector2Int size)
    {

        mapSize = size;
        return mapSize;
    }

    /// <summary>
    /// Reload all current session changes and reset the map to the last saved state.  
    /// </summary>
    void Reset()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            Clear();
        }
    }
}
