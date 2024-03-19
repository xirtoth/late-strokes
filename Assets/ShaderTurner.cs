using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShaderTurner : MonoBehaviour
{
    public Material defaultMaterial;
    public Material rainbowMaterial;

    private GameObject gc;
    void Start()
    {
        GetComponent<SpriteRenderer>().material = defaultMaterial;
        gc = GameObject.Find("GameController");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TurnRainbow()
    {
        Debug.Log("TurnRainbow");
        var enemies = GameObject.FindGameObjectsWithTag("Enemy");
        Debug.Log("enemies: " + enemies.Length);
        GetComponent<SpriteRenderer>().material = rainbowMaterial;
        StartCoroutine(TurnRainbowOff());
        foreach (var enemy in enemies)
        {

            //check if enemy is null
            if (enemy == null)
            {
                continue;
            }
            if (enemy.GetComponent<EnemyScript>() != null)
            {
                enemy.GetComponent<EnemyScript>().TakeDamage();
            }
            else
            {
                enemy.GetComponent<BallEnemyScript>().TakeDamage();
            }
        }
    }

    public void TurnDefault()
    {
        GetComponent<SpriteRenderer>().material = defaultMaterial;
    }

    private IEnumerator TurnRainbowOff()
    {
        yield return new WaitForSeconds(5);
        TurnDefault();
        gc.GetComponent<GameController>().canSpawnPowerUp = true;
    }
}
