using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D rb;

    public float moveSpeed = 10f;

    public float dashForce = 10f;

    public GameObject bulletPrefab;

    public float bulletVelocity = 10f;


    public float shootCooldown = 0.5f;

    public float health = 100f;
    public float maxHelalth = 100f;

    public GameObject gc;

    public GameObject canvas;
    private Vector2 movement;

    private GameObject brush;

    private Animation anim;





    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gc = GameObject.FindGameObjectWithTag("GameController");
        canvas = GameObject.FindGameObjectWithTag("Canvas");
        //get brush it is child of this gameobject
        brush = transform.GetChild(0).gameObject;

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
            Debug.Log(brush.name);
            if (Time.time < shootCooldown)
            {
                return;
            }
            shootCooldown = Time.time + 0.5f;

            // Enable the collider when the player is attacking
            brush.GetComponent<Collider2D>().enabled = true;

            brush.GetComponent<Animation>().Play("Swing");


            // also shoot bullet towards mouse position
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = (mousePos - (Vector2)transform.position).normalized;
            ShootBullet(direction);

        }
        else
        {
            // Disable the collider when the player is not attacking
            brush.GetComponent<Collider2D>().enabled = false;
        }




    }

    private void FixedUpdate()
    {
        //rotate the player
        rb.velocity = movement * moveSpeed;
        //if player is moving right then rotate right
        if (movement.x > 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        //if player is moving left then rotate left
        else if (movement.x < 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        //when player is moving make it rotate to left and right rapidly using sin
        if (movement.x != 0 || movement.y != 0)
        {

            transform.rotation *= Quaternion.Euler(0, 0, 5 * Mathf.Sin(Time.time * 20));


        }
    }

    private void ShootBullet(Vector2 direction)
    {

        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        //make Time.timescale not affect bullet speed

        bullet.GetComponent<Rigidbody2D>().velocity = direction * bulletVelocity;
        //change bullet to random colour
        bullet.GetComponent<SpriteRenderer>().color = new Color(Random.value, Random.value, Random.value, 1);



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
        if (col.gameObject.tag == "Enemy" && !gameObject.CompareTag("Weapon"))
        {
            Debug.Log("Enemy hit +" + col.gameObject.name + " at " + gameObject.tag);
            //find all nearby object tagged as splash
            GameObject[] splashes = GameObject.FindGameObjectsWithTag("Splash");
            Debug.Log("found splashes" + splashes.Length);
            foreach (GameObject splash in splashes)
            {
                //if its 1 unit near to player in circular radius then destroy it

                if (Vector3.Distance(splash.transform.position, transform.position) < 4f)
                {
                    Destroy(splash);

                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Drug")
        {
            canvas.GetComponent<ShaderTurner>().TurnRainbow();
            gc.GetComponent<GameController>().canSpawnPowerUp = false;
            Destroy(col.gameObject);
        }
    }


}