using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    public GameObject turretEnemies;
    public GameObject bomberEnemies;

    private void Update()
    {
        if (AreAllEnemiesDead(turretEnemies, bomberEnemies))
        {
            OpenDoors();
        }
    }

    private void OpenDoors()
    {
        this.gameObject.SetActive(false);
    }

    private bool AreAllEnemiesDead(GameObject turrets, GameObject bombers)
    {
        return AreTurretEnemiesDead(turrets) && AreBomberEnemiesDead(bombers);
    }

    private bool AreTurretEnemiesDead(GameObject turrets)
    {
        return turrets.transform.childCount == 0;
    }

    private bool AreBomberEnemiesDead(GameObject bombers)
    {
        foreach (Transform bomber in bombers.transform)
        {
            if (bomber.gameObject.activeSelf)
            {
                return false;
            }
        }
        return true;
    }
}
