using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private Gamemanager _gamemanager;

    private void Start()
    {
        _gamemanager = FindObjectOfType<Gamemanager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("player"))
        {
            _gamemanager.IncreaseCoin();
            Destroy(gameObject);
        }
    }
    
}
