using System;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int maxHitPoints = 5;

    private Enemy _enemy;
    private int _currentHitPoints;

    private void Start()
    {
        _enemy = GetComponent<Enemy>();
    }

    private void OnEnable()
    {
        _currentHitPoints = maxHitPoints;
    }

    private void OnParticleCollision(GameObject other)
    {
        ProcessHit();
    }

    private void ProcessHit()
    {
        _currentHitPoints--;
        if (_currentHitPoints <= 0)
        {
            _enemy.RewardGold();
            gameObject.SetActive(false);
        }
    }
}
