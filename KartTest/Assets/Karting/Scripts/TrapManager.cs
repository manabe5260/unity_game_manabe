using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapManager : MonoBehaviour
{
    public Vector3 smashPosition;

    private void Update()
    {
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("isEnter");
            other.transform.Translate(smashPosition);
            other.GetComponent<SmashManager>().Smash();
        }
    }
}
