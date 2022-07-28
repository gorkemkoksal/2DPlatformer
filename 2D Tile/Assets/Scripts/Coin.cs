using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] int coinScore=100;
    bool isCollected = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag=="Player" && !isCollected)
        {
            isCollected = true;
            FindObjectOfType<GameSession>().AddScore(coinScore);
            Destroy(gameObject);
            
        }
    }

}
