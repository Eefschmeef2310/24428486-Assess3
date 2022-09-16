using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LevelGenerator : MonoBehaviour
{
    public GameObject oldLevel;
    public Tilemap tilemap;
    public Tile[] tiles;
    int[,] tileLayout = {{1,2,2,2,2,2,2,2,2,2,2,2,2,7}, 
                         {2,5,5,5,5,5,5,5,5,5,5,5,5,4}, 
                         {2,5,3,4,4,3,5,3,4,4,4,3,5,4}, 
                         {2,6,4,0,0,4,5,4,0,0,0,4,5,4}, 
                         {2,5,3,4,4,3,5,3,4,4,4,3,5,3}, 
                         {2,5,5,5,5,5,5,5,5,5,5,5,5,5}, 
                         {2,5,3,4,4,3,5,3,3,5,3,4,4,4}, 
                         {2,5,3,4,4,3,5,4,4,5,3,4,4,3}, 
                         {2,5,5,5,5,5,5,4,4,5,5,5,5,4}, 
                         {1,2,2,2,2,1,5,4,3,4,4,3,0,4}, 
                         {0,0,0,0,0,2,5,4,3,4,4,3,0,3}, 
                         {0,0,0,0,0,2,5,4,4,0,0,0,0,0}, 
                         {0,0,0,0,0,2,5,4,4,0,3,4,4,0}, 
                         {2,2,2,2,2,1,5,3,3,0,4,0,0,0}, 
                         {0,0,0,0,0,0,5,0,0,0,4,0,0,0}};

    private Vector3Int startPos;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(oldLevel);
        startPos = new Vector3Int(-14,14,0);
        generateLevel();
        //tilemap.SetTile(startPos, tiles[1]);
    }

    void generateLevel()
    {
        //Debug.Log(tileLayout.GetLength(0));
        for(int row = 0; row < tileLayout.GetLength(0); row++)
        {
            //Debug.Log()
            for(int column = 0; column < tileLayout.GetLength(row) - 1; column++)
            {
                Debug.Log("" + row + "," + column);
            }
        }
    }
}
