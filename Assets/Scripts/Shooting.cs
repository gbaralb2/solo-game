using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine.Utility;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class Shooting : MonoBehaviour
{

    public Transform firePoint;
    public GameObject bulletPrefab;

    public float bulletForce = 20f;
    public Camera cam;
    private float threshold = .8f;
    private Transform player;

    void Start()
    {
        player = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 weaponPos = (player.position + mousePos) / 2f;
        
        weaponPos.x = Mathf.Clamp(weaponPos.x, -threshold + player.position.x, threshold + player.position.x);
        weaponPos.y = Mathf.Clamp(weaponPos.y, -(threshold + .5f) + player.position.y, threshold + player.position.y);

        firePoint.position = weaponPos;

        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 fireDir = (mousePos - firePoint.position).normalized;
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        Physics2D.IgnoreCollision(bullet.GetComponent<Collider2D>(), GetComponent<Collider2D>());

        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = new Vector3(fireDir.x, fireDir.y).normalized * bulletForce;
    }
}
