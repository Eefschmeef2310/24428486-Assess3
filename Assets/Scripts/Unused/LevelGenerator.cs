using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private GameObject oldLevel;
    [SerializeField] private GameObject tile;
    private SpriteRenderer spriteRenderer;
    public List<Sprite> tiles;
    public Sprite[] notWallSprites;
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

    private List<int> notWalls = new List<int>(){0,0,0,0,0,5,6,0}; //I'm using this to check surrounding tiles
    private Vector3 startPos;
    private Vector3 position;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = tile.GetComponent<SpriteRenderer>();
        Destroy(oldLevel);
        generateLevel();
    }

    void generateLevel()
    {
        for(int iterations = 0; iterations < 3; iterations++)
        {
            for(int corner = 1; corner < 5; corner++)
            {
                for(int row = 0; row < tileLayout.GetLength(0); row++)
                {
                    for(int column = 0; column < tileLayout.GetLength(1); column++)
                    {          
                        getCorner(corner, row, column);   
                        if(corner != 4 && corner != 3)
                        {
                            if(iterations == 0 && tileLayout[row, column] != 0)
                            {
                                if(tileLayout[row,column] == 6)
                                {
                                    spriteRenderer.sprite = notWallSprites[tileLayout[row,column]];
                                    Instantiate(tile, position, Quaternion.identity);
                                }
                                else if (tileLayout[row,column] == 5)
                                {
                                    spriteRenderer.sprite = notWallSprites[tileLayout[row,column]];
                                    Instantiate(tile, position, Quaternion.identity);
                                }
                                else
                                {
                                    spriteRenderer.sprite = tiles[tileLayout[row,column]];
                                    Instantiate(tile, position, Quaternion.identity);
                                }
                            }
                        }
                        else
                        {
                            if(row != tileLayout.GetLength(0) - 1)
                            {
                                if(iterations == 0 && tileLayout[row, column] != 0)
                                {
                                    if(tileLayout[row,column] == 6)
                                    {
                                        spriteRenderer.sprite = notWallSprites[tileLayout[row,column]];
                                        Instantiate(tile, position, Quaternion.identity);
                                    }
                                    else if (tileLayout[row,column] == 5)
                                    {
                                        spriteRenderer.sprite = notWallSprites[tileLayout[row,column]];
                                        Instantiate(tile, position, Quaternion.identity);
                                    }
                                    else
                                    {
                                        spriteRenderer.sprite = tiles[tileLayout[row,column]];
                                        Instantiate(tile, position, Quaternion.identity);
                                    }
                                }
                            }
                        }
                        
                        
                        //Set rotation for walls
                        if(iterations == 1)
                        {
                            if(Physics.CheckSphere(position, 0.1f))
                            {
                                tile = Physics.OverlapSphere(position, 0.1f)[0].gameObject;
                            
                                switch(tileLayout[row,column])
                                {
                                    case 2:
                                        tile.transform.rotation = Quaternion.Euler(0,0,90*getRotationAmountStraightThick(position));
                                        break;
                                    case 4:
                                        tile.transform.rotation = Quaternion.Euler(0,0,90*getRotationAmountStraightThin(position));
                                        break;
                                }
                            }
                        } 
                        //set rotation for corners
                        else if(iterations == 2)
                        {
                            if(Physics.CheckSphere(position, 0.1f))
                            {
                                tile = Physics.OverlapSphere(position, 0.1f)[0].gameObject;
                                switch(tileLayout[row,column])
                                {
                                    case 1:
                                        tile.transform.rotation = Quaternion.Euler(0,0,90*getRotationAmountCorner(position));
                                        break;
                                    case 3:
                                        tile.transform.rotation = Quaternion.Euler(0,0,90*getRotationAmountCorner(position));
                                        break;
                                    case 7:
                                        tile.transform.rotation = Quaternion.Euler(180*getRotationAmountJunctionX(position),180*getRotationAmountJunctionY(position),0);
                                        break;
                                }   
                            } 
                            spriteRenderer.sprite = null;
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
                startPos = new Vector3(-tileLayout.GetLength(0)+1,tileLayout.GetLength(0)-1,0);
                position = new Vector3(startPos.x + column + 0.5f, startPos.y - row + 0.5f, 0);
                break;
            case 2:
                startPos = new Vector3(tileLayout.GetLength(1)-1,tileLayout.GetLength(0)-1,0);
                position = new Vector3(startPos.x - column + 0.5f, startPos.y - row + 0.5f, 0);
                break;
            case 3:
                startPos = new Vector3(tileLayout.GetLength(1)-1,-tileLayout.GetLength(0)+1,0);
                position = new Vector3(startPos.x - column + 0.5f, startPos.y + row + 0.5f, 0);
                break;
            case 4:
                startPos = new Vector3(-tileLayout.GetLength(0)+1,-tileLayout.GetLength(0)+1,0);
                position = new Vector3(startPos.x + column + 0.5f, startPos.y + row + 0.5f, 0);
                break;
        }
    }

    int getRotationAmountCorner(Vector3 position)
    {
        //check if all four surroundign tiles are walls (right, down, left, up)
        if(Physics.CheckSphere(new Vector3(position.x + 1, position.y, 0), 0.1f) && Physics.CheckSphere(new Vector3(position.x, position.y - 1), 0.1f) && Physics.CheckSphere(new Vector3(position.x - 1, position.y,0), 0.1f) && Physics.CheckSphere(new Vector3(position.x, position.y + 1,0), 0.1f))
        {
            if(tiles.Contains(Physics.OverlapSphere(new Vector3(position.x + 1, position.y,0), 0.1f)[0].gameObject.GetComponent<SpriteRenderer>().sprite) && tiles.Contains(Physics.OverlapSphere(new Vector3(position.x, position.y - 1,0), 0.1f)[0].gameObject.GetComponent<SpriteRenderer>().sprite) && tiles.Contains(Physics.OverlapSphere(new Vector3(position.x - 1, position.y,0), 0.1f)[0].gameObject.GetComponent<SpriteRenderer>().sprite) && tiles.Contains(Physics.OverlapSphere(new Vector3(position.x, position.y + 1,0), 0.1f)[0].gameObject.GetComponent<SpriteRenderer>().sprite))
            {
                return smallCornerRotation(position);
            }
            //if above is true, tile is surrounded by walls (corner or straight) and must be a small corner
        }
        //right and down
        if(Physics.CheckSphere(new Vector3(position.x + 1, position.y, 0), 0.1f) && Physics.CheckSphere(new Vector3(position.x, position.y - 1, 0), 0.1f))
        {
            if(tiles.Contains(Physics.OverlapSphere(new Vector3(position.x + 1, position.y, 0), 0.1f)[0].gameObject.GetComponent<SpriteRenderer>().sprite) && tiles.Contains(Physics.OverlapSphere(new Vector3(position.x, position.y - 1,0), 0.1f)[0].gameObject.GetComponent<SpriteRenderer>().sprite))
            {
                return 0;
            }
        }
        if(Physics.CheckSphere(new Vector3(position.x, position.y + 1, 0), 0.1f) && Physics.CheckSphere(new Vector3(position.x + 1, position.y, 0), 0.1f))
        {
            //up and right
            if(tiles.Contains(Physics.OverlapSphere(new Vector3(position.x, position.y + 1,0), 0.1f)[0].gameObject.GetComponent<SpriteRenderer>().sprite) && tiles.Contains(Physics.OverlapSphere(new Vector3(position.x + 1, position.y,0), 0.1f)[0].gameObject.GetComponent<SpriteRenderer>().sprite))
            {
                return 1;
            }
        }
        if(Physics.CheckSphere(new Vector3(position.x - 1, position.y, 0), 0.1f) && Physics.CheckSphere(new Vector3(position.x, position.y + 1, 0), 0.1f))
        {
            //left and up
            if(tiles.Contains(Physics.OverlapSphere(new Vector3(position.x - 1, position.y,0), 0.1f)[0].gameObject.GetComponent<SpriteRenderer>().sprite) && tiles.Contains(Physics.OverlapSphere(new Vector3(position.x, position.y + 1,0), 0.1f)[0].gameObject.GetComponent<SpriteRenderer>().sprite))
            {
                return 2;
            }
        }
        if(Physics.CheckSphere(new Vector3(position.x, position.y - 1, 0), 0.1f) && Physics.CheckSphere(new Vector3(position.x - 1, position.y, 0), 0.1f))
        {
            //down and left
            if(tiles.Contains(Physics.OverlapSphere(new Vector3(position.x, position.y - 1,0), 0.1f)[0].gameObject.GetComponent<SpriteRenderer>().sprite) && tiles.Contains(Physics.OverlapSphere(new Vector3(position.x - 1, position.y,0), 0.1f)[0].gameObject.GetComponent<SpriteRenderer>().sprite))
            {
                return 3;
            }
        }
        return 0;
    }

    int smallCornerRotation(Vector3 position)
    {
        //right-down
        if(Physics.OverlapSphere(new Vector3(position.x + 1, position.y,0), 0.1f)[0].gameObject.GetComponent<SpriteRenderer>().sprite == Physics.OverlapSphere(new Vector3(position.x, position.y - 1, 0), 0.1f)[0].gameObject.GetComponent<SpriteRenderer>().sprite)
        {
            if(Physics.OverlapSphere(new Vector3(position.x + 1, position.y, 0),0.1f)[0].transform.rotation.eulerAngles.z != Physics.OverlapSphere(new Vector3(position.x, position.y - 1, 0), 0.1f)[0].transform.rotation.eulerAngles.z)
            {
                return 0;
            }
        }
        
        //up-right
        if(Physics.OverlapSphere(new Vector3(position.x, position.y + 1,0), 0.1f)[0].gameObject.GetComponent<SpriteRenderer>().sprite == Physics.OverlapSphere(new Vector3(position.x + 1, position.y, 0), 0.1f)[0].gameObject.GetComponent<SpriteRenderer>().sprite)
        {
            if(Physics.OverlapSphere(new Vector3(position.x, position.y + 1, 0),0.1f)[0].transform.rotation.eulerAngles.z != Physics.OverlapSphere(new Vector3(position.x + 1, position.y, 0),0.1f)[0].transform.rotation.eulerAngles.z)
            {
                return 1;
            }
        }
        //left-up
        if(Physics.OverlapSphere(new Vector3(position.x - 1, position.y,0), 0.1f)[0].gameObject.GetComponent<SpriteRenderer>().sprite == Physics.OverlapSphere(new Vector3(position.x, position.y + 1, 0), 0.1f)[0].gameObject.GetComponent<SpriteRenderer>().sprite)
        {
            if(Physics.OverlapSphere(new Vector3(position.x - 1, position.y, 0),0.1f)[0].transform.rotation.eulerAngles.z != Physics.OverlapSphere(new Vector3(position.x, position.y + 1, 0),0.1f)[0].transform.rotation.eulerAngles.z)
            {
                return 2;
            }
        }
        //down-left
        if(Physics.OverlapSphere(new Vector3(position.x, position.y - 1,0), 0.1f)[0].gameObject.GetComponent<SpriteRenderer>().sprite == Physics.OverlapSphere(new Vector3(position.x - 1, position.y, 0), 0.1f)[0].gameObject.GetComponent<SpriteRenderer>().sprite)
        {
            if(Physics.OverlapSphere(new Vector3(position.x, position.y - 1, 0),0.1f)[0].transform.rotation.eulerAngles.z != Physics.OverlapSphere(new Vector3(position.x - 1, position.y, 0),0.1f)[0].transform.rotation.eulerAngles.z)
            {
                return 3;
            }
        }
        //Debug.Log(position);
        return -1;
    }

    int getRotationAmountJunctionZ(Vector3 position)
    {
        //Debug.Log(tiles.IndexOf(Physics.OverlapSphere(new Vector3(position.x, position.y - 1, 0))));
        //right-down
        if(Physics.CheckSphere(new Vector3(position.x + 1, position.y, 0), 0.1f) && Physics.CheckSphere(new Vector3(position.x, position.y - 1, 0), 0.1f))
        {
            if(tiles.IndexOf(Physics.OverlapSphere(new Vector3(position.x + 1, position.y, 0), 0.1f)[0].gameObject.GetComponent<SpriteRenderer>().sprite) == 2 && tiles.IndexOf(Physics.OverlapSphere(new Vector3(position.x, position.y - 1, 0), 0.1f)[0].gameObject.GetComponent<SpriteRenderer>().sprite) == 4)
            {
                return 0;
            }
        }
        //up-right
        if(Physics.CheckSphere(new Vector3(position.x, position.y + 1, 0), 0.1f) && Physics.CheckSphere(new Vector3(position.x + 1, position.y, 0), 0.1f))
        {
            if(tiles.IndexOf(Physics.OverlapSphere(new Vector3(position.x, position.y + 1, 0), 0.1f)[0].gameObject.GetComponent<SpriteRenderer>().sprite) == 4 && tiles.IndexOf(Physics.OverlapSphere(new Vector3(position.x + 1, position.y, 0), 0.1f)[0].gameObject.GetComponent<SpriteRenderer>().sprite) == 2)
            {
                return 1;
            }
        }
        //down-left
        if(Physics.CheckSphere(new Vector3(position.x - 1, position.y, 0), 0.1f) && Physics.CheckSphere(new Vector3(position.x, position.y + 1, 0), 0.1f))
        {
            if(tiles.IndexOf(Physics.OverlapSphere(new Vector3(position.x - 1, position.y, 0), 0.1f)[0].gameObject.GetComponent<SpriteRenderer>().sprite) == 2 && tiles.IndexOf(Physics.OverlapSphere(new Vector3(position.x, position.y + 1, 0), 0.1f)[0].gameObject.GetComponent<SpriteRenderer>().sprite) == 4)
            {
                return 2;
            }
        }
        //down-right
        if(Physics.CheckSphere(new Vector3(position.x, position.y - 1, 0), 0.1f) && Physics.CheckSphere(new Vector3(position.x - 1, position.y, 0), 0.1f))
        {
            if(tiles.IndexOf(Physics.OverlapSphere(new Vector3(position.x, position.y - 1, 0), 0.1f)[0].gameObject.GetComponent<SpriteRenderer>().sprite) == 4 && tiles.IndexOf(Physics.OverlapSphere(new Vector3(position.x - 1, position.y, 0), 0.1f)[0].gameObject.GetComponent<SpriteRenderer>().sprite) == 2)
            {
                return 3;
            }
        }
        return -1;
    }

    int getRotationAmountJunctionY(Vector3 position)
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
    int getRotationAmountJunctionX(Vector3 position)
    {
        if(Physics.CheckSphere(new Vector3(position.x, position.y + 1, 0), 0.1f))
        {
            return 1;
        }
        return 0;
    }

    int getRotationAmountStraightThick(Vector3 position)
    {
        //left-right
        if(Physics.CheckSphere(new Vector3(position.x + 1, position.y,0), 0.1f) && Physics.CheckSphere(new Vector3(position.x - 1, position.y,0), 0.1f))
        {
            if(tiles.Contains(Physics.OverlapSphere(new Vector3(position.x + 1, position.y,0), 0.1f)[0].gameObject.GetComponent<SpriteRenderer>().sprite) && tiles.Contains(Physics.OverlapSphere(new Vector3(position.x - 1, position.y,0), 0.1f)[0].gameObject.GetComponent<SpriteRenderer>().sprite))
            {
                return 1;
            }
        }
        
        //up-down
        if(Physics.CheckSphere(new Vector3(position.x, position.y + 1,0), 0.1f) && Physics.CheckSphere(new Vector3(position.x, position.y - 1,0), 0.1f))
        {
            if(tiles.Contains(Physics.OverlapSphere(new Vector3(position.x, position.y + 1,0), 0.1f)[0].gameObject.GetComponent<SpriteRenderer>().sprite) && tiles.Contains(Physics.OverlapSphere(new Vector3(position.x, position.y - 1,0), 0.1f)[0].gameObject.GetComponent<SpriteRenderer>().sprite))
            {
                return 0;
            }
        }
        return 1;
    }
    int getRotationAmountStraightThin(Vector3 position)
    {
        //Debug.Log(position + "test " + (!tiles.Contains(Physics.OverlapSphere(new Vector3(position.x + 1, position.y,0), 0.1f)[0].gameObject.GetComponent<SpriteRenderer>().sprite.GetComponent<SpriteRenderer>().sprite)) && notWalls.Contains(Array.IndexOf(notWallSprites, Physics.OverlapSphere(new Vector3(position.x - 1, position.y,0), 0.1f)[0].gameObject.GetComponent<SpriteRenderer>().sprite.GetComponent<SpriteRenderer>().sprite))));
        //left-right
        if(Physics.CheckSphere(new Vector3(position.x + 1, position.y, 0), 0.1f) && Physics.CheckSphere(new Vector3(position.x - 1, position.y, 0), 0.1f))
        {
            if(tiles.Contains(Physics.OverlapSphere(new Vector3(position.x + 1, position.y,0), 0.1f)[0].gameObject.GetComponent<SpriteRenderer>().sprite) && tiles.Contains(Physics.OverlapSphere(new Vector3(position.x - 1, position.y,0), 0.1f)[0].gameObject.GetComponent<SpriteRenderer>().sprite))
            {
                return 0;
            }
        }
        //up-down
        if(Physics.CheckSphere(new Vector3(position.x, position.y - 1, 0), 0.1f) && Physics.CheckSphere(new Vector3(position.x, position.y + 1, 0), 0.1f))
        {
            if(tiles.Contains(Physics.OverlapSphere(new Vector3(position.x, position.y + 1,0), 0.1f)[0].gameObject.GetComponent<SpriteRenderer>().sprite) && tiles.Contains(Physics.OverlapSphere(new Vector3(position.x, position.y - 1,0), 0.1f)[0].gameObject.GetComponent<SpriteRenderer>().sprite))
            {
                return 1;
            }
        }        
        return 0;
    }
}