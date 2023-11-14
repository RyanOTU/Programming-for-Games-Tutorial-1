using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public abstract class WeaponBase : MonoBehaviour
{

    [Header("Weapon Base Stats")]
    [SerializeField]
    protected float timeBetweenAttacks;

    [SerializeField] protected float chargeUpTime;
    [SerializeField, Range(0, 1)] protected float minChargePercent;
    [SerializeField] private bool isFullyAuto;
    public TextMeshProUGUI weaponUI;
    public int maxAmmo = 0;
    public int ammoAmount = 0;

    private Coroutine _currentFireTimer;
    private bool _isOnCooldown;
    private float _currentChargeTime;
    protected float percent;

    private WaitForSeconds _coolDownWait;
    private WaitUntil _coolDownEnforce;

    private void Start()
    {
        _coolDownWait = new WaitForSeconds(timeBetweenAttacks);
        _coolDownEnforce = new WaitUntil(() => !_isOnCooldown);
    }

    public void StartShooting()
    {
        _currentFireTimer = StartCoroutine(ReFireTimer());
    }

    public void StopShooting()
    {
        StopCoroutine(_currentFireTimer);

        percent = _currentChargeTime / chargeUpTime;
        if (percent != 0) TryAttack(percent);

    }

    private IEnumerator CooldownTimer()
    {
        _isOnCooldown = true;
        yield return _coolDownWait;
        _isOnCooldown = false;
    }

    private IEnumerator ReFireTimer()
    {
        print("Waiting for cooldown");
        yield return _coolDownEnforce;
        print("Post cooldown");

        while (_currentChargeTime < chargeUpTime)
        {
            _currentChargeTime += Time.deltaTime;
            yield return null;
        }

        TryAttack(1);
        yield return null;
    }

    private void TryAttack(float percent)
    {
        _currentChargeTime = 0;
        if (!CanAttack(percent)) return;

        Attack(percent);
        ScoreManager.scoreManager.UpdateAmmo(this);
        StartCoroutine(CooldownTimer());

        if (isFullyAuto && percent >= 1) _currentFireTimer = StartCoroutine(ReFireTimer()); // Auto refire

    }

    protected virtual bool CanAttack(float percent)
    {

        Vector3 math = 50 * Time.deltaTime * Vector3.one;

        return !_isOnCooldown && percent >= minChargePercent;
    }

    protected abstract void Attack(float percent);

    public void AddAmmo(int amount)
    {
        if ((ammoAmount + amount) >= maxAmmo) ammoAmount = maxAmmo;
        else ammoAmount += amount;
    }

    public int GetMaxAmmo()
    {
        return maxAmmo;
    }
    public int GetAmmo()
    {
        return ammoAmount;
    }
}
