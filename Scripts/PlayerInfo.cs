using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfo : MonoBehaviour
{
    public GameObject InventoryObject;
    private Inventory inventory;
    public Text pointsUI;
    public int points;
    Button[] items;
    // Start is called before the first frame update
    void Start()
    {
        InventoryObject = GameObject.Find("PlayerInventory");
        inventory = InventoryObject.GetComponent<Inventory>();
        items = this.GetComponentsInChildren<Button>();
        //points = GameObject.Find("Points");
    }
    public void update()
    {
        for (int i = 0; i< items.Length; i++)
        {
            TextMeshProUGUI name = items[i].GetComponentInChildren<TextMeshProUGUI>();
            try
            {
                name.text = inventory.objects.ElementAt(i).Value.name;
            }
            catch (Exception ex)
            {
                name.text = i.ToString();
            }
        }
    }
    public void updatePoints(int point)
    {
        points += point;
        pointsUI.text = points.ToString();
    }
}
