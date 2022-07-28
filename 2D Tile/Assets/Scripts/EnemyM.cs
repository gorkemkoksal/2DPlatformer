using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyM : MonoBehaviour
{
    Rigidbody2D enemyRB;
    [SerializeField] float enemySpeed = 2f;
    void Start()
    {
        enemyRB = GetComponent<Rigidbody2D>();
    }

    
    void Update()
    {
        enemyRB.velocity = new Vector2(enemySpeed, 0f);
    }
    void OnTriggerExit2D(Collider2D other)
    {
        enemySpeed = -enemySpeed;
        enemyFlip();

    }
    void enemyFlip()
    {
        transform.localScale = new Vector2(-(Mathf.Sign(enemyRB.velocity.x)), 1f);
    }
}
