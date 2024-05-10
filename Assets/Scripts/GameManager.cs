using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject winPanel;
    public Text scoreText;
    int score;
    int coinCount;

    private void Start()
    {
        //coinCount = FindObjectsOfType<Coin>().Length;  
    }


    //public void GotCoin()
    //{
    //    score += 100;
    //    scoreText.text = $"Á¡¼ö: {score}";

    //    coinCount--;
    //    if(coinCount <= 0)
    //    {
    //        winPanel.SetActive(true);
    //    }
    //}

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
