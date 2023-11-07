using Palmmedia.ReportGenerator.Core.Reporting.Builders;
using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] private WeaponOS weaponStats;
    private Coroutine timerCoroutine;
    protected float currentChargeTime;
    private bool attackTimerDone = true;
    protected Rigidbody owner;

    public float contactDamage;
    public float chargeTime;
    public float minCharge;

    public WaitForSeconds coolDown;
    public float coolDownTime;

    private void OnEnable()
    {
        weaponStats.coolDown = new WaitForSeconds(coolDownTime);
    }
    protected abstract void Attack(float chargePercent);
    protected virtual bool CanAttack()
    {
        return attackTimerDone;
    }
    private void TryAttack(float percent)
    {
        if (percent < minCharge)
        {
            return;
        }
        Attack(percent);
        StartCoroutine(CoolDownTimer());
    }
    private IEnumerator CoolDownTimer()
    {
        attackTimerDone = false;
        yield return coolDown;
        attackTimerDone = true;
    }
    public void StartAttack()
    {
        timerCoroutine = StartCoroutine(HandleCharge());
    }
    public void EndAttack()
    {
        StopCoroutine(timerCoroutine);
        TryAttack(currentChargeTime/chargeTime);
    }
    private IEnumerator HandleCharge()
    {
        currentChargeTime = 0;
        print("StartCharge");
        yield return new WaitUntil(()=> attackTimerDone);
        print("CoolDownDone");

        while(currentChargeTime < chargeTime)
        {
            currentChargeTime += Time.deltaTime;
            yield return null;
        }
        print("Attack Completed");
        TryAttack(1);
        timerCoroutine = StartCoroutine(HandleCharge());
    }
}