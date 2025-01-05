using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    private const string PlayerTag = "Player", GameManagerObject = "GameManager";

    private Weapon _currentWeapon;
    private Weapon _lastEquippedWeapon;
    private GameObject _weaponInstance;
    private GameObject _player;
    private InventoryManager _playerItems;
    private InventoryItems _allItems;

    void Start()
    {
        InitializeComponents();
        _lastEquippedWeapon = _currentWeapon;
    }

    void Update()
    {
        FollowTheMouse();
        _currentWeapon = UpdateWeapon(_playerItems, _allItems);

        if (_currentWeapon != _lastEquippedWeapon)
        {
            EquipCurrentWeapon(_currentWeapon, ref _weaponInstance);
            _lastEquippedWeapon = _currentWeapon;
        }
    }

    private void InitializeComponents()
    {
        _player = GameObject.FindGameObjectWithTag(PlayerTag);
        _playerItems = _player.GetComponent<InventoryManager>();
        _allItems = GameObject.Find(GameManagerObject).GetComponent<InventoryItems>();
    }

    private void EquipCurrentWeapon(Weapon weapon, ref GameObject instance)
    {
        if(weapon != null)
        {
            if (instance != null)
            {
                Destroy(instance);
            }
            instance = Instantiate(weapon.WeaponPrefab, 
                                          transform.position, 
                                          SetRotationToMouse(), 
                                          this.transform);

            // Inicializamos la pila de municiones si es un rifle
            InitializePoolIfNeed(instance);
        }
    }

    private void InitializePoolIfNeed(GameObject instance)
    {
        // Si la arma es un rifle, inicializamos la pila de municiones
        if (instance.name.Contains("Rifle"))
            BulletMunition.InitializeMunitionStack();
    }

    private Quaternion SetRotationToMouse()
    {
        // Asegura que la rotación inicial sea correcta
        Vector2 mouseDirection = GetMouseDirection(_player.transform);
        float angle = Mathf.Atan2(mouseDirection.y, mouseDirection.x) * Mathf.Rad2Deg;
        return Quaternion.Euler(0, 0, angle);
    }

    private Weapon UpdateWeapon(InventoryManager equipment, InventoryItems library)
    {
        try
        {
            // Obtenemos el id del arma equipada
            string id = equipment.Weapons[0].Id;

            // Buscamos el arma en formato Item y su prefab
            GameObject weaponObject = library.GetWeaponById(id);

            return weaponObject.GetComponent<Weapon>();
        }
        catch
        {
            // _playerItems.Weapons[index] puedes ser null
            return null;
        }
    }

    private void FollowTheMouse()
    {
        Vector2 mouseDirection = GetMouseDirection(_player.transform);
        Debug.DrawRay(_player.transform.position, mouseDirection, Color.red);

        RotateAroundPlayer(mouseDirection);
    }

    // Es estatica porque lo podemos utilizar en las clases de los proyectiles y aprovehcar codigo
    public static Vector2 GetMouseDirection(Transform entity)
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return (mousePosition - (Vector2)entity.position).normalized;
    }

    private void RotateAroundPlayer(Vector2 dir)
    {
        float radius = 1f;
        transform.position = (Vector2)_player.transform.position + dir * radius;
        transform.right = dir;
    }

    public void Attack()
    {
        if (_currentWeapon != null)
            _currentWeapon.GetComponent<Weapon>().UseWeapon(_weaponInstance);
    }
}
