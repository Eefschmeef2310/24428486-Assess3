using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LevelGenerator : MonoBehaviour
{
    public GameObject oldLevel;
    public Tilemap tilemap;
    public Tile[] tiles;
    public RuleTile[] ruleTiles;
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

    private List<int> notWalls = new List<int>(){0, 5, 6}; //I'm using this to check surrounding tiles
    private Vector3Int startPos;
    private Vector3Int position;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(oldLevel);
        //startPos = new Vector3Int(-14,14,0);
        generateLevel();
        //tilemap.SetTile(startPos, tiles[1]);
    }

    void generateLevel()
    {
        for(int iterations = 0; iterations < 3; iterations++)
        {
            for(int corner = 1; corner < 5; corner++)
            {
                
                //Debug.Log(tileLayout.GetLength(0));
                for(int row = 0; row < tileLayout.GetLength(0); row++)
                {
                    for(int column = 0; column < tileLayout.GetLength(1); column++)
                    {             
                        getCorner(corner, row, column);       
                        //Debug.Log("" + row + "," + column);
                        if(tileLayout[row,column] == 6)
                        {
                            tilemap.SetTile(position, ruleTiles[tileLayout[row,column]]);
                        }
                        else if (tileLayout[row,column] == 5)
                        {
                            tilemap.SetTile(position, ruleTiles[tileLayout[row,column]]);
                        }
                        else
                        {
                            tilemap.SetTile(position, tiles[tileLayout[row,column]]);
                        }
                        //Tile setTile = tilemap.GetTile(position);


                        //Set rotationfor walls
                        if(iterations == 1)
                        {
                            switch(tileLayout[row,column])
                            {
                                case 2:
                                    tilemap.SetTransformMatrix(position, Matrix4x4.TRS(Vector3.zero, Quaternion.Euler(0,0,90*getRotationAmountStraightThick(position)),Vector3.one));
                                    break;
                                case 4:
                                    tilemap.SetTransformMatrix(position, Matrix4x4.TRS(Vector3.zero, Quaternion.Euler(0,0,90*getRotationAmountStraightThin(position)),Vector3.one));
                                    break;
                            }
                        } 
                        //set rotation for corners
                        else if(iterations == 2)
                        {
                            switch(tileLayout[row,column])
                            {
                                case 1:
                                    tilemap.SetTransformMatrix(position, Matrix4x4.TRS(Vector3.zero, Quaternion.Euler(0,0,90*getRotationAmountCorner(position)),Vector3.one));
                                    break;
                                case 3:
                                    tilemap.SetTransformMatrix(position, Matrix4x4.TRS(Vector3.zero, Quaternion.Euler(0,0,90*getRotationAmountCorner(position)),Vector3.one));
                                    break;
                                case 7:
                                    tilemap.SetTransformMatrix(position, Matrix4x4.TRS(Vector3.zero, Quaternion.Euler(180*getRotationAmountJunctionX(position),180*getRotationAmountJunctionY(position),0),Vector3.one));
                                    //tilemap.SetTransformMatrix(position, Matrix4x4.TRS(Vector3.zero, Quaternion.Euler(0,180*getRotationAmountJunctionY(position),0),Vector3.one));
                                    break;
                            }   
                            
                        }
                    }
                }
            }
        }
    }

    void getCorner(int corner, int row, int column)
    {
        switch(corner)
        {
            case 1:
                startPos = new Vector3Int(-14,14,0);
                position = new Vector3Int(startPos.x + column, startPos.y - row, 0);
                break;
            case 2:
                startPos = new Vector3Int(13,14,0);
                position = new Vector3Int(startPos.x - column, startPos.y - row, 0);
                break;
            case 3:
                startPos = new Vector3Int(13,-14,0);
                position = new Vector3Int(startPos.x - column, startPos.y + row, 0);
                break;
            case 4:
                startPos = new Vector3Int(-14,-14,0);
                position = new Vector3Int(startPos.x + column, startPos.y + row, 0);
                break;
        }
    }

    int getRotationAmountCorner(Vector3Int position)
    {
        if(!notWalls.Contains(Array.IndexOf(ruleTiles, tilemap.GetTile(new Vector3Int(position.x + 1, position.y, 0)))) && !notWalls.Contains(Array.IndexOf(ruleTiles, tilemap.GetTile(new Vector3Int(position.x, position.y - 1,0)))) && !notWalls.Contains(Array.IndexOf(ruleTiles, tilemap.GetTile(new Vector3Int(position.x - 1, position.y,0)))) && !notWalls.Contains(Array.IndexOf(ruleTiles, tilemap.GetTile(new Vector3Int(position.x, position.y + 1,0)))))
        {
            //if above is true, tile is surrounded by walls (corner or straight) and must be a small corner
            return smallCornerRotation(position);
        }
        else
        {
            //right and down
            if(!notWalls.Contains(Array.IndexOf(ruleTiles, tilemap.GetTile(new Vector3Int(position.x + 1, position.y, 0)))) && !notWalls.Contains(Array.IndexOf(ruleTiles, tilemap.GetTile(new Vector3Int(position.x, position.y - 1,0)))))
            {
                return 0;
            }
            //up and right
            else if(!notWalls.Contains(Array.IndexOf(ruleTiles, tilemap.GetTile(new Vector3Int(position.x, position.y + 1,0)))) && !notWalls.Contains(Array.IndexOf(ruleTiles, tilemap.GetTile(new Vector3Int(position.x + 1, position.y,0)))))
            {
                return 1;
            }
            //left and up
            else if(!notWalls.Contains(Array.IndexOf(ruleTiles, tilemap.GetTile(new Vector3Int(position.x - 1, position.y,0)))) && !notWalls.Contains(Array.IndexOf(ruleTiles, tilemap.GetTile(new Vector3Int(position.x, position.y + 1,0)))))
            {
                return 2;
            }
            //down and left
            else if(!notWalls.Contains(Array.IndexOf(ruleTiles, tilemap.GetTile(new Vector3Int(position.x, position.y - 1,0)))) && !notWalls.Contains(Array.IndexOf(ruleTiles, tilemap.GetTile(new Vector3Int(position.x - 1, position.y,0)))))
            {
                return 3;
            }

            //if they are inside corners surrounded by walls
            

            return 0;
        }
    }

    int smallCornerRotation(Vector3Int position)
    {
        //right-down
        if(Array.IndexOf(tiles, tilemap.GetTile(new Vector3Int(position.x + 1, position.y, 0))) == 4 && Array.IndexOf(tiles, tilemap.GetTile(new Vector3Int(position.x, position.y - 1, 0))) == 4)
        {
            if(tilemap.GetTransformMatrix(new Vector3Int(position.x + 1, position.y, 0)).rotation.eulerAngles.z != tilemap.GetTransformMatrix(new Vector3Int(position.x, position.y - 1, 0)).rotation.eulerAngles.z)
            {
                return 0;
            }
        }
        //up-right
        if(Array.IndexOf(tiles, tilemap.GetTile(new Vector3Int(position.x, position.y + 1, 0))) == 4 && Array.IndexOf(tiles, tilemap.GetTile(new Vector3Int(position.x + 1, position.y, 0))) == 4)
        {
            if(tilemap.GetTransformMatrix(new Vector3Int(position.x, position.y + 1, 0)).rotation.eulerAngles.z != tilemap.GetTransformMatrix(new Vector3Int(position.x + 1, position.y, 0)).rotation.eulerAngles.z)
            {
                return 1;
            }
        }
        //left-up
        if(Array.IndexOf(tiles, tilemap.GetTile(new Vector3Int(position.x - 1, position.y, 0))) == 4 && Array.IndexOf(tiles, tilemap.GetTile(new Vector3Int(position.x, position.y + 1, 0))) == 4)
        {
            if(tilemap.GetTransformMatrix(new Vector3Int(position.x - 1, position.y, 0)).rotation.eulerAngles.z != tilemap.GetTransformMatrix(new Vector3Int(position.x, position.y + 1, 0)).rotation.eulerAngles.z)
            {
                return 2;
            }
        }
        //down-left
        if(Array.IndexOf(tiles, tilemap.GetTile(new Vector3Int(position.x, position.y - 1, 0))) == 4 && Array.IndexOf(tiles, tilemap.GetTile(new Vector3Int(position.x - 1, position.y, 0))) == 4)
        {
            if(tilemap.GetTransformMatrix(new Vector3Int(position.x, position.y - 1, 0)).rotation.eulerAngles.z != tilemap.GetTransformMatrix(new Vector3Int(position.x - 1, position.y, 0)).rotation.eulerAngles.z)
            {
                return 3;
            }
        }
        return -1;
    }

    int getRotationAmountJunctionZ(Vector3Int position)
    {
        //Debug.Log(Array.IndexOf(tiles, tilemap.GetTile(new Vector3Int(position.x, position.y - 1, 0))));
        //right-down
        if(Array.IndexOf(tiles, tilemap.GetTile(new Vector3Int(position.x + 1, position.y, 0))) == 2 && Array.IndexOf(tiles, tilemap.GetTile(new Vector3Int(position.x, position.y - 1, 0))) == 4)
        {
            return 0;
        }
        //up-right
        if(Array.IndexOf(tiles, tilemap.GetTile(new Vector3Int(position.x, position.y + 1, 0))) == 4 && Array.IndexOf(tiles, tilemap.GetTile(new Vector3Int(position.x + 1, position.y, 0))) == 2)
        {
            return 1;
        }
        //down-left
        if(Array.IndexOf(tiles, tilemap.GetTile(new Vector3Int(position.x - 1, position.y, 0))) == 2 && Array.IndexOf(tiles, tilemap.GetTile(new Vector3Int(position.x, position.y + 1, 0))) == 4)
        {
            return 2;
        }
        //down-right
        if(Array.IndexOf(tiles, tilemap.GetTile(new Vector3Int(position.x, position.y - 1, 0))) == 4 && Array.IndexOf(tiles, tilemap.GetTile(new Vector3Int(position.x - 1, position.y, 0))) == 2)
        {
            return 3;
        }
        return -1;
    }

    int getRotationAmountJunctionY(Vector3Int position)
    {
        switch(getRotationAmountJunctionZ(position))
        {
            case 0: 
                return 0;
            case 1:
                return 0;
            case 2:
                return 1;
            case 3: 
                return 1;
        }
        return -1;
    }
    int getRotationAmountJunctionX(Vector3Int position)
    {
        if((Array.IndexOf(tiles, tilemap.GetTile(new Vector3Int(position.x, position.y + 1, 0))) == 4 || Array.IndexOf(tiles, tilemap.GetTile(new Vector3Int(position.x, position.y + 1, 0))) == 2) && (Array.IndexOf(tiles, tilemap.GetTile(new Vector3Int(position.x, position.y - 1, 0))) != 4 || Array.IndexOf(tiles, tilemap.GetTile(new Vector3Int(position.x, position.y - 1, 0))) != 2))
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }

    int getRotationAmountStraightThick(Vector3Int position)
    {
        //left-right
        if(!notWalls.Contains(Array.IndexOf(ruleTiles, tilemap.GetTile(new Vector3Int(position.x + 1, position.y,0)))) && !notWalls.Contains(Array.IndexOf(ruleTiles, tilemap.GetTile(new Vector3Int(position.x - 1, position.y,0)))))
        {
            return 1;
        }
        //up-down
        else if(!notWalls.Contains(Array.IndexOf(ruleTiles, tilemap.GetTile(new Vector3Int(position.x, position.y + 1,0)))) && !notWalls.Contains(Array.IndexOf(ruleTiles, tilemap.GetTile(new Vector3Int(position.x, position.y - 1,0)))))
        {
            return 0;
        }
        
        return 1;
    }

    int getRotationAmountStraightThin(Vector3Int position)
    {
        //left-right
        if(!notWalls.Contains(Array.IndexOf(ruleTiles, tilemap.GetTile(new Vector3Int(position.x + 1, position.y,0)))) && !notWalls.Contains(Array.IndexOf(ruleTiles, tilemap.GetTile(new Vector3Int(position.x - 1, position.y,0)))))
        {
            return 0;
        }
        //up-down
        else if(!notWalls.Contains(Array.IndexOf(ruleTiles, tilemap.GetTile(new Vector3Int(position.x, position.y + 1,0)))) && !notWalls.Contains(Array.IndexOf(ruleTiles, tilemap.GetTile(new Vector3Int(position.x, position.y - 1,0)))))
        {
            return 1;
        }
        
        return 0;
    }
}
