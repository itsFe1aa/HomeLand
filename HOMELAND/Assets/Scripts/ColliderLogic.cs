using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderLogic : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Contains("cactus"))
        {
            transform.parent.GetComponent<PlayerMovement>().enemyList.Add(other.gameObject);

        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name.Contains("cactus"))
        {
            transform.parent.GetComponent<PlayerMovement>().enemyList.Remove(other.gameObject);

        }
    }
}
