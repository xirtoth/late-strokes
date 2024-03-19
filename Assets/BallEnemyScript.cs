using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallEnemyScript : MonoBehaviour
{
    public float moveSpeed = 0.02f;
    private Rigidbody2D rb;

    public List<Sprite> dieSpash = new List<Sprite>();

    private int bounces = 0;
    private int bouncestoDie = 5;

    public float bounceForce = 5000f;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //move forward in x direction
        rb.AddForce(transform.right * moveSpeed, ForceMode2D.Force);
        //if force is too high, reduce it
        if (Mathf.Abs(rb.velocity.x) > 5)
        {
            rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * 5, rb.velocity.y);
        }




        if (bounces >= bouncestoDie)
        {
            Die();
        }


    }

    public void Die()
    {
        var randomSplash = Random.Range(0, dieSpash.Count);
        //pick splash from list
        var randomSplashSprite = dieSpash[randomSplash];

        //add die splash to position
        GameObject dieSplash = new GameObject();
        //add random scale
        //float randomScale = Random.Range(1f, 6f);
        // dieSplash.transform.localScale = new Vector3(randomScale, randomScale, 1);
        dieSplash.transform.position = transform.position;
        dieSplash.AddComponent<FadeInScript>();
        dieSplash.AddComponent<SpriteRenderer>().sprite = randomSplashSprite;
        //sorting order should be lower than enemy
        dieSplash.GetComponent<SpriteRenderer>().sortingOrder = -1;
        dieSplash.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.5f);
        //but higher than background
        dieSplash.GetComponent<SpriteRenderer>().sortingLayerName = "Background";

        //canvas.GetComponent<SpriteTextureGenerator>().AddColorSplash(Color.red, 5);

        //destroy the enemy
        Destroy(gameObject);
    }



    private IEnumerator fadeOut()
    {
        //disable collider and rb
        GetComponent<Collider2D>().enabled = false;
        rb.velocity = Vector2.zero;

        for (float f = 1f; f >= -0.05f; f -= 0.05f)
        {
            Color c = GetComponent<SpriteRenderer>().material.color;
            c.a = f;
            GetComponent<SpriteRenderer>().material.color = c;
            Debug.Log("fading. " + f);
            //also scale down
            transform.localScale = new Vector3(transform.localScale.x - 0.1f, transform.localScale.y - 0.1f, 1);
            //if scale is < 0 then break
            if (transform.localScale.x < 0)
            {
                break;
            }
            yield return new WaitForSeconds(0.005f);
        }

        // Call Die() after the fade out effect has completed
        AudioManager.Instance.PlayAudio(Sound.Blob);
        Die();
    }

    public void TakeDamage()
    {
        Debug.Log("taking damage");
        StartCoroutine(fadeOut());
    }


    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            bounces++;
            //bounce 
            rb.AddForce(collision.contacts[0].normal * bounceForce, ForceMode2D.Impulse);

        }

    }
}
