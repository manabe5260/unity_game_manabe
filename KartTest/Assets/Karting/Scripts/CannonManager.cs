using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonManager : MonoBehaviour
{
    public GameObject player;
    public GameObject[] frag;
    public GameObject cannon;

    const int maxFragLife = 300;
    public int fragLife = 301;
    public float fragSpeed;
    public float random;

    public float land = (float)0.75;

    public Vector3 smashPosition;
    public Vector3 smashRotation;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.LookAt(new Vector3(player.transform.position.x, 0, player.transform.position.z));

        if (fragLife > maxFragLife)
        {
            for (int i = 0; i < frag.Length; i++)
            {
                frag[i].transform.position = cannon.transform.position;

            }
        }
        else
        {
            fragLife++;
        }
    }

    public void Fire()
    {
        if (fragLife > maxFragLife)
        {
            Vector3 playerPosition = new Vector3(player.transform.position.x, land, player.transform.position.z);

            for (int i = 0; i < frag.Length; i++)
            {
                frag[i].transform.position = player.transform.position;
                frag[i].GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-random,random), 1, Random.Range(-random,random)) * fragSpeed, ForceMode.VelocityChange);
                
            }

            fragLife = 0;

            if (player.transform.position.y < land)
            {
                Debug.Log("jump");
                player.transform.Translate(smashPosition);
                player.transform.Rotate(smashRotation);
            }
        }
    }

}
