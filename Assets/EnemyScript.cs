using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    private Rigidbody2D rb;
    public Vector2 randomDirection;

    public List<Sprite> dieSpash = new List<Sprite>();

    public GameObject canvas;

    public GameObject player;

    public float moveSpeed = 5f;

    public float shakeSpeed = 16f;
    public float shakeAmount = 5f;

    public float health = 100f;
    public float maxHelath = 100f;

    private float randomScale;
    public float hopForce = 5f; // The force of the hop
    public float hopInterval = 1f; // The time between hops
    private float nextHopTime;

    public float slowingFactor = 0.95f;
    public float stoppingDistance = 1f;
    public float amplitude = 40f; // The magnitude of oscillation
    public float frequency = 0.5f; // The frequency of oscillation
    public Vector2 initialPosition; // The initial position of the enemy


    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        //randomDirection = Random.insideUnitCircle.normalized;
        // InvokeRepeating("ChangeDirection", 2f, 5f);
        //after 10 seconds call die
        //Invoke("Die", 10f);

        canvas = GameObject.FindGameObjectWithTag("Canvas");
        Debug.Log(canvas.name);
        //set this to children of canvas
        //find gameobject with tag canvas
        //set scale to random between 1 and 5   
        randomScale = Random.Range(1f, 2.5f);
        transform.localScale = new Vector3(randomScale, randomScale, 1);
        moveSpeed = Random.Range(1f, 4f);
        initialPosition = transform.position;

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
        // Determine the direction towards the player
        Vector2 direction = (player.transform.position - transform.position).normalized;

        // Calculate the sine offset for vertical oscillation
        float offset = Mathf.Sin(Time.time * frequency) * amplitude;

        // Apply the direction towards the player with vertical oscillation
        Vector2 movement = (direction + new Vector2(0, offset)).normalized * moveSpeed;

        // Apply the movement to the enemy
        rb.velocity = movement;
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
}