using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;

public class SceneOpening : MonoBehaviour
{
    public GameObject highInsideCamera;
    public GameObject highOutsideCamera;
    public GameObject player;
    public GameObject playerCamera;
    public GameObject hands;
    public GameObject peopleInTrainCamera;
    public GameObject cockpitCamera;
    public GameObject endCamera;
    public List<AudioCutsceneDTO> sources;
    public FirstPersonController personController;

    // Start is called before the first frame update
    void Start()
    {
        //personController = GameObject.Find("ActivePlayer");
        StartCoroutine(StartFirstScene(FindAudioClip("Opening")));
        StartCoroutine(PlaySoundBackgroundSound(FindAudioClip("Background")));
    }
    private IEnumerator PlaySoundBackgroundSound(AudioSource[] background)
    {
        yield return new WaitForSeconds(6);
        background[0].Play();
        yield return new WaitForSeconds(30);
        StartCoroutine(VoiceCall(FindAudioClip("Oproep eerste stop")));
    }

    public IEnumerator StartFirstScene(AudioSource[] opening)
    {
        player.SetActive(false);
        opening[0].Play();
        //opening[0].clip.length+1
        yield return new WaitForSeconds(opening[0].clip.length + 1);
        StartCoroutine(TrainPreviewScene(FindAudioClip("Camera preview trein")));
    }

    public IEnumerator TrainPreviewScene(AudioSource[] aclips)
    {
        yield return new WaitForSeconds(1);
        highOutsideCamera.SetActive(true);
        yield return new WaitForSeconds(2.3f);
        
        highOutsideCamera.SetActive(false);
        highInsideCamera.SetActive(true);
        yield return new WaitForSeconds(10);
        highInsideCamera.SetActive(false);

        aclips[0].Play();
        peopleInTrainCamera.SetActive(true);
        yield return new WaitForSeconds(10);
        peopleInTrainCamera.SetActive(false);
        player.SetActive(true);

        yield return new WaitForSeconds(60);
        StartCoroutine(VoiceCall(FindAudioClip("Oproep tas verloren")));
    }

    public void StartReadCutscene()
    {
        StartCoroutine(ReadCutscene(FindAudioClip("Gescheurd papier")));
    }
    public IEnumerator ReadCutscene(AudioSource[] aclips)
    {
        personController.GetComponent<FirstPersonController>().mouseLookEnabled = false;
        hands.SetActive(true);
        aclips[0].Play();
        yield return new WaitForSeconds(aclips[0].clip.length + 1f);

        aclips[1].Play();
        yield return new WaitForSeconds(aclips[1].clip.length);
        hands.SetActive(false);
        personController.GetComponent<FirstPersonController>().mouseLookEnabled = true;
    }
    public IEnumerator VoiceCall(AudioSource[] aclips)
    {
        aclips[0].Play();
        yield return new WaitForSeconds(5);
    }
    public void StartVisitMachinistCutscene()
    {
        StartCoroutine(VisitMachinistCutscene(FindAudioClip("Enter cockpit")));
    }

    private IEnumerator VisitMachinistCutscene(AudioSource[] aclips)
    {
        yield return new WaitForSeconds(2f);
        aclips[0].Play();
        yield return new WaitForSeconds(0.5f);
        //Set position and play knocking sound
        personController.mouseLookEnabled = false;
        personController.transform.position = new Vector3(-27.71f, 0.407f, 0.294f);
        yield return new WaitForSeconds(aclips[0].clip.length);

        //Play rest
        GameObject machinist = GameObject.Find("Machinist");
        //machinist.SetActive(false);
        personController.transform.position = new Vector3(-27.71f, 0.407f, 0.294f);
        playerCamera.transform.position = new Vector3(0, 0.938f, 3.48f);
        playerCamera.transform.rotation = new Quaternion(0, 0, 0, 0);

        //Show and set object actions
        ObjectChoiceActions choiceActions = new ObjectChoiceActions();
        List<ActionChoice> actions = new List<ActionChoice>();
        List<ButtonInput> buttonInput = new List<ButtonInput>();

        UnityEvent addListeners(UnityEvent btnEvent)
        {
            btnEvent.AddListener(() => personController.transform.position = new Vector3(-25.71f, 0.407f, 0.294f));
            btnEvent.AddListener(() => StartCoroutine(endLevel()));
            btnEvent.AddListener(() => choiceActions.stop());
            btnEvent.AddListener(() => personController.mouseLookEnabled = true);
            return btnEvent;
        }
        UnityEvent buttonEvent = new UnityEvent();
        buttonEvent = addListeners(buttonEvent);
        buttonEvent.AddListener(() => FindAudioClip("Exit cockpit")[0].Play());

        UnityEvent button2Event = new UnityEvent();
        button2Event.AddListener(() => GameObject.Find("Humans").SetActive(false));
        button2Event = addListeners(button2Event);

        buttonInput.Add(new ButtonInput("Het strand", buttonEvent));
        buttonInput.Add(new ButtonInput("De stad", button2Event));
        actions.Add(new ActionChoice("Where do we go?", buttonInput ));
        choiceActions.setIndex(0);
        choiceActions.actionChoicesInput = actions;
        choiceActions.Trigger();
    }

    public AudioSource[] FindAudioClip(string name)
    {
        foreach(AudioCutsceneDTO source in sources)
        {
            if (source.audioName == name) return source.aclips;
        }
        return new AudioSource[1];
    }
    void setActive(GameObject[] objs)
    {
        foreach(GameObject obj in objs)
        {
            obj.SetActive(!obj.activeSelf);        
        }
    }
    IEnumerator endLevel()
    {
        yield return new WaitForSeconds(30);
        FindAudioClip("Aankomst")[0].Play();
        yield return new WaitForSeconds(FindAudioClip("Aankomst")[0].clip.length);
        player.SetActive(false);
        endCamera.SetActive(true);
    }
}
