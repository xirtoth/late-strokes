using System.Collections;
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
    public GameObject canvas;

    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        // every 5 seconds spawn random enemy
        if (Time.frameCount % 300 == 0)
        {
            int enemyType = Random.Range(0, 3);
            //random spawn type
            int spawnType = Random.Range(0, 3);
            StartCoroutine(SpawnEnemy((SpawnType)spawnType, enemyType));
        }
    }

    public IEnumerator SpawnEnemy(SpawnType spawnType, int enemyType)
    {
        GameObject enemy;
        Vector3 spawnPosition = new Vector3(Random.Range(-1, 1), Random.Range(-1, 1), 0);
        switch (spawnType)
        {
            case SpawnType.Single:
                enemy = InstantiateEnemy(enemyType, spawnPosition);
                break;

            case SpawnType.MultipleVertical:
                for (int i = 0; i < 5; i++)
                {
                    Vector3 position = spawnPosition + new Vector3(i, 0, 0);
                    enemy = InstantiateEnemy(enemyType, position);
                    yield return new WaitForSeconds(0.02f);
                }
                break;

            case SpawnType.Circle:
                for (int i = 0; i < 5; i++)
                {
                    float angle = i * Mathf.PI * 2 / 5;
                    Vector3 position = spawnPosition + new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0);
                    enemy = InstantiateEnemy(enemyType, position);
                    yield return new WaitForSeconds(0.02f);
                }
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