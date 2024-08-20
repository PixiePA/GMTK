using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FinishController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Something entered");
        if (collision.tag == "Player")
        {
            Debug.Log("Player Touched Goal");
            GameEvents.GoalReached();
        }
    }


}
