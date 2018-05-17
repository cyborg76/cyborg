﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToEndGame : MonoBehaviour {
    private bool end = true;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (end)
        {
            if (other.name == "Player")
            {
                Invoke("EndGame", 2);
                end = false;
            }
        }
       
    }

    private void EndGame()
    {
        SceneManager.LoadScene("EndGameScreen");
    }
}
