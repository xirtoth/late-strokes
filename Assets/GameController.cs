using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public List<GameObject> powerUps = new List<GameObject>();
    public GameObject canvas;
    public bool canSpawnPowerUp = true;
    private int enemiesKilled = 0;

    // Start is called before the first frame update
    void Start()
    {
        Bounds bounds = canvas.GetComponent<Renderer>().bounds;

        Vector3 min = bounds.min;
        Vector3 max = bounds.max;


    }

    // Update is called once per frame
    void Update()
    {

    }

    public void EnemyKilled()
    {
        enemiesKilled++;
        if (enemiesKilled == 10)
        {
            if (canSpawnPowerUp)
            {
                canSpawnPowerUp = false;
                SpawnPowerUp();
            }

        }
    }

    private void SpawnPowerUp()
    {
        Bounds bounds = canvas.GetComponent<Renderer>().bounds;

        Vector3 min = bounds.min;
        Vector3 max = bounds.max;

        Vector3 randomPosition = new Vector3(Random.Range(min.x, max.x), Random.Range(min.y, max.y), 0);
        Instantiate(powerUps[Random.Range(0, powerUps.Count)], randomPosition, Quaternion.identity);
        Debug.Log("instantiated at location " + randomPosition.ToString());
    }




}
