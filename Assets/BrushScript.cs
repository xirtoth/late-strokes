using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental;
using UnityEngine;

public class BrushScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            if (col.gameObject.GetComponent<EnemyScript>() != null)
            {
                col.gameObject.GetComponent<EnemyScript>().TakeDamage();
            }
            else if (col.gameObject.GetComponent<BallEnemyScript>() != null)
            {
                col.gameObject.GetComponent<BallEnemyScript>().TakeDamage();
            }
        }
    }
}
