using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 2f);
    }

    // Update is called once per frame
    void Update()
    {

    }


    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Wall")
        {
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
