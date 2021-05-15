using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    [SerializeField] private Material selectedMaterial;
    [SerializeField] private Material defaultMaterial;
    public TextMeshProUGUI interactIcon;
    private string selectableTag = "IObject";
    private Transform selected;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (selected != null)
        {
            selected.GetComponent<Renderer>().material = defaultMaterial;
            interactIcon.gameObject.SetActive(false);
            //Ends endless loop of get component
            selected = null;
        }

        //Waits for raycast hit
        var raycast = Camera.main.ScreenPointToRay(Camera.main.WorldToScreenPoint(GameObject.Find("InteractPointer").transform.position));
        RaycastHit hit;
        if(Physics.Raycast(raycast, out hit))
        {
            var selection = hit.transform;
            if (selection.CompareTag(selectableTag))
            {
                var selectionRenderer = selection.GetComponent<Renderer>();
                defaultMaterial = selectionRenderer.material;
                if (selectionRenderer != null)
                {
                    interactIcon.gameObject.SetActive(true);
                    selectionRenderer.material = selectedMaterial;
                    if (Input.GetKeyDown(KeyCode.F))
                    {
                        selection.parent.GetComponent<ObjectChoiceActions>().Trigger();
                    }
                }
                selected = selection;
            }
        }
    }
}
