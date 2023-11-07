using Palmmedia.ReportGenerator.Core.Reporting.Builders;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Projectile : MonoBehaviour 
{
    private float currentSpeed;
    private float currentDamage;
    private Vector3 currentDirection;
    private Rigidbody owner;
    public float baseSpeed;
    public float contactDamage;
    public float lifetime = 0;
    public void Initialize(float chargePercent, Rigidbody owner)
    {
        this.owner = owner;
        currentDirection = transform.right;
        currentSpeed = baseSpeed * chargePercent;
        currentDamage = contactDamage * chargePercent;
        GetComponent<Rigidbody>().AddForce(transform.forward * currentSpeed, ForceMode.Impulse);
    }
}
