using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float splashtime = 0.2f;
    void Start()
    {
        Destroy(gameObject, 2f);
    }

    // Update is called once per frame
    void Update()
    {
        //every splashtime seconds instantiate a small 2d circle same as bullets colour
        splashtime -= Time.deltaTime;
        if (splashtime <= 0)
        {
            GameObject splash = new GameObject();
            splash.transform.position = transform.position;
            splash.AddComponent<SpriteRenderer>().sprite = GetComponent<SpriteRenderer>().sprite;
            splash.GetComponent<SpriteRenderer>().color = GetComponent<SpriteRenderer>().color;
            //set colour alpha to half
            splash.GetComponent<SpriteRenderer>().color = new Color(splash.GetComponent<SpriteRenderer>().color.r, splash.GetComponent<SpriteRenderer>().color.g, splash.GetComponent<SpriteRenderer>().color.b, 0.5f);

            // Add a CircleCollider2D component to make it a 2D circle
            splash.AddComponent<CircleCollider2D>();
            //set collider inactive
            splash.GetComponent<CircleCollider2D>().enabled = false;
            //set layer to Capture
            splash.layer = 9;

            splash.tag = "Splash";

            // splash.AddComponent<FadeInScript>();

            // Set the scale to 0.1, 0.1, 1
            var randomScale = Random.Range(0.1f, 0.3f);
            splash.transform.localScale = new Vector3(randomScale, randomScale, 1f);

            splashtime = 0.2f;
        }
    }


    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Wall")
        {
            // change direction to opposite
            Destroy(gameObject);
        }

        if (other.gameObject.tag == "Enemy")
        {
            if (other.gameObject.GetComponent<EnemyScript>() != null)
            {
                other.gameObject.GetComponent<EnemyScript>().TakeDamage();
                Destroy(gameObject);
            }
            else if (other.gameObject.GetComponent<BallEnemyScript>() != null)
            {
                other.gameObject.GetComponent<BallEnemyScript>().TakeDamage();

                Destroy(gameObject);
            }
        }
    }
}
