using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LapseManager : MonoBehaviour
{
    const int year = 360;
    const int season = 90;
    private int day = 0;

    public Color[] skyColor;
    private int countSky;

    public GameObject clouds;
    public float cloudSpeed;

    public GameObject light;
    public float lightSpeed;

    public GameObject[] building;
    public int paseBuilding;
    private int upPase;
    private int numberBuilding = 0;
    const float buildingHeight = 50;
    private float upHeight;
    public int buildCount;
    private int count = 0;

    public GameObject[] rock;
    public int paseRock;
    private int numberRock = 0;

    public GameObject[] ballTree;
    public GameObject[] cubeTree;
    public Color[] colorBallTree;
    public Color[] colorCubeTree;
    const int paseTree = 5;
    private int countBallColor = 0;
    private int countCubeColor = 0;
    private int paseBallColor;
    private int paseCubeColor;

    // Start is called before the first frame update
    void Start()
    {
        countSky = skyColor.Length / 4;

        foreach (GameObject building in building)
        {
            building.transform.Translate(0, -buildingHeight, 0);
        }

        upHeight = buildingHeight / buildCount;
        upPase = paseBuilding / buildCount;

        paseBallColor = year / colorBallTree.Length;
        paseCubeColor = year / colorCubeTree.Length;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.L))
        {
            day++;
            Move_Building();
            Move_Cloud();
            Move_Light();
            Move_Rock();
            Move_Sky();
            Move_Tree();
        }
    }

    void Move_Sky()
    {
        int i = (day % year) / season;
        RenderSettings.ambientGroundColor = skyColor[Random.Range(i * countSky, i * countSky + (countSky - 1))];
        RenderSettings.ambientSkyColor = skyColor[Random.Range(i * countSky, i * countSky + (countSky - 1))];
    }

    void Move_Cloud()
    {
        clouds.transform.Rotate(0, cloudSpeed, 0);
    }

    void Move_Light()
    {
        light.transform.Rotate(0, lightSpeed, 0);
    }

    void Move_Building()
    {
        if ((day % year) % upPase == 0 && numberBuilding < building.Length)
        {
            building[numberBuilding].transform.Translate(0, upHeight, 0);

            count++;
            if (count >= buildCount)
            {
                building[numberBuilding].transform.Find("Cube").gameObject.SetActive(false);
                numberBuilding++;
                count = 0;
            }
        }
    }

    void Move_Rock()
    {
        if ((day % year) % paseRock == 0 && numberRock < rock.Length)
        {
            rock[numberRock].transform.Translate(0, -100, 0);

            numberRock++;
        }
    }

    void Move_Tree()
    {
        if (day % paseTree == 0)
        {
            foreach (GameObject tree in ballTree)
            {
                tree.transform.Rotate(0, Random.Range(10, 350), 0);

                if (day % paseBallColor == 0)
                {
                    tree.GetComponent<Renderer>().material.color = colorBallTree[countBallColor];
                    countBallColor++;
                    if (countBallColor >= colorBallTree.Length)
                    {
                        countBallColor = 0;
                    }
                }
            }

            foreach (GameObject tree in cubeTree)
            {
                tree.transform.Rotate(0, Random.Range(10, 350), 0);

                if (day % paseCubeColor == 0)
                {
                    tree.GetComponent<Renderer>().material.color = colorCubeTree[countCubeColor];
                    countCubeColor++;
                    if (countCubeColor >= colorCubeTree.Length)
                    {
                        countCubeColor = 0;
                    }
                }
            }
        }
    }
}
