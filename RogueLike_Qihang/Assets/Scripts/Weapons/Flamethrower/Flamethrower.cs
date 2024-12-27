using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "Flamethrower", menuName = "Weapon/Flamethrower")]
public class Flamethrower : WeaponSO
{
    public override void Use()
    {
        ParticleSystem particleSystem = GetParticleSystem();

        if (particleSystem.isPlaying)
        {
            particleSystem.Stop();
        }
        else
        {
            particleSystem.Play();
        }
    }

    private ParticleSystem GetParticleSystem()
    {
        GameObject particleSystem = WeaponPrefab.transform.GetChild(0).gameObject;
        return particleSystem.GetComponent<ParticleSystem>();
    }
}
