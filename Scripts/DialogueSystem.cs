using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;
using System.Linq;
using TMPro;

public class DialogueSystem: MonoBehaviour {

    public Text nameText;
    public Text dialogueText;
    public TextMeshProUGUI InteractIcon;

    public GameObject dialogueGUI;
    public Transform dialogueBoxGUI;

    public KeyCode DialogueInput = KeyCode.F;
    public string Names;

    public bool dialogueActive = false;
    public int dialogIndex = 0;

    void Start()
    {
        dialogueGUI.SetActive(false);
        dialogueBoxGUI.gameObject.SetActive(false);
    }
    public void EnterRangeOfNPC()
    {
        dialogueGUI.SetActive(true);
        if (!dialogueActive)
        {
            InteractIcon.gameObject.SetActive(true);
        }
    }

    public void NPCName()
    {
        dialogueActive = true;
        dialogueBoxGUI.gameObject.SetActive(true);
        nameText.text = Names;
        InteractIcon.gameObject.SetActive(false);
    }

    public void setText(char text)
    {
        dialogueText.text += text;
    }

    public void StopDialog()
    {
        dialogueActive = false;
        dialogueGUI.SetActive(false);
        dialogueBoxGUI.gameObject.SetActive(false);
        dialogIndex = 0;
    }
}
