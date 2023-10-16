using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Timer timer;

    private float startTime;
    private float bestTime = float.MaxValue;
    private bool isTimerRunning = false;

    public EndMessage endMessage;

    [SerializeField]
    public MazeRenderer mazeRenderer;

    [SerializeField]
    private WallState[,] generatedMaze;

    [SerializeField]
    public Player playerPrefab;
    private Player playerInstance;


    [SerializeField]
    [Range(1f,50f)]
    public int width = 10;
    

    [SerializeField]
    [Range(1, 50)]
    public int height = 10;

    [SerializeField]
    public Vector3 spawnPosition;

    [SerializeField]
    public Vector3[] vectorArray;

    [SerializeField]
    private Vector3 endPosition;

    private Vector3 correctEndPoint;

    void Start()
    {
        beginGame(mazeRenderer);
        spawnPlayer();
    }

    void Update()
    {
        CheckPlayerPosition();
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            restartGame();
        }
    }

    public void restartGame()
    {
        Destroy(mazeRenderer.gameObject);
        beginGame(mazeRenderer);
    }

    private void spawnPlayer()
    {
        if(playerPrefab != null)
        {
            playerInstance = Instantiate(playerPrefab,spawnPosition,Quaternion.identity);
        }
        else
        {
            Debug.LogError("Player prefab is not assigned!");
        }
    }

    public void beginGame(MazeRenderer mazeRenderer)
    {
        generatedMaze = MazeGenerator.Generate(width, height);
        mazeRenderer.Draw(generatedMaze);
        correctEndPoint = randomEndPoint();
        if (timer!= null)
        {
            timer.StartTimer();
            startTime = Time.time;
            isTimerRunning = true;
        }
    }

    public void PlayerReachedEnd()
    {
        if (isTimerRunning)
        {
            isTimerRunning = false;
            timer.StopTimer();
            if (endMessage != null)
            {
                endMessage.ShowText("Congrats you Completed the level");
            }

            float endTime = Time.time;
            float elapsedTime = endTime - startTime;

            if (elapsedTime < bestTime)
            {
                bestTime = elapsedTime;
                timer.SetBestTime(elapsedTime);
            }
        }
    }

    private Vector3 randomEndPoint()
    {
        vectorArray = new Vector3[]
        {
            new Vector3(4.0f,0.33f,-5.0f),
            new Vector3(-5.0f,0.33f,4.0f),
            new Vector3(-5.0f,0.33f,-5.0f)
        };
        int randIndex = UnityEngine.Random.Range(0, vectorArray.Length);
        endPosition = vectorArray[randIndex];
        Console.Write(endPosition.ToString());

        return endPosition;
    }
    private void CheckPlayerPosition()
    {
        if (playerInstance != null)
        {
            Vector3 playerPos = playerInstance.transform.position;

            float distance = Vector3.Distance(playerPos, correctEndPoint);
            if (distance < 0.3f)
            {
                PlayerReachedEnd();
            }
        }
    }
}
