using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewindManager : MonoBehaviour
{
    const int maxObject = 6;
    const int maxRewindFrame = 150;
    const int maxCoolTime = 250;

    public GameObject[] gameObject = new GameObject[maxObject];
    public TransRecord[] transRecord = new TransRecord[maxObject];

    private bool isRecord = true;  // 1:cooling or cooled, 0:using 
    private int rewindFrame = 0;
    private int coolTime = 0;

    public GameObject coolTimeText;
    public GameObject coolTimeSlider;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < transRecord.Length; i++)
            transRecord[i] = new TransRecord();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        TransInfo temp = new TransInfo(new Vector3(), new Quaternion());

        if (isRecord == true)
        {
            for (int i = 0; i < transRecord.Length; i++)
                temp = transRecord[i].Move(isRecord, gameObject[i].transform.position, gameObject[i].transform.rotation);

            coolTimeSlider.GetComponent<Slider>().value = (float)coolTime / maxCoolTime;

            coolTimeText.GetComponent<Text>().text = "COOLING";
            if (coolTime > maxCoolTime)
            {
                coolTimeText.GetComponent<Text>().text = "PRESS F";
                coolTimeSlider.GetComponent<Slider>().value = 1;

                if (Input.GetKeyDown(KeyCode.F))
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

            coolTimeText.GetComponent<Text>().text = "REWIND";
            coolTimeSlider.GetComponent<Slider>().value = (float)(maxRewindFrame - rewindFrame)/maxRewindFrame;
        }

    }

    public bool isRecording()
    {
        return isRecord;
    }
}

[System.Serializable]
public class TransRecord
{
    const int maxFrame = 250;  //180+a

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
        
        for (int i = 0; i < maxFrame - 1; i++)
        {
            transInfos[i].position = transInfos[i + 1].position;
            transInfos[i].rotation = transInfos[i + 1].rotation;
        }
        transInfos[maxFrame - 1].position = new Vector3();
        transInfos[maxFrame - 1].rotation = new Quaternion();

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