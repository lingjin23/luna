using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour
{
    public float DestroyTime ;
    // Start is called before the first frame update
    void Start()
    {
        if(DestroyTime != -1){
            Destroy(gameObject,DestroyTime);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
