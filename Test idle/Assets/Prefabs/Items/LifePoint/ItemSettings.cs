using System;
using System.Collections;
using System.Collections.Generic;
using Scriptable_object.Items;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class ItemSettings : MonoBehaviour
{
    [SerializeField] private BaseAbstractItem _abstractItem;
    public BaseAbstractItem baseAbstractItem => _abstractItem;
    
    private Image image;

    private void Awake()
    {
        image = GetComponent<Image>();
        image.sprite = _abstractItem.iconItem;
    }
}
