using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class ActionChoice
{
    public string text;
    public List<ButtonInput> buttonInput;
    public ActionChoice(string text, List<ButtonInput> buttonInput)
    {
        this.text = text;
        this.buttonInput = buttonInput;
    }
}

[System.Serializable]
public class ButtonInput
{
    public string buttonText;
    public UnityEvent buttonAction;
    public ButtonInput(string buttonText, UnityEvent buttonAction)
    {
        this.buttonText = buttonText;
        this.buttonAction = buttonAction;
    }
}
