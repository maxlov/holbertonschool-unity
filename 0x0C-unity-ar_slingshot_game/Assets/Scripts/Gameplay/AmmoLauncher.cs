using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoLauncher : MonoBehaviour
{
    public GameManager gameManager;
    private TargetManager targetManager;

    public int ammocap = 7;
    private int _ammoCount;
    public int ammoCount
    { 
        get { return _ammoCount; }
        set { AmmoUpdate(value); }
    }

    public bool outOfAmmo = false;

    public GameObject ammoIconHolder;
    private List<GameObject> ammoUIList = new List<GameObject>();
    public GameObject ammoIconPrefab;

    [SerializeField]
    private GameObject ammo;
    private GameObject currentBullet;

    private void Start()
    {
        _ammoCount = 0;
        AmmoUpdate(ammocap);
        targetManager = gameManager.targetManager;
    }

    private void AmmoUpdate(int value)
    {
        if (value > _ammoCount)
        {
            for (int i = 0; i < value - _ammoCount; i++)
                ammoUIList.Add(Instantiate(ammoIconPrefab, ammoIconHolder.transform));
            _ammoCount = value;
            if (_ammoCount > 0)
                outOfAmmo = false;
        }
        else if (value < _ammoCount)
        {
            for (int i = 0; i < _ammoCount - value; i++)
            {
                GameObject temp = ammoUIList[0];
                ammoUIList.RemoveAt(0);
                Destroy(temp);
            }
            _ammoCount = value;
            if (_ammoCount <= 0)
                outOfAmmo = true;
        }
    }

    public void SpawnAmmo()
    {
        if (ammoCount <= 0)
            return;

        GameObject tempBullet = currentBullet;
        Destroy(tempBullet, 1f);
        currentBullet = Instantiate(ammo, transform);
    }

    public void AmmoHitTarget(GameObject target)
    {
        targetManager.OnTargetHit(target);
        gameManager.scoreManager.ScoreUpdate(10);
    }
}
