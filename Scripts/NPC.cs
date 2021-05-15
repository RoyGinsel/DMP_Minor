using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;
using System.Threading.Tasks;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;

[System.Serializable]
public class NPC : BasicAI
{
    public enum NPCState
    {
        Begin,
        Talking,
        Walking,
        Sitting,
        Dancing
    }
    public NPCState startState;
    private NPCState npcState;
    private DialogueSystem dialogueSystem;
    public Animator _anim;
    public string Name;
    public int interactionPoints;
    public GestureDetector gestureDetector;

    [TextArea(5, 10)]
    public string[] sentence;
    //public ObjectChoiceActions choiceActions;
    private PlayerInfo playerUI;
    private GameObject isTalkingTo;
    public string[] animParams = new string[] { "IsStanding", "IsTalking", "IsWalking", "IsSitting", "IsDancing" };
    void Start()
    {
        npcState = startState;
        NPCStateActions();
        dialogueSystem = FindObjectOfType<DialogueSystem>();
        _anim = GetComponent<Animator>();
        playerUI = FindObjectOfType<PlayerInfo>();
        if (agent) agent.updateRotation = false;
    }
    private void Update()
    {
    }

    public void NPCStateActions()
    {
        //Debug.Log(name);
        //Debug.Log(npcState);
        switch (npcState)
        {
            case NPCState.Walking:
                setAnimActive(NPCState.Walking);
                StartCoroutine(Walking());
                break;
            case NPCState.Talking:
                setAnimActive(NPCState.Talking);
                dialogueSystem.Names = Name;
                dialogueSystem.dialogIndex = 0;
                FindObjectOfType<DialogueSystem>().NPCName();
                StartCoroutine(Talking());
                playerUI = FindObjectOfType<PlayerInfo>();
                playerUI.updatePoints(interactionPoints);
                interactionPoints = 0;
                break;
            case NPCState.Sitting:
                setAnimActive(NPCState.Sitting);
                StartCoroutine(StationairyAnimation());
                break;
            case NPCState.Dancing:
                setAnimActive(NPCState.Dancing);
                Dancing();
                break;
            case NPCState.Begin:
                setAnimActive(NPCState.Begin);
                break;
            default:
                stopDialog();
                DefaultFunction();
                break;
        }
    }

    public void setAnimActive(NPCState state)
    {
        int stateIndex = (int)state;
        //bug.Log(stateIndex);
        try
        {
            for (int i = 0; i < animParams.Length; i++)
            {
                if (i != stateIndex)
                {
                    if (animParams[stateIndex] != "IsTalking")
                    {
                        _anim.SetBool(animParams[i], false);
                    }
                }
            }
            //Debug.Log(animParams[stateIndex]);
            //Debug.Log(animParams.Length);
            _anim.SetBool(animParams[stateIndex], true);
        } catch (Exception ex)
        {
            Debug.Log(ex);
        }
    }

    public void OnTriggerStay(Collider other)
    {
        try
        {
            FindObjectOfType<DialogueSystem>().InteractIcon.gameObject.SetActive(false);
            var raycast = Camera.main.ScreenPointToRay(Camera.main.WorldToScreenPoint(GameObject.Find("InteractPointer").transform.position));
            RaycastHit hit;
            if (Physics.Raycast(raycast, out hit))
            {
                var selection = hit.transform;
                if (selection.name != "Train" && (other.gameObject.tag == "Player"))
                {
                    FindObjectOfType<DialogueSystem>().EnterRangeOfNPC();
                    if ((gestureDetector.currentGesture.name == "One finger point" || OVRInput.GetDown(OVRInput.Button.One)) || Input.GetKeyDown(KeyCode.F) && npcState != NPCState.Talking)
                    {
                        isTalkingTo = other.gameObject;
                        //transform.LookAt(other.gameObject.transform);
                        npcState = NPCState.Talking;
                        NPCStateActions();
                    }
                }
            }
        }catch(Exception ex)
        {
            Debug.Log(ex);
        }
    }

    public void OnTriggerExit()
    {
        npcState = startState;
        stopDialog();
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (npcState == NPCState.Walking)
        {
            if (collision.gameObject == waypoints[waypointIndex].gameObject && waypointIndex < waypoints.Length)
            {
                waypointIndex += 1;
                if (waypointIndex >= waypoints.Length)
                {
                    Debug.Log("Arrived");
                    agent.SetDestination(this.transform.position);
                    waypointIndex = 0;
                    npcState = NPCState.Begin;
                }
            }
            else if (collision.gameObject == waypoints[waypointIndex].gameObject) npcState = NPCState.Begin;
            else npcState = NPCState.Begin;
            NPCStateActions();
        }
    }

    private void Dancing()
    {
    }


    private IEnumerator StationairyAnimation()
    {
        bool done = false;
        while (!done)
        {
            if (npcState == NPCState.Begin) done = true;
            yield return null;
        }
        //NPCStateActions();
        yield return null;
    }

    //Give name and text to dialogueSystem
    private IEnumerator Talking()
    {
        bool done = false;
        while (!done)
        {
            char[] characters = sentence[dialogueSystem.dialogIndex].ToCharArray();
            
            foreach (char a in characters)
            {
                //Debug.Log(a);
                dialogueSystem.setText(a);
                Debug.Log(dialogueSystem.dialogueText.text);
                yield return dialogueSystem.dialogueText.text;
                yield return new WaitForSeconds(0.04f);
            }
            yield return waitForInput(characters);

            if (FindObjectOfType<DialogueSystem>().dialogIndex > sentence.Length - 1)
            {
                done = true; // breaks the loop
                stopDialog();
            }
        }
        npcState = startState;
        NPCStateActions();
        yield return null;
    }
    private IEnumerator waitForInput(char[] charakters)
    {
        bool done = false;
        while (!done) // essentially a "while true", but with a bool to break out naturally
        {
            if (OVRInput.GetDown(OVRInput.Button.One) || Input.GetKeyDown(KeyCode.Q))
            {
                dialogueSystem.dialogueText.text = "";
                dialogueSystem.dialogIndex += 1;
                done = true; // breaks the loop
            }
            yield return null;
        }
    }

    public void stopDialog()
    {
        FindObjectOfType<DialogueSystem>().StopDialog();
    }
    private IEnumerator Walking()
    {
        while (npcState == NPCState.Walking)
        {
            bool moving = Moving();
            if (!moving)
            {
                npcState = NPCState.Begin;
            }
            yield return null;
        }
        NPCStateActions();
    }

    private void DefaultFunction()
    {
    }
}
