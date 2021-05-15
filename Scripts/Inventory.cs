using System.Collections.Generic;
using UnityEngine;

public class Inventory: MonoBehaviour
{
    public Dictionary<string,GameObject> objects = new Dictionary<string, GameObject>();
    private GameObject playerUI;
    private PlayerInfo playerInfo;
    public void Start()
    {
        playerUI = GameObject.Find("UserInterface");
        playerInfo = playerUI.GetComponent<PlayerInfo>();
    }
    public void pickupItem(GameObject item)
    {
        objects.Add(item.name,item);
        item.transform.SetParent(this.transform , false);
        notify();
    }

    public void putDownItem(GameObject item)
    {
        objects.Remove(item.name);
        item.transform.SetParent(item.transform, false);
        notify();
    }
    internal void notify()
    {
        playerInfo.update();
    }
}