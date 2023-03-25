using System.Collections;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float spawnTime = 1f;
    
    private void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    private IEnumerator SpawnEnemy()
    {
        while (true)
        {
            Instantiate(enemyPrefab, transform);
            yield return new WaitForSeconds(spawnTime);
        }
    }
}
