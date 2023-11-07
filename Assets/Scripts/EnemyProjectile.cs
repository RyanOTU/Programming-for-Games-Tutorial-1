using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    private float TrueDamage;
    private void OnCollisionEnter(Collision collision)
    {
        print (collision.transform.name + "," + collision.transform.root.name);
        if (collision.transform.root.TryGetComponent(out IDamagable hitTarget))
        {
            hitTarget.TakeDamage(TrueDamage);
        }
    }
}
