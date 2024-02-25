using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Item", menuName = "new Item", order = 1)]
public class ItemScriptableObject : ScriptableObject
{
    public string item_name;
    public Sprite item_image;
    public GameObject item_model;


    public bool cookable;
    public bool isFinalDish;
    public bool stackable;
    public float stunTime;
}
