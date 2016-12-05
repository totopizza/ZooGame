﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FoodSatietyLevelChanger : MonoBehaviour
{
    public GameObject foodList;
    FoodList food;

    public GameObject foodIDSetter;
    FoodIDSetter setter;

    int ID;

    void Start()
    {
        food = new FoodList();
        food = foodList.GetComponent<FoodList>();

        setter = new FoodIDSetter();
        setter = foodIDSetter.GetComponent<FoodIDSetter>();

        ID = 0;

        TextUpdater();
    }

    public void TextUpdater()
    {
        ID = setter.GetID();

        var satietyLevelText = gameObject.GetComponent<Text>();
        satietyLevelText.text = "+" + food.foodList[ID].satietyLevelUpValue.ToString();
    }
}