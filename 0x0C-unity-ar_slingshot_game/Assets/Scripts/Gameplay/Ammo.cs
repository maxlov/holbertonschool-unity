using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class Ammo : MonoBehaviour
{
    private Vector2 touchStartPos;

    private Rigidbody rb;

    private AmmoLauncher ammoLauncher;

    private bool isShoot = false;
    public float forceMultiplier = 3;

    private void Start()
    {
        ammoLauncher = gameObject.GetComponentInParent<AmmoLauncher>();
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                touchStartPos = touch.position;
            }

            if (touch.phase == TouchPhase.Ended)
                Shoot(touch.position - touchStartPos);
        }

        if (transform.position.y <= -50f)
        {
            ammoLauncher.spawnAmmo();
            Destroy(gameObject);
        }

    }

    void Shoot(Vector2 force)
    {
        if (isShoot)
            return;

        ammoLauncher.ammoCount -= 1;

        Vector3 shootDir = transform.forward * force.magnitude * forceMultiplier;
        transform.parent = null;
        rb.isKinematic = false;
        rb.AddForce(shootDir);
        isShoot = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("target"))
        {
            Debug.Log("Hit Target!");
        }
        ammoLauncher.spawnAmmo();
        Destroy(gameObject);
    }
}
