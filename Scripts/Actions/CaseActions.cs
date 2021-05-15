using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaseActions : MonoBehaviour
{
    private Animator anim;
    private ObjectActionSystem objectActionSystem;
    private int index = 0;

    void Start()
    {
        anim = GetComponent<Animator>();
        objectActionSystem = FindObjectOfType<ObjectActionSystem>();
    }

    public void openCase()
    {
        anim.SetTrigger("Isopening");
        anim.ResetTrigger("IsClosing");
        objectActionSystem.StopActionChoice();
        index += 1;
        objectActionSystem.objectActionsChoiceIndex = index;
    }
    public void pickupItem()
    {
        objectActionSystem.StopActionChoice();
        index += 1;
        objectActionSystem.objectActionsChoiceIndex = index;
    }

    public void closeCase()
    {
        anim.SetTrigger("IsClosing");
        anim.ResetTrigger("Isopening");
        objectActionSystem.StopActionChoice();
        objectActionSystem.objectActionsChoiceIndex = 0;
    }
}
