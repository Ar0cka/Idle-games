using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenInventory : MonoBehaviour
{
    [SerializeField] private GameObject inventory;
    [SerializeField] private GameObject itemPanel;

    private void Awake()
    {
        itemPanel.SetActive(false);
        inventory.SetActive(false);
    }

    public void Open()
    {
        inventory.SetActive(true);
    }

    public void Close()
    {
        inventory.SetActive(false);
    }
}
