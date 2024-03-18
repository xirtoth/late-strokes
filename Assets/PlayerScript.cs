using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D rb;

    public float moveSpeed = 10f;

    public GameObject bulletPrefab;

    public float bulletVelocity = 50f;

    private Vector2 movement;
    public float shootCooldown = 0.5f;

    public float health = 100f;
    public float maxHelalth = 100f;

    public GameObject gc;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gc = GameObject.FindGameObjectWithTag("GameController");
    }

    // Update is called once per frame
    private void Update()
    {
        //use input getaxis to move the player
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        movement = new Vector2(moveHorizontal, moveVertical);

        // shoot towards mouse position
        if (Input.GetMouseButton(0))
        {
            if (Time.time < shootCooldown)
            {
                return;
            }
            shootCooldown = Time.time + 0.5f;


            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = (mousePos - transform.position).normalized;
            ShootBullet(direction);
        }



    }

    private void FixedUpdate()
    {
        //rotate the player
        rb.velocity = movement * moveSpeed;
        //if player is moving right then rotate right
        if (movement.x > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        //if player is moving left then rotate left
        else if (movement.x < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }

    private void ShootBullet(Vector2 direction)
    {

        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody2D>().velocity = direction * bulletVelocity;



    }


    public void TakeDamage(int amount)
    {
        health -= amount;
        gc.GetComponent<UIcontroller>().UpdateHealth(health);
        if (health <= 0)
        {
            health = 100;
        }
    }

    public void Die()
    {
        //load level again
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            TakeDamage(10);
        }
    }


}