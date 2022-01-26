using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoLauncher : MonoBehaviour
{
    public int ammoCount = 7;

    [SerializeField]
    private GameObject ammo;
    private GameObject currentBullet;

    public void spawnAmmo()
    {
        if (ammoCount <= 0)
            return;

        currentBullet = Instantiate(ammo, transform);
    }
}
