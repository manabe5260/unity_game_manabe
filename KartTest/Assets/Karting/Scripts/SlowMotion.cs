using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SlowMotion : MonoBehaviour
{
    const int maxCoolTime = 200;
    const int maxCount = 150;

    private bool isSlow = false;
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
            coolTimeText.GetComponent<Text>().text = "PRESS T";
            coolTimeSlider.GetComponent<Slider>().value = 1;

            if (Input.GetKeyDown(KeyCode.T))
            {
                if (Time.timeScale == 1.0f)
                {
                    Time.timeScale = 0.3f;
                    Time.fixedDeltaTime = 0.006f;
                    isSlow = true;
                }
                else
                {
                    count = 0;
                    Time.timeScale = 1.0f;
                    Time.fixedDeltaTime = 0.02f;
                    isSlow = false;
                }
            }
        }

        coolTime++;

        if (isSlow == true)
        {
            count += 1;
            coolTimeText.GetComponent<Text>().text = "SLOW";
            coolTimeSlider.GetComponent<Slider>().value = (float)(maxCount - count) / maxCount;
            coolTime = 0;
        }

        if (count > maxCount)
        {
            count = 0;
            Time.timeScale = 1.0f;
            Time.fixedDeltaTime = 0.02f;
            isSlow = false;
        }
    }
}