using UnityEngine;

public class TargetLocator : MonoBehaviour
{
    [SerializeField] private Transform weapon;
    private Transform _target;
    
    private void Start()
    {
        _target = FindObjectOfType<EnemyMover>().transform;
    }

    private void Update()
    {
        AimWeapon();
    }

    private void AimWeapon()
    {
        weapon.transform.LookAt(_target.transform);
    }
}
