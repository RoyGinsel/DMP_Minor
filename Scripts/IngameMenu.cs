using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class IngameMenu : MonoBehaviour
{
    public bool isOpen;
    public GameObject ingameMenu;
    public FirstPersonController playerController;

    // Start is called before the first frame update
    void Start()
    {
        ingameMenu.gameObject.SetActive(isOpen);
        playerController = FindObjectOfType<FirstPersonController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            isOpen = !isOpen;
            ingameMenu.gameObject.SetActive(isOpen);
            Cursor.visible = isOpen;
            if(isOpen)Cursor.lockState = CursorLockMode.Confined;
            playerController.m_MouseLook.lockCursor = !isOpen;
            //playerController.mouseLookEnabled = !isOpen;
        }
    }

    public void LoadScene(int sceneNumber)
    {
        UnityEngine.Debug.Log("Loading other scence");
        SceneManager.LoadScene(sceneNumber);
    }
}
