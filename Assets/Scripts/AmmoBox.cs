using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBox : MonoBehaviour
{
    Collectables ammo;
    [SerializeField] private int ammoValue = 50;

    private void Awake()
    {
        ammo = new Collectables("ammo", ammoValue, 0);
    }
    private void OnCollisionEnter(Collision collision)
    {
        WeaponBase weapon = collision.gameObject.GetComponent<Player>().GetWeapon();
        if (collision.gameObject.tag == "Player")
        {
            if (weapon)
            {
                weapon.AddAmmo(ammoValue);
                ammo.UpdateAmmo(weapon);
                Destroy(gameObject);
            }
        }
    }
}
