using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponStats", menuName = "ScriptableObjects/WeaponStats", order = 1)]
public class WeaponOS : ScriptableObject
{
    [field: Header("Weapon Base Stats")]
    [field: SerializeField] public float timeBetweenAttacks;
    [field: SerializeField] public WaitForSeconds cooldown;

    public WaitForSeconds coolDown { get; set; }

    private void OnEnable()
    {
        //cooldown = new WaitForSeconds(cooldown);
    }
}
