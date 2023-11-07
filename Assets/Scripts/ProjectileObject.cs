using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.GridLayoutGroup;

public class ProjectileObject : Weapon
{
    [SerializeField] private Projectile projectileFired;
    [SerializeField] private Transform firePoint;

    protected override void Attack(float chargePercent)
    {
        Projectile currentProjectile = Instantiate(projectileFired, firePoint.position, owner.transform.rotation);
        currentProjectile.Initialize(chargePercent, owner);
        currentProjectile.gameObject.layer = gameObject.layer;
    }
}
