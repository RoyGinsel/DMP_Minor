using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class PuzzleScript : MonoBehaviour
{
    [SerializeField] PuzzlePieceScript[] pictureObjects;
    [SerializeField] Transform[] pictures;

    //[SerializeField] GameObject clearedText;
    public bool completed;
    private ObjectChoiceActions objectActionChoice;
    private int index;

    public void Start()
    {
        objectActionChoice = this.GetComponentInParent<ObjectChoiceActions>();
        pictureObjects = GetComponentsInChildren<PuzzlePieceScript>();
        pictures = new Transform[pictureObjects.Length];
        for (int i = 0; i < pictureObjects.Length; i++)
        {
            pictures[i] = pictureObjects[i].transform;
        }
    }
    public void UpdateonClick()
    {
        foreach(Transform picture in pictures)
        {
            if (Math.Round(picture.rotation.z, 1) != 0)
            {
                index++;
                completed = false;
                break;
            }
            else if (index >= pictures.Length) completed = true;
        }
        if (completed)
        {
            Debug.Log("Completed");
            index = 1;
            objectActionChoice.setIndex(index);
        }
    }
}
