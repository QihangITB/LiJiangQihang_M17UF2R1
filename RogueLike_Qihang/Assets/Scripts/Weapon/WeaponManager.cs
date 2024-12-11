using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class WeaponManager : MonoBehaviour
{
    private const string PlayerTag = "Player";

    public WeaponSO CurrentWeapon;

    private GameObject _player;

    void Start()
    {
        InitializeComponents();
    }

    void Update()
    {
        FollowTheMouse();
    }

    private void InitializeComponents()
    {
        _player = GameObject.FindGameObjectWithTag(PlayerTag);
        EquipCurrentWeapon();
    }

    private void EquipCurrentWeapon()
    {
        GameObject weapon = Instantiate(CurrentWeapon.Weapon, _player.transform);
        weapon.transform.SetParent(this.transform);
    }

    private void FollowTheMouse()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mouseDirection = (mousePosition - (Vector2)_player.transform.position).normalized;
        Debug.DrawRay(_player.transform.position, mouseDirection, Color.red);

        RotateAroundPlayer(mouseDirection);
    }

    private void RotateAroundPlayer(Vector2 dir)
    {
        float radius = 1f;
        transform.position = (Vector2)_player.transform.position + dir * radius;
        transform.right = dir;
    }

    public void Attack()
    {
        CurrentWeapon.UseWeapon();
    }
}
