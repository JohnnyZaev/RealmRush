using UnityEngine;

public class TargetLocator : MonoBehaviour
{
    [SerializeField] private Transform weapon;
    [SerializeField] private float radius = 15f;
    [SerializeField] private ParticleSystem projectileParticle;
    private Transform _target;

    private void Update()
    {
        FindClosestTarget();
        AimWeapon();
    }

    private void AimWeapon()
    {
        var targetDistance = Vector3.Distance(transform.position, _target.position);
        
        weapon.transform.LookAt(_target.transform);
        Attack(targetDistance < radius);
    }

    private void Attack(bool isActive)
    {
        var emission = projectileParticle.emission;
        emission.enabled = isActive;
    }

    private void FindClosestTarget()
    {
        var enemies = FindObjectsOfType<Enemy>();
        Transform closestTarget = null;
        var maxDistance = Mathf.Infinity;

        foreach (var enemy in enemies)
        {
            var targetDistance = Vector3.Distance(transform.position, enemy.transform.position);
            if (!(targetDistance < maxDistance)) continue;
            closestTarget = enemy.transform;
            maxDistance = targetDistance;
        }

        _target = closestTarget;
    }
}
