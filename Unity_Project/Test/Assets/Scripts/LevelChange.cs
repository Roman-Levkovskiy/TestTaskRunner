using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChange : MonoBehaviour
{
    public void changeLevel(int levelNumber)
    {
        SceneManager.LoadScene(levelNumber);
    }
}
