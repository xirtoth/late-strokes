using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 randomDirection;

    public float shakeSpeed = 30f;
    public float shakeAmount = 8f;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        randomDirection = Random.insideUnitCircle.normalized;
        InvokeRepeating("ChangeDirection", 5f, 5f);
    }

    // Update is called once per frame
    private void Update()
    {
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        // Generate a random direction
        //rotate the enemy between -8 and 8  in timespan of 2sec
        // transform.rotation = Quaternion.Euler(0, 0, Mathf.Sin(Time.time * shakeSpeed) * shakeAmount);


        // Apply the random direction to the enemy's velocity
        rb.velocity = randomDirection;

        // Change the direction every 5 seconds

    }

    private void ChangeDirection()
    {
        // Generate a new random direction
        randomDirection = Random.insideUnitCircle.normalized;

        // Apply the new random direction to the enemy's velocity
        rb.velocity = randomDirection;
    }
}