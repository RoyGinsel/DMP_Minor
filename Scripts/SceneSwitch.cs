﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitch : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void LoadScene(int sceneNumber)
    {
        UnityEngine.Debug.Log("Loading other scence");
        SceneManager.LoadScene(sceneNumber);
    }
}
