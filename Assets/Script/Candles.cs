using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candles : MonoBehaviour
{
    public GameObject StarsEffect;
    public AudioClip pickSound;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Luna"))
        {
            GameManager.Instance.CandleNum++;          
            Instantiate(StarsEffect, transform.position, Quaternion.identity);
            if(GameManager.Instance.CandleNum >= 5)
            {
                GameManager.Instance.SetContentIndex();
            }
            GameManager.Instance.PlaySound(pickSound);
            Destroy(gameObject);
        }
    }
}