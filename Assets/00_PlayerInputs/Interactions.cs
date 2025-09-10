using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Interactions : MonoBehaviour
{
    
    private bool key1 = false; [SerializeField]
    private bool key2 = false; [SerializeField]
    
    IEnumerator = 
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start() 
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)

        {
            case "NPC1":
                Debug.Log("Tengo la llave 1");
                break;

        
            case "NPC2":
                Debug.Log("Tengo la llave 2");
                break;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
