using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonSensor : MonoBehaviour
{
    public GameObject cannon;

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player") {
            cannon.GetComponent<CannonManager>().Fire();
        }
    }


}
