using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    /*public GameObject hands;
    public GameObject ObjectSelected;
    public GameObject InventoryObject;
    public Inventory inventory;
    int number;*/

    // Start is called before the first frame update
    void Start()
    {
        /*hands = GameObject.Find("Hands");
        ObjectSelected = GameObject.Find("ObjectSelected");
        InventoryObject = GameObject.Find("PlayerInventory");
        inventory = InventoryObject.GetComponent<Inventory>();*/
    }

    // Update is called once per frame
    void Update()
    {
     /*   var input = Input.inputString;
        bool parseInt = Int32.TryParse(input, out number);
        if (parseInt)
        {
            //throw in hands
            hands.SetActive(true);
            inventory.objects.ElementAt(number).Value.transform.SetParent(ObjectSelected.transform, false);
        }
        if(Input.GetKeyDown("b"))
        {
            inventory.objects.ElementAt(number).Value.transform.SetParent(inventory.transform, false);
            hands.SetActive(false);
        }*/
    }
}
