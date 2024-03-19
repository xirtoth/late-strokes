using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShaderTurner : MonoBehaviour
{
    public Material defaultMaterial;
    public Material rainbowMaterial;
    void Start()
    {
        GetComponent<SpriteRenderer>().material = defaultMaterial;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TurnRainbow()
    {
        Debug.Log("TurnRainbow");
        var enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GetComponent<SpriteRenderer>().material = rainbowMaterial;
        StartCoroutine(TurnRainbowOff());
        foreach (var enemy in enemies)
        {
            enemy.GetComponent<EnemyScript>().TakeDamage();
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
    }
}
