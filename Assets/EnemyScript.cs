using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    private Rigidbody2D rb;
    public Vector2 randomDirection;

    public Sprite dieSpash;

    public GameObject canvas;

    public float shakeSpeed = 30f;
    public float shakeAmount = 8f;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //randomDirection = Random.insideUnitCircle.normalized;
        InvokeRepeating("ChangeDirection", 2f, 5f);
        //after 10 seconds call die
        Invoke("Die", 2f);
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

    public void Die()
    {
        //add die splash to position
        GameObject dieSplash = new GameObject();
        //add random scale
        float randomScale = Random.Range(1f, 6f);
        dieSplash.transform.localScale = new Vector3(randomScale, randomScale, 1);
        dieSplash.transform.position = transform.position;
        dieSplash.AddComponent<SpriteRenderer>().sprite = dieSpash;
        //sorting order should be lower than enemy
        dieSplash.GetComponent<SpriteRenderer>().sortingOrder = -1;
        //but higher than background
        dieSplash.GetComponent<SpriteRenderer>().sortingLayerName = "Background";

        //destroy the enemy
        Destroy(gameObject);
    }
}