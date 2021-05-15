using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        TimeSpan time = DateTime.Now.TimeOfDay;

    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(Vector3.zero,Vector3.right,10f*Time.deltaTime);
        transform.LookAt(Vector3.zero);
    }
}
