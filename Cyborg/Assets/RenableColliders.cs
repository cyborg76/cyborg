﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenableColliders : MonoBehaviour {
    public GameObject colliderHolder;
    public AudioSource alarm;
    private bool firstCollide;

    private void Start()
    {
        firstCollide = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Player")
        {
            colliderHolder.SetActive(true);
            if (firstCollide)
            {
                alarm.Play();
                firstCollide = false;
                Invoke("StopAlarm", 5);
            }
        }


    }


    private void StopAlarm()
    {
        alarm.mute = true;
    }
    
}
