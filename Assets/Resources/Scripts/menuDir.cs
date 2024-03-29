﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class menuDir : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI TXTspeed;
    [SerializeField]
    TextMeshProUGUI TXTaccel;
    [SerializeField]
    TextMeshProUGUI TXTbrake;
    [SerializeField]
    TextMeshProUGUI TXThandle;
    [SerializeField]
    TextMeshProUGUI TXTboost;
    [SerializeField]
    TextMeshProUGUI TXTname;
    // Start is called before the first frame update
    void Start()
    {
        generateStats();
    }

    private void Awake()
    {
        generateStats();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Play()
    {
        StartCoroutine(Loader.loadScene("SampleScene"));
        
    }

    public void quit()
    {
        Application.Quit();
    }

    public void generateStats()
    {
        string[] names = { "accord", "cobalt", "civic", "altima", "camry", "sentra", "forte"};
        TXTname.text = names[Random.Range(0, names.Length)];

        int points = 25;
        Dictionary<string, float> stats = new Dictionary<string, float>(){
            { "speed", 1 },
            { "accel", 1 },
            { "turn", 1 },
            { "boost", 1 },
            { "brake", 1 },
        };
        Dictionary<string, float> statsNew = new Dictionary<string, float>();

        float failSafe = 0;
        while (points > 0)
        {
            failSafe++;
            if(failSafe>30)
            {
                break;
            }

            foreach (KeyValuePair<string, float> kvp in stats)
            {
                int temp = Random.Range(1, 10);
                if (temp > points)
                { temp = points; }
                points -= temp;

                if(statsNew.ContainsKey(kvp.Key))
                {
                    statsNew[kvp.Key] += temp;
                }
                else
                {
                    statsNew.Add(kvp.Key, 1 + temp);
                }
                
                PlayerPrefs.SetFloat(kvp.Key, statsNew[kvp.Key]);
            }
        }
        Color col = new Color(0, 0, 0);
        int colnum = Random.Range(0, 4);
        switch(colnum)
        {
            case 0:
                col = Color.black;
                break;
            case 1:
                col = Color.cyan;
                break;
            case 2:
                col = Color.red;
                break;
            case 3:
                col = Color.yellow;
                break;
        }
        Resources.Load<Material>("Materials/CarBody").color = col;

        sheetUpdate(statsNew);

    }

    void sheetUpdate(Dictionary<string,float> dic)
    {
        TXTaccel.text = dic["accel"].ToString();
        TXTspeed.text = dic["speed"].ToString();
        TXTbrake.text = dic["brake"].ToString();
        TXThandle.text = dic["turn"].ToString();
        TXTboost.text = dic["boost"].ToString();
    }
}
