using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzlePieceScript : MonoBehaviour
{
    public PuzzleScript puzzleScript;
    public void Start()
    {
        puzzleScript = gameObject.GetComponentInParent<PuzzleScript>();
    }
    private void OnMouseDown()
    {
        if (!puzzleScript.completed)
        {
            transform.Rotate(0f, 0f, 90f);
            puzzleScript.UpdateonClick();
        }
    }
}
