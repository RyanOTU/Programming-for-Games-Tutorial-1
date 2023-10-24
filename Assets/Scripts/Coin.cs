using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    Collectables coin;
    [SerializeField] private int coinValue = 1;

    private void Awake()
    {
        coin = new Collectables("coin", coinValue, 0);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            coin.UpdateScore();
            Destroy(gameObject);
        }
    }
}
