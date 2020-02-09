using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonManager : MonoBehaviour
{
    public GameObject player;
    public GameObject cannon;
    public GameObject missile;
    public GameObject[] frag;

    public int maxMissileLife;
    public float missileSpeed;
    public float missileCollider;
    private int missileLife;
    private bool isMisile = false;

    public int maxFragLife;
    public float fragSpeed;
    private int fragLife;

    public float land = (float)0.75;
    public Vector3 smashPosition;

    // Start is called before the first frame update
    void Start()
    {
        missileLife = maxMissileLife;
        fragLife = maxFragLife;
    }

    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.LookAt(new Vector3(player.transform.position.x, 0, player.transform.position.z));

        if (isMisile == false)
        {
            missile.transform.position = cannon.transform.position;
        }
        else
        {
            missile.transform.LookAt(new Vector3(player.transform.position.x, 0, player.transform.position.z));
            missile.transform.Translate(0, 0, missileSpeed);

            if (missile.transform.position.y < land)
            {
                isMisile = false;               
                if (Vector3.Distance(player.transform.position, missile.transform.position) < missileCollider)
                {
                    player.GetComponent<SmashManager>().Smash();
                }

                fragLife = 0;
                foreach (GameObject gameObject in frag)
                {
                    gameObject.transform.position = new Vector3(missile.transform.position.x, 0, missile.transform.position.z);
                }
            }
        }

        if (fragLife >= maxFragLife)
        {
            foreach (GameObject gameObject in frag)
            {
                gameObject.transform.position = cannon.transform.position;
            }
        }

        fragLife++;
        missileLife++;
    }

    public void Fire()
    {
        if (missileLife >= maxMissileLife)
        {
            missileLife = 0;
            isMisile = true;
        }
    }

}
