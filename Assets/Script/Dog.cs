using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dog : MonoBehaviour
{
    private Animator animator;
    public GameObject starEffect;
    public AudioClip petSound;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void BeHappy()
    {
        animator.CrossFade("BeHappy", 0);
        GameManager.Instance.HasPetTheDog = true;
        GameManager.Instance.SetContentIndex();
        Destroy(starEffect);
        GameManager.Instance.PlaySound(petSound);
        Invoke("CanControlLuna", 1.75f);
    }

    private void CanControlLuna()
    
    {
        GameManager.Instance.CanControlLuna = true;
    }
}