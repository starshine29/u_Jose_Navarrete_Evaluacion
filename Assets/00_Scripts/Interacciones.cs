using System;
using UnityEngine;

public class Interacciones : MonoBehaviour


{
    private bool key1 = false; [SerializeField]
    private bool key2 = false; [SerializeField]
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "NPC1":
                Debug.Log("Tengo la primera llave");
                break;
            
            case "NPC2":
                Debug.Log("Tengo la segunda llave");
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
