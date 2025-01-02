using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ShootBehaviour : MonoBehaviour
{
    const string ParamShoot = "Shoot";

    private Animator _animator;
    private Transform _spawner;
    private float _currentCooldown;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _spawner = transform.GetChild(0).GetComponent<Transform>();
        _currentCooldown = 0f;
    }

    public void HandleCooldown(float deltaTime)
    {
        _currentCooldown -= deltaTime;
    }

    public bool CanShoot()
    {
        return _currentCooldown <= 0f;
    }

    public void Shoot(Turret data, Vector3 targetPosition)
    {
        if (!CanShoot()) return;

        _animator.SetTrigger(ParamShoot);
        _currentCooldown = data.RechargeCooldown;

        GameObject bullet = Instantiate(data.BulletPrefab, _spawner.position, Quaternion.identity);

        TurretBullet turretBullet = bullet.GetComponent<TurretBullet>();
        turretBullet.Speed = data.AttackSpeed;
        turretBullet.Direction = targetPosition - _spawner.position;
    }
}
