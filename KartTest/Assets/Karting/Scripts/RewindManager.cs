using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewindManager : MonoBehaviour
{
    const int maxRewindFrame = 180;
    const int maxCoolTime = 300;

    public GameObject[] gameObject;
    public TransRecord[] transRecord;

    private bool isRecord = true;  // 1:cooling or cooled, 0:using 
    public int rewindFrame = 0;
    public int coolTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < transRecord.Length; i++)
            transRecord[i] = new TransRecord();
    }

    // Update is called once per frame
    void Update()
    {
        TransInfo temp = new TransInfo(new Vector3(), new Quaternion());

        if (isRecord == true)
        {
            for (int i = 0; i < transRecord.Length; i++)
                temp = transRecord[i].Move(isRecord, gameObject[i].transform.position, gameObject[i].transform.rotation);

            if (Input.GetKey(KeyCode.F) && coolTime > maxCoolTime)
            {
                isRecord = false;
            }

            coolTime++;
            rewindFrame = 0;
        }
        else
        {
            for (int i = 0; i < transRecord.Length; i++)
            {
                temp = transRecord[i].Move(isRecord, gameObject[i].transform.position, gameObject[i].transform.rotation);
                gameObject[i].transform.position = temp.position;
                gameObject[i].transform.rotation = temp.rotation;
            }

            if (rewindFrame > maxRewindFrame)
            {
                isRecord = true;

            }

            rewindFrame++;
            coolTime = 0;
        }
    }
}

[System.Serializable]
public class TransRecord
{
    const int maxFrame = 240;  //180+a

    private TransInfo[] transInfos = new TransInfo[maxFrame];

    public TransRecord()
    {
        for (int i = 0; i < maxFrame; i++)
            transInfos[i] = new TransInfo(new Vector3(), new Quaternion());
    }

    public TransInfo Move(bool isRecord, Vector3 _position, Quaternion _rotation)
    {
        TransInfo temp = new TransInfo(new Vector3(), new Quaternion());
        if (isRecord == true)
            Record(_position, _rotation);
        else
            temp = Rewind();

        return temp;
    }

    private void Record(Vector3 _position, Quaternion _rotation) //queでpush
    {
        for (int i = maxFrame - 1; i > 0; i--)
        {
            transInfos[i].position = transInfos[i - 1].position;
            transInfos[i].rotation = transInfos[i - 1].rotation;
        }
        transInfos[0].position = _position;
        transInfos[0].rotation = _rotation;
    }

    private TransInfo Rewind()  //stackでpop
    {
        TransInfo temp = transInfos[0];
        Debug.Log(transInfos[0].position);

        for (int i = 0; i < maxFrame - 1; i++)
        {
            transInfos[i].position = transInfos[i + 1].position;
            transInfos[i].rotation = transInfos[i + 1].rotation;
        }
        transInfos[maxFrame-1].position = new Vector3();
        transInfos[maxFrame-1].rotation = new Quaternion();

        return temp;
    }
}


public class TransInfo
{
    public Vector3 position = new Vector3();
    public Quaternion rotation = new Quaternion();

    public TransInfo(Vector3 _position, Quaternion _rotation)
    {
        position = _position;
        rotation = _rotation;
    }
}