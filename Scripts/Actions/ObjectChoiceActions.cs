using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[Serializable]
public class ObjectChoiceActions : MonoBehaviour
{
    public List<ActionChoice> actionChoicesInput;
    public GestureDetector gestureDetector;
    private ObjectActionSystem objectActionSystem;
    private int index = 0;
    private bool active;
    void Start()
    {//Trigger();
    }
    public void setIndex(int number)
    {
        objectActionSystem = FindObjectOfType<ObjectActionSystem>();
        this.index = number;
        objectActionSystem.objectActionsChoiceIndex = index;
    }
    public void OnTriggerStay(Collider other)
    {
        var raycast = Camera.main.ScreenPointToRay(Camera.main.WorldToScreenPoint(GameObject.Find("InteractPointer").transform.position));
        RaycastHit hit;
        if (Physics.Raycast(raycast, out hit))
        {
            if ((other.gameObject.tag == "Player"))
            {
                if (!active && (gestureDetector.currentGesture.name == "One finger point" || OVRInput.GetDown(OVRInput.Button.One)))
                {
                    active = true;
                    transform.LookAt(other.gameObject.transform);
                    Trigger();
                }
            }
        }
    }
    public void Trigger()
    {
        objectActionSystem = FindObjectOfType<ObjectActionSystem>();
        //objectActionSystem.spawnPoint = this.gameObject.transform.position + new Vector3(0,this.gameObject.transform.localScale.y*2.5f);
        setIndex(index);
        if (!objectActionSystem.isActiveAndEnabled) objectActionSystem.gameObject.SetActive(true);
        objectActionSystem.createActionChoices(actionChoicesInput);
        objectActionSystem.ObjectChoiceTriggered();
    }

    public void OnTriggerExit(Collider other)
    {
        stop();
    }
    public void stop()
    {
        Debug.Log("stop");
        active = false;
        objectActionSystem.StopActionChoice();
    }

}
