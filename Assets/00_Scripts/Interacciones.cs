using System;
using UnityEngine;

public class Interacciones : MonoBehaviour


{
    [SerializeField] private bool key1 = false; 
    [SerializeField] private bool key2 = false; 
    
    
    

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

   
}
