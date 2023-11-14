using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class Collectables : MonoBehaviour
{
    [SerializeField] public TMP_Text _coinTextBox;
    [SerializeField] public TMP_Text _AmmoTextBox;
    public string nameCollectable;
    public WeaponBase weapon;
    public int score;
    public int restoreHp;

    public Collectables(string name, int scoreValue, int restoreHPValue)
    {
        this.nameCollectable = name;
        this.score = scoreValue;
        this.restoreHp = restoreHPValue;
    }

    public Collectables(string name, WeaponBase _weapon)
    {
        this.weapon = _weapon;
    }
    public void UpdateScore()
    {
        ScoreManager.scoreManager.UpdateScore(score);
    }
    public void UpdateAmmo(WeaponBase weapon)
    {
        ScoreManager.scoreManager.UpdateAmmo(weapon);
    }
    public void UpdateHealth()
    {
        
    }
    /*private void Start()
    {
        _coinTextBox.text = "Coins: " + _player.GetCoinAmount();
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player") {
            Debug.Log("Collided");
            _player.AddCoin(1);
            _coinTextBox.text = "Coins: " + _player.GetCoinAmount();
            Destroy(gameObject);
        }
    }*/
}
