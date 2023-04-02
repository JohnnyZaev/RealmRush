using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int maxHitPoints = 3;
    [SerializeField] private int difficultyRamp = 1;

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
        if (_currentHitPoints > 0) return;
        _enemy.RewardGold();
        maxHitPoints += difficultyRamp;
        gameObject.SetActive(false);
    }
}
