using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecoverHP : MonoBehaviour
{
    
    public AudioClip pickSound;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // LunaController lunaController = collision.GetComponent<LunaController>();
        // if(lunaController != null)
        // {
        //     if (lunaController.lunaCurrentHP < lunaController.lunaHP)
        //     {
        //         lunaController.ChangeHealth(1);
        //         Destroy(gameObject);
        //     }
        // }
        if (GameManager.Instance.lunaCurrentHP < GameManager.Instance.lunaHP)
            {
                GameManager.Instance.AddOrDecreaseHP(20f);
                GameManager.Instance.PlaySound(pickSound);
                Destroy(gameObject);
            }
    }
}
