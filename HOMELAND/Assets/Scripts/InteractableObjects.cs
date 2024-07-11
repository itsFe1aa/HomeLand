using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Interatc : MonoBehaviour
{
    public bool PlayerInRange;
    public PlayerMovement playermovement;

     void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && PlayerInRange)
        {
            Debug.Log("item in invertory");
            playermovement.RockCount++;
            Destroy(gameObject);

        }



    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerInRange = true;
            playermovement = other.GetComponent<PlayerMovement>();
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerInRange = false;
            playermovement = null;
        }



    }

   










}// class end
