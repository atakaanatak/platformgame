using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamemanager : MonoBehaviour
{

    private int _coinNumber;
    public GameObject panel;
    public GameObject youwin;
    public GameObject youlose;
    


    public void IncreaseCoin()
    {
        _coinNumber++;
        if (_coinNumber == 4)
        {
            //win game
            YouWin();
        } 
    }

    public void YouWin()
    {
        panel.SetActive(true);
        youwin.SetActive(true);
    }

    public void Youlose()
    {
        panel.SetActive(true);
        youlose.SetActive(true);
    }
}
