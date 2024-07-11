using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMainController : MonoBehaviour
{

    public Transform target;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindObjectOfType<PlayerMovement>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = target.position;
    }
}
