using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapManager : MonoBehaviour
{
    public Vector3 smashPosition;
    public Vector3 smashRotation;

    private void Update()
    {
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("isEnter");
            other.transform.Translate(smashPosition);
            other.transform.Rotate(smashRotation);
        }
    }
}
