using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    public float aliveTime = 2f;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            if (other.gameObject.GetComponent<EnemyScript>() != null)
            {
                other.gameObject.GetComponent<EnemyScript>().TakeDamage();
            }
            else if (other.gameObject.GetComponent<BallEnemyScript>() != null)
            {
                other.gameObject.GetComponent<BallEnemyScript>().TakeDamage();

                //Destroy(gameObject);
            }
        }
    }
}
