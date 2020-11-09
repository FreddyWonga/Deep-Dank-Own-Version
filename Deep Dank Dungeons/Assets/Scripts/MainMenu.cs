using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    //Load the first level when the play game button is pressed
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    //Load the leaderboard scene when the leaderboard button is pressed
    public void LeaderboardScene()
    {
        SceneManager.LoadScene("HighScoreTable");
    }

    //Load the main menu when the main menu button is pressed
    public void MainMenuScene()
    {
        SceneManager.LoadScene("MainMenu");
    }

    //Quits the game when the quit button is pressed
    public void QuitGame()
    {
        Application.Quit();
    }
}