using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public TextMeshProUGUI highScoreText;

    public GameObject pauseMenuUI;

    //private void OnEnable()
    //{
    //    highScoreText.gameObject.SetActive(false);
    //}

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1.0f;
        GameIsPaused = false;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        highScoreText.gameObject.SetActive(false);
    }
    
    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        highScoreText.gameObject.SetActive(true);

        float highScore = PlayerPrefs.GetFloat("HighScore", float.MaxValue);
        if(highScore != float.MaxValue)
        {
            highScoreText.text = "Best Time: " + highScore.ToString("F2") + "s";
        }
        else
        {
            highScoreText.text = "Best Time: N/A";
        }
    }

    public void LoadMenu()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(0);
    }

}
