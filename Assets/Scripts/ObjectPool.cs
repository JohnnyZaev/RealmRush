using System.Collections;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float spawnTime = 1f;

    private WaitForSeconds _waitTime;
    
    private void Start()
    {
        StartCoroutine(SpawnEnemy());
        _waitTime = new WaitForSeconds(spawnTime);
    }

    private IEnumerator SpawnEnemy()
    {
        while (true)
        {
            Instantiate(enemyPrefab, transform);
            yield return _waitTime;
        }
    }
}
