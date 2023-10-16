using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MazeRenderer : MonoBehaviour
{


    [SerializeField]
    [Range(1, 50)]
    private int width = 10;

    [SerializeField]
    [Range(1, 50)]
    private int height = 10;



    [SerializeField]
    private Transform wallPrefab = null;

    [SerializeField]
    private Transform floorPrefab = null;



    private float size = 1f;
    void Start()
    {
        var maze = MazeGenerator.Generate(width, height);
        Draw(maze);
    }

    public void Draw(WallState[,] maze)
    {

        GameObject parentObject = new GameObject("FloorParent");
        GameObject parentWall = new GameObject("ParentWall");

        var floor = Instantiate(floorPrefab, parentObject.transform);
        floor.localScale = new Vector3(width,1, height);

        for(int i = 0; i < width; i++)
        {
            for(int j = 0; j < height; j++)
            {
                var cell = maze[i, j];
                var position = new Vector3(-width/2 + i, 0, -height/2 + j);

                if(cell.HasFlag(WallState.UP))
                {
                    var topWall = Instantiate(wallPrefab, parentWall.transform);
                    topWall.position = position + new Vector3(0,0,size/2);
                    topWall.localScale = new Vector3(size,topWall.localScale.y,topWall.localScale.z);
                }
                if (cell.HasFlag(WallState.LEFT))
                {
                    var leftWall = Instantiate(wallPrefab, parentWall.transform) ;
                    leftWall.position = position + new Vector3(-size/2, 0, 0);
                    leftWall.eulerAngles = new Vector3(0,90,0);
                    leftWall.localScale = new Vector3(size, leftWall.localScale.y, leftWall.localScale.z);
                }
                if (i == width - 1)
                {
                    if (cell.HasFlag(WallState.RIGHT))
                    {
                        var rightWall = Instantiate(wallPrefab, parentWall.transform);
                        rightWall.position = position + new Vector3(size / 2, 0, 0);
                        rightWall.eulerAngles = new Vector3(0, 90, 0);
                        rightWall.localScale = new Vector3(size, rightWall.localScale.y, rightWall.localScale.z);
                    }
                }
                if(j == 0)
                {
                    if (cell.HasFlag(WallState.DOWN))
                    {
                        var downWall = Instantiate(wallPrefab, parentWall.transform);
                        downWall.position = position + new Vector3(0, 0, -size / 2);
                        downWall.localScale = new Vector3(size, downWall.localScale.y, downWall.localScale.z);
                    }
                }
                
            }
        }
    }

}
