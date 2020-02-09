using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmashManager : MonoBehaviour
{
    public float maxCount;
    public float maxRotate;
    public float maxHeight;
    private bool isSmash;
    private float count = 0;
    private float rotate;
    private float height;

    // Start is called before the first frame update
    void Start()
    {
        rotate = maxRotate / maxCount;
        height = maxHeight / maxCount;
    }

    // Update is called once per frame
    void Update()
    {
        if (isSmash)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + height, transform.position.z);
            transform.Rotate(0, 0, rotate);

            count++;
            if (count > maxCount)
            {
                transform.rotation = new Quaternion(0, transform.rotation.y, 0, 0);
                isSmash = false;
            }
        }

    }

    public void Smash()
    {
        isSmash = true;
        count = 0;
    }
}
