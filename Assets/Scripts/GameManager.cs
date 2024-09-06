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
    [Range(1f, 50f)]
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
        BeginGame();
    }

    void Update()
    {
        CheckPlayerPosition();
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            RestartGame();
        }
    }

    public void RestartGame()
    {
        Cleanup();
        BeginGame();
    }

    private void Cleanup()
    {
        if (playerInstance != null)
        {
            Destroy(playerInstance.gameObject);
        }

        if (mazeRenderer != null)
        {
            Destroy(mazeRenderer.gameObject); // Assuming your MazeRenderer has a method to clear the previous maze
        }
    }

    private void SpawnPlayer()
    {
        if (playerPrefab != null)
        {
            playerInstance = Instantiate(playerPrefab, spawnPosition, Quaternion.identity);
        }
        else
        {
            Debug.LogError("Player prefab is not assigned!");
        }
    }

    public void BeginGame()
    {
        generatedMaze = MazeGenerator.Generate(width, height);
        mazeRenderer.Draw(generatedMaze);

        correctEndPoint = RandomEndPoint();

        SpawnPlayer();

        if (timer != null)
        {
            timer.StartTimer();
            startTime = Time.time;
            isTimerRunning = true;
        }

        // Ensure game is unpaused
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void PlayerReachedEnd()
    {
        if (isTimerRunning)
        {
            isTimerRunning = false;
            timer.StopTimer();
            
            float endTime = Time.time;
            float elapsedTime = endTime - startTime;

            if (elapsedTime < bestTime)
            {
                bestTime = elapsedTime;
                timer.SetBestTime(elapsedTime);
                PlayerPrefs.SetFloat("HighScore", bestTime);
            }

            if (endMessage != null)
            {
                endMessage.ShowText("Congrats, you completed the level!");
            }

        }
    }

    private Vector3 RandomEndPoint()
    {
        vectorArray = new Vector3[]
        {
            new Vector3(4.0f,0.33f,-5.0f),
            new Vector3(-5.0f,0.33f,4.0f),
            new Vector3(-5.0f,0.33f,-5.0f)
        };

        int randIndex = UnityEngine.Random.Range(0, vectorArray.Length);
        endPosition = vectorArray[randIndex];
        Debug.Log("End Position: " + endPosition.ToString());

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
