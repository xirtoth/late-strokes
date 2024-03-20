using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public enum SpawnType
{
    Single,
    MultipleVertical,
    Circle
}

public class Spawner : MonoBehaviour
{
    public int maxEnemies = 30;
    public GameObject greenEnemy;
    public GameObject blueEnemy;
    public GameObject redEnemy;

    public GameObject redBall;
    public GameObject blueBall;
    public GameObject greenBall;

    public GameObject canvas;

    public GameObject ballEnemySpawn;

    public GameObject playArea;

    //declare 5 spawnpoints public
    [SerializeField]
    public List<GameObject> spawnPoints = new List<GameObject>();

    public float spawnInterval = 5f;

    //private int spawnCount = 0;

    private Vector3 farSpawnPosition;

    private void Start()
    {
        InvokeRepeating("SpawnAtEdges", 2.0f, 5.0f);
    }

    // Update is called once per frame
    private void Update()
    {
        // every 5 seconds spawn random enemy
        /*   if (Time.frameCount % 1000 == 0)
           {
               int enemyType = Random.Range(0, 3);
               //random spawn type
               int spawnType = Random.Range(0, 3);
               StartCoroutine(SpawnEnemy((SpawnType)spawnType, enemyType));
           } */

        /*  if (Time.time > spawnInterval)
          {
              spawnInterval += 5f - spawnCount * 0.1f;
              spawnCount++;
              if (spawnCount > 10)
              {
                  spawnCount = 10;
              }
              int enemyType = Random.Range(0, 3);
              //random spawn type
              int spawnType = Random.Range(0, 3);
              StartCoroutine(SpawnEnemy((SpawnType)spawnType, enemyType));
          }*/
    }

    public void SpawnAtEdges()
    {
        //spawn enemies at canvas edges
        //get canvas bounds
        Bounds bounds = playArea.GetComponent<SpriteRenderer>().bounds;
        Vector3 min = bounds.min;
        Vector3 max = bounds.max;
        //spawn enemies at edges
        for (int i = 0; i < 4; i++)
        {
            Vector3 position = new Vector3();
            switch (i)
            {
                case 0:
                    position = new Vector3(min.x, Random.Range(min.y, max.y), 0);
                    break;
                case 1:
                    position = new Vector3(max.x, Random.Range(min.y, max.y), 0);
                    break;
                case 2:
                    position = new Vector3(Random.Range(min.x, max.x), min.y, 0);
                    break;
                case 3:
                    position = new Vector3(Random.Range(min.x, max.x), max.y, 0);
                    break;
            }
            InstantiateEnemy(Random.Range(0, 3), position);
        }
    }
    public void SpawnBalls()
    {
        StartCoroutine(SpawnBall());
    }

    public IEnumerator SpawnBall()
    {
        for (int i = 0; i < 10; i++)
        {
            int RandomBall = Random.Range(0, 3);
            switch (RandomBall)
            {
                case 0:
                    Instantiate(redBall, ballEnemySpawn.transform.position, Quaternion.identity);
                    break;
                case 1:
                    Instantiate(blueBall, ballEnemySpawn.transform.position, Quaternion.identity);
                    break;
                case 2:
                    Instantiate(greenBall, ballEnemySpawn.transform.position, Quaternion.identity);
                    break;

            }
            yield return new WaitForSeconds(0.2f);
        }
    }

    public void SpawnEnemies()
    {
        StartCoroutine(SpawnEnemy(SpawnType.Single, Random.Range(0, 3)));
        StartCoroutine(SpawnEnemy(SpawnType.MultipleVertical, Random.Range(0, 3)));
        StartCoroutine(SpawnEnemy(SpawnType.Circle, Random.Range(0, 3)));
    }

    public IEnumerator SpawnEnemy(SpawnType spawnType, int enemyType)
    {

        //var spawnPosition = spawnPoints[Random.Range(0, spawnPoints.Count)].transform.position;
        //get farthest spawn point from player
        foreach (var spawnPoint in spawnPoints)
        {
            if (Vector3.Distance(spawnPoint.transform.position, canvas.transform.position) > Vector3.Distance(farSpawnPosition, canvas.transform.position))
            {
                farSpawnPosition = spawnPoint.transform.position;
                Debug.Log(spawnPoint.name + " is farthest");
            }
        }
        GameObject enemy;
        //Vector3 spawnPosition = new Vector3(Random.Range(-1, 1), Random.Range(-1, 1), 0);
        switch (spawnType)
        {
            case SpawnType.Single:
                enemy = InstantiateEnemy(enemyType, farSpawnPosition);
                break;

            case SpawnType.MultipleVertical:
                //make random direction vector2
                Vector2 randomDirection = new Vector2(Random.Range(-1, 1), Random.Range(-1, 1));
                for (int i = 0; i < 6; i++)
                {
                    Vector3 position = farSpawnPosition + new Vector3(i * 0.5f, 0, 0);
                    enemy = InstantiateEnemy(enemyType, position);
                    //set enemy move direction
                    enemy.GetComponent<EnemyScript>().randomDirection = randomDirection;
                    yield return new WaitForSeconds(0.4f);
                }
                break;

            case SpawnType.Circle:
                randomDirection = new Vector2(Random.Range(-1, 1), Random.Range(-1, 1));
                for (int i = 0; i < 3; i++)
                {
                    float angle = i * Mathf.PI * 2 / 5;
                    Vector3 position = farSpawnPosition + new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0);
                    enemy = InstantiateEnemy(enemyType, position);
                    enemy.GetComponent<EnemyScript>().randomDirection = randomDirection;
                    yield return new WaitForSeconds(0.4f);
                }
                Camera cam = Camera.main;
                // cam.GetComponent<CameraController>().ShakeCamera(0.5f, 0.5f);
                break;
        }
    }

    private GameObject InstantiateEnemy(int enemyType, Vector3 position)
    {
        GameObject enemy;
        switch (enemyType)
        {
            case 0:
                enemy = Instantiate(greenEnemy, position, Quaternion.identity);
                break;

            case 1:
                enemy = Instantiate(blueEnemy, position, Quaternion.identity);
                break;

            case 2:
                enemy = Instantiate(redEnemy, position, Quaternion.identity);
                break;

            default:
                enemy = Instantiate(greenEnemy, position, Quaternion.identity);
                break;
        }
        return enemy;
    }
}