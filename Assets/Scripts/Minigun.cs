using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minigun : WeaponBase
{
    [SerializeField] private Rigidbody proj;
    [SerializeField] private float force = 50;
    public Player player;

    private PlayerAction actions = new PlayerAction();

    protected override void Attack(float percent)
    {
        if (ammoAmount > 0)
        {
            print("My weapon attacked: " + percent);
            Rigidbody rb = Instantiate(proj, player.projectilePos.position, transform.rotation);
            rb.AddForce(transform.TransformDirection(new Vector3(0, 1, 0)) * force, ForceMode.Impulse);
            print("Blam");
            ammoAmount--;
        }
    }
    
}
