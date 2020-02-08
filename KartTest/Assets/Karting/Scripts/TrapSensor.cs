using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapSensor : MonoBehaviour
{
    public GameObject needle;
    public float offset;

    private void Start()
    {
        needle.transform.Translate(0, -offset, 0);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            needle.transform.Translate(0,offset,0);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            needle.transform.Translate(0, -offset, 0);
        }
    }
}
