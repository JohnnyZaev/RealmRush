using System;
using System.Collections;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float spawnTime = 1f;
    [SerializeField] private int poolSize = 5;

    private WaitForSeconds _waitTime;
    private GameObject[] _pool;

    private void Awake()
    {
        PopulatePool();
    }

    private void PopulatePool()
    {
        _pool = new GameObject[poolSize];

        for (int i = 0; i < _pool.Length; i++)
        {
            _pool[i] = Instantiate(enemyPrefab, transform);
            _pool[i].SetActive(false);
        }
    }

    private void Start()
    {
        StartCoroutine(SpawnEnemy());
        _waitTime = new WaitForSeconds(spawnTime);
    }

    private IEnumerator SpawnEnemy()
    {
        while (true)
        {
            EnableObjectInPool();
            yield return _waitTime;
        }
    }

    private void EnableObjectInPool()
    {
        for (int i = 0; i < _pool.Length; i++)
        {
            if (!_pool[i].activeInHierarchy)
            {
                _pool[i].SetActive(true);
                return;
            }
        }
    }
}
