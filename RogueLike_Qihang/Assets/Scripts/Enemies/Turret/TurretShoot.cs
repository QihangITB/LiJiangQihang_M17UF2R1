using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TurretShoot : MonoBehaviour
{
    const string ParamShoot = "Shoot";

    [SerializeField] private Animator _animator;
    [SerializeField] private Turret _turretData;
    [SerializeField] private Transform _spawner;

    private TurretAim _turretAim;
    private float _currentCooldown;

    private void Start()
    {
        _turretAim = GetComponent<TurretAim>();
        _currentCooldown = _turretData.RechargeCooldown;
    }

    private void Update()
    {
        if (_turretAim.CanShoot && _currentCooldown <= 0f)
        {
            _animator.SetTrigger(ParamShoot);
            _currentCooldown = _turretData.RechargeCooldown;
        }
        else
        {
            _currentCooldown -= Time.deltaTime;
        }
    }

    // Tras finalizar la animacion de disparo, se ejecuta este metodo
    private void Shoot()
    {
        GameObject bullet = Instantiate(_turretData.BulletPrefab, _spawner.position, Quaternion.identity);

        TurretBullet turretBullet = bullet.GetComponent<TurretBullet>();
        turretBullet.Speed = _turretData.AttackSpeed;
        turretBullet.Direction = _turretAim.TargetPosition - _spawner.position;
    }

}
