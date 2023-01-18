using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RecoredDurationTime : MonoBehaviour
{
    public bool hasStudyStarts = false;

    private bool hasRecordStartTime = false;
    private string recordTimeFileName = "recordTimeDuration.txt";


    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (hasStudyStarts)
        {
            if (!hasRecordStartTime)
            {
                //var curTime = Time.time.ToString();
                var curTime = System.DateTime.Now;
                hasRecordStartTime = true;
                string path = Application.dataPath + "/" + recordTimeFileName;
                if (!System.IO.File.Exists(path))
                    System.IO.File.WriteAllText(path, "start_time:" + curTime + "\n");
                else
                    System.IO.File.AppendAllText(path, "start_time:" + curTime + "\n");
            }
        }
    }

    void OnApplicationQuit()
    {
        // var curTime = Time.time.ToString();
        var curTime = System.DateTime.Now;
        string path = Application.dataPath + "/" + recordTimeFileName;
        if (!System.IO.File.Exists(path))
            System.IO.File.WriteAllText(path, " stop_time:" + curTime + "\n");
        else
            System.IO.File.AppendAllText(path, " stop_time:" + curTime + "\n");
    }
}
