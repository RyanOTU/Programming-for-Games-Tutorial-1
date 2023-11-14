using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableGun : MonoBehaviour
{
    Collectables gun;
    public WeaponBase weapon;

    private void Awake()
    {
        gun = new Collectables("gun", weapon);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Player>().AddGun(weapon);
            Destroy(gameObject);
        }
    }
}
