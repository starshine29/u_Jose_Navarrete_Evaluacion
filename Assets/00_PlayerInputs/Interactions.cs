using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interactions : MonoBehaviour
{

    [Header("Llaves")]
    [Tooltip("Estas son las llaves")]
    [SerializeField] private bool key1 = false;
    [SerializeField] private bool key2 = false;
    
    
    //Se declaran las variables como booleanas para poder activarlas (pasar de false a true), y asi entrar a la puerta que las necesita



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        
        
    }


    private void OnTriggerEnter(Collider other) //El jugador entra a un objeto con trigger, y se activan los casos
    {
        switch (other.tag)

        {
            case "NPC1":
                Debug.Log("Tengo la llave 1"); //Si toca al NPC1, obtienes la llave, y la variable pasa a ser true
                key1 = true;
                break;


            case "NPC2":
                Debug.Log("Tengo la llave 2");
                key2 = true;
                break;

            case "Door":
                if (key1 && key2) //Si tienes las 2 llaves, puedes abrir la puerta
                {
                    Debug.Log("Abri la puerta");
                    Animator animator = other.gameObject.GetComponent<Animator>(Pivote);
                }

                else //Si no tienes las llaves, no puedes abrir la puerta
                {
                    Debug.Log("Necesito las llaves para entrar");
                }
                break;


        }

    }


private void OnTriggerExit(Collider other)
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

