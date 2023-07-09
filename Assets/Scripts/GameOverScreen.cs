using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public Text dialogue;


    public void Loss(int InsertGameStateOfLossHere)
    {
        gameObject.SetActive(true);
        
    }

    public void RestartButton() 
    {
        SceneManager.LoadScene(1);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }

}
