﻿using UnityEngine;

public class PopUpManager : MonoBehaviour {


    [SerializeField] private GameObject Child;



    //To be done
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            Child.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Child.SetActive(false);
        }
    }

}