using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class ObjectActionSystem : MonoBehaviour
{
    public TextMeshProUGUI interactText;
    public GameObject objectActionsGUI;
    public Transform objectActionsBoxGUI;
    public List<ActionChoice> actionChoicesInput;
    private List<ActionChoiceOutput> actionsOutput = new List<ActionChoiceOutput>();
    public FirstPersonController playerController;

    public Vector3 spawnPoint = Vector3.zero;
    public GameObject player;
    public GameObject buttonPrefab;
    public ActionChoiceOutput currentAction;
    public bool objectActionsActive = false;
    public int objectActionsChoiceIndex = 0;
    void Start()
    {
        objectActionsGUI.SetActive(false);
        objectActionsBoxGUI.gameObject.SetActive(false);
    }

    public void ObjectChoiceTriggered()
    {
        Debug.Log("Start");
        objectActionsGUI.SetActive(true);
        this.transform.position = spawnPoint;
        transform.LookAt(player.transform);
        StartInteraction();
        Debug.Log("Start2");
    }

    public void StartInteraction()
    {
        objectActionsActive = true;
        doCursersettings(objectActionsActive);
        setButtonPostions(currentAction.buttonWithAction.Count);
        objectActionsBoxGUI.gameObject.SetActive(true);
    }
    public void StopActionChoice()
    {
        objectActionsActive = false;
        doCursersettings(objectActionsActive);
        objectActionsGUI.SetActive(false);
        objectActionsBoxGUI.gameObject.SetActive(false);

        foreach (Button b in objectActionsBoxGUI.GetComponentsInChildren<Button>().Where(c => c.gameObject.tag != "Skip").ToList<Button>())
        {
            Destroy(b.gameObject);
        }
    }
    //Function for desktop
    public void doCursersettings(bool active)
    {
        playerController = FindObjectOfType<FirstPersonController>();
        Cursor.visible = active;
        if (active) Cursor.lockState = CursorLockMode.Confined;
        playerController.m_MouseLook.lockCursor = !active;
        playerController.mouseLookEnabled = !active;
    }
    public void createActionChoices(List<ActionChoice> input)
    {
        actionChoicesInput = input;
        actionsOutput.Add(new ActionChoiceOutput());
        actionsOutput[objectActionsChoiceIndex].buttonWithAction = createButtons(objectActionsChoiceIndex);
        actionsOutput[objectActionsChoiceIndex].text = actionChoicesInput[objectActionsChoiceIndex].text;

        currentAction = actionsOutput[objectActionsChoiceIndex];
        interactText.text = currentAction.text;
    }
    public List<GameObject> createButtons(int indexx)
    {
        List<ButtonInput> buttonActions = actionChoicesInput[indexx].buttonInput;
        List<GameObject> buttons = new List<GameObject>();

        for (int i = 0; i < buttonActions.Count; i++)
        {
            //Instantiate button prefab and set location under canvas
            try
            {
                buttons.Add(Instantiate(buttonPrefab));
                buttons[i].transform.SetParent(objectActionsBoxGUI, false);
                
                List<Button> parentButtons = objectActionsBoxGUI.GetComponentsInChildren<Button>().Where(c => c.gameObject.tag != "Skip").ToList<Button>();
                Debug.Log(parentButtons.Count);
                Button currentButton = parentButtons[i];
                UnityEvent action = buttonActions[i].buttonAction;
                UnityAction buttonAction = () => action.Invoke();
                currentButton.onClick.AddListener(buttonAction);

                Text buttonText = buttons[i].GetComponentInChildren<Text>();
                buttonText.text = buttonActions[i].buttonText;
            } catch(Exception ex)
            {
                Debug.Log(ex);
            }
            
        }
        return buttons;
    }
    public void setButtonPostions(int buttonsAmount)
    {
        float buttonLength = calculateCanvas(buttonsAmount);

        for (int i = 0; i < buttonsAmount; i++)
        {
            Vector3 newPosition = Vector3.right * (i * buttonLength);
            currentAction.buttonWithAction[i].transform.Translate(newPosition);
        }
    }
    public float calculateCanvas(int buttonsAmount)
    {
        Transform canvas = objectActionsBoxGUI.transform;
        float canvasWidth = canvas.GetComponent<RectTransform>().sizeDelta.x;
        float canvasScale = canvas.parent.localScale.x;
        return ((canvasWidth / buttonsAmount) - 10) * canvasScale;
    }
}
