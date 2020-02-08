using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FastForward : MonoBehaviour
{
    const int maxCoolTime = 200;
    const int maxCount = 150;

    private bool isFast = false;
    private int coolTime = 0;
    private int count = 0;

    public GameObject coolTimeText;
    public GameObject coolTimeSlider;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        coolTimeText.GetComponent<Text>().text = "COOLING";
        coolTimeSlider.GetComponent<Slider>().value = (float)coolTime / maxCoolTime;

        if (coolTime > maxCoolTime)
        {
            coolTimeText.GetComponent<Text>().text = "PRESS F";
            coolTimeSlider.GetComponent<Slider>().value = 1;

            if (Input.GetKeyDown(KeyCode.F))
            {
                if (Time.timeScale == 1.0f)
                {
                    Time.timeScale = 1.5f;
                    Time.fixedDeltaTime = 0.03f;
                    isFast = true;
                }
                else
                {
                    count = 0;
                    Time.timeScale = 1.0f;
                    Time.fixedDeltaTime = 0.02f;
                    isFast = false;
                }
            }
        }

        coolTime++;

        if (isFast == true)
        {
            count += 1;
            coolTimeText.GetComponent<Text>().text = "FAST";
            coolTimeSlider.GetComponent<Slider>().value = (float)(maxCount - count) / maxCount;
            coolTime = 0;
        }

        if (count > maxCount)
        {
            count = 0;
            Time.timeScale = 1.0f;
            Time.fixedDeltaTime = 0.02f;
            isFast = false;
        }
    }
}