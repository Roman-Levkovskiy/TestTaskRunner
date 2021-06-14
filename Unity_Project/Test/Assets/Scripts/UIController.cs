using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] GameObject restartPanel;
    [SerializeField] GameObject uiPanel;
    [SerializeField] GameObject winPanel;
    [SerializeField] GameObject pausePanel;
    [SerializeField] Text scoreText;
    private void Start()
    {
        uiPanel.SetActive(true);
        winPanel.SetActive(false);
        pausePanel.SetActive(false);
        restartPanel.SetActive(false);
    }
    public void changeScore(int score)
    {
        scoreText.text = "" + score;
    }
    public void looseTheGame()
    {
        uiPanel.SetActive(false);
        winPanel.SetActive(false);
        pausePanel.SetActive(false);
        restartPanel.SetActive(true);
    }
    public void restart()
    {
        uiPanel.SetActive(false);
        winPanel.SetActive(false);
        pausePanel.SetActive(false);
        restartPanel.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void nextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        /*
        if(SceneManager.GetActiveScene().buildIndex<levelsCount)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
            SceneManager.LoadScene(0);
        }
        */
    }
    public void mainMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void winTheGame()
    {
        uiPanel.SetActive(false);
        winPanel.SetActive(true);
        pausePanel.SetActive(false);
        restartPanel.SetActive(false);
    }
    public void pause()
    {
        winPanel.SetActive(false);
        pausePanel.SetActive(true);
        restartPanel.SetActive(false);
        Time.timeScale = 0.15f;
    }
    public void unpause()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1;
    }
}
