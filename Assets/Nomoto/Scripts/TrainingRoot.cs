﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class TrainingRoot : MonoBehaviour
{
    [SerializeField]
    AnimalStatusManager animalStatusManager = null;

    [SerializeField]
    GameObject HomeButton = null;

    [SerializeField]
    GameObject EatButton = null;

    [SerializeField]
    GameObject EatBoard = null;

    [SerializeField]
    GameObject BrushButton = null;

    [SerializeField]
    GameObject TalkButton = null;

    [SerializeField]
    Text NameText = null;

    [SerializeField]
    Text TalkText = null;

    [SerializeField]
    CutTextureManager HeartManager = null;

    [SerializeField]
    CutTextureManager EatManager = null;

    [SerializeField]
    RectTransform popPosition = null;

    [SerializeField]
    GameObject Heart = null;

    private bool isSelectButton = false;

    public int satietyLelel = 0;
    public int maxSatietyLevel = 0;

    public int loveLevel = 0;
    public int maxLoveLevel = 0;

    int foodType = 0;

    private bool isAnimation = false;

    enum Type
    {
        NONE,
        EAT,
        BRUSH,
        TALK,
        COMMENT,
        FOOD_COMMENT
    }

    enum FoodCommentType
    {
        NONE,
        LIKE,
        DONT_LIKE
    }

    FoodCommentType foodCommentType = FoodCommentType.NONE;

    Type animationType = Type.NONE;

    [SerializeField]
    GameObject Brush = null;

    Vector3 BrushPos;

    Vector3 BrushMoveSpeed;

    int BrushCount = 1;

    float BrushTime = 0.0f;

    [SerializeField]
    GameObject Food = null;

    [SerializeField]
    Sprite[] foodImage = null;

    Vector3 FoodPos;

    Vector3 FoodSize;

    float FoodTime = 0.0f;

    [SerializeField]
    GameObject CommentBoard = null;

    float animationTime = 0.0f;

    [SerializeField]
    GameObject Moya = null;

    Vector2 moyaSize;


    void Start()
    {
        animalStatusManager.status.Rarity = 4;
        animalStatusManager.status.FoodType = 0;
        int rarity = animalStatusManager.status.Rarity;
        foodType = animalStatusManager.status.FoodType;
        maxLoveLevel = rarity * 20;
        maxSatietyLevel = 10;

        BrushPos = Brush.GetComponent<RectTransform>().position;
        BrushMoveSpeed = new Vector3(-70, -10, 0);
        FoodPos = Food.GetComponent<RectTransform>().position;
        FoodSize = Food.GetComponent<RectTransform>().sizeDelta;
        moyaSize = Moya.GetComponent<RectTransform>().sizeDelta;
    }

    void Update()
    {
        if (isAnimation == true)
        {
            if (animationType == Type.BRUSH)
            {
                Brush.GetComponent<RectTransform>().position += BrushMoveSpeed * Time.deltaTime;

                if (0.6f * BrushCount < BrushTime)
                {
                    BrushMoveSpeed *= -1;
                    ++BrushCount;
                }

                BrushTime += Time.deltaTime;

                if (BrushTime > 3.5f)
                {
                    animationType = Type.COMMENT;
                    Brush.GetComponent<RectTransform>().position = BrushPos;
                    BrushTime = 0.0f;
                    Brush.SetActive(false);
                    CommentBoard.SetActive(true);

                    for (int i = 0; i < 10; ++i)
                    {
                        Vector3 pos = new Vector3(Random.Range(-20, 20), Random.Range(-30, 30), 1);
                        GameObject obj = Instantiate(Heart,
                                                     new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
                        obj.transform.SetParent(GameObject.Find("Canvas").transform);
                        obj.GetComponent<RectTransform>().position = popPosition.position + pos;
                        obj.GetComponent<MoveHeart>().Make();
                    }

                    Brush.GetComponent<RectTransform>().position = BrushPos;
                    BrushMoveSpeed = new Vector3(-70, -10, 0);
                    BrushCount = 1;
                }

            }

            else if (animationType == Type.EAT)
            {
                animationTime += Time.deltaTime;

                if (Food.GetComponent<RectTransform>().sizeDelta.x > 0)
                {
                    Food.GetComponent<RectTransform>().sizeDelta -= new Vector2(0.5f, 0.5f);
                }

                if (animationTime > 2.5f)
                {
                    animationTime = 0.0f;
                    animationType = Type.COMMENT;
                    Food.GetComponent<RectTransform>().sizeDelta = FoodSize;
                    Food.SetActive(false);
                    CommentBoard.SetActive(true);

                    if (foodCommentType == FoodCommentType.LIKE)
                    {
                        for (int i = 0; i < 10; ++i)
                        {
                            Vector3 pos = new Vector3(Random.Range(-20, 20), Random.Range(-30, 30), 1);
                            GameObject obj = Instantiate(Heart,
                                                         new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
                            obj.transform.SetParent(GameObject.Find("Canvas").transform);
                            obj.GetComponent<RectTransform>().position = popPosition.position + pos;
                            obj.GetComponent<MoveHeart>().Make();
                        }
                    }

                    else if (foodCommentType == FoodCommentType.DONT_LIKE)
                    {
                        Moya.SetActive(true);
                    }
                }
            }

            else if (animationType == Type.COMMENT)
            {
                animationTime += Time.deltaTime;

                if (foodCommentType == FoodCommentType.DONT_LIKE)
                {
                    Moya.GetComponent<RectTransform>().sizeDelta += new Vector2(0,0.4f);
                }
                if (animationTime > 2.5f)
                {
                    animationTime = 0.0f;
                    isAnimation = false;
                    animationType = Type.NONE;
                    CommentBoard.SetActive(false);
                    isSelectButton = false;
                    SetActiveButton(true);
                    if (foodCommentType == FoodCommentType.DONT_LIKE)
                    {
                        Moya.SetActive(false);
                        Moya.GetComponent<RectTransform>().sizeDelta = moyaSize;
                        foodCommentType = FoodCommentType.NONE;
                    }
                }
            }
        }
    }

    void SetActiveButton(bool active)
    {
        EatButton.SetActive(active);
        BrushButton.SetActive(active);
        TalkButton.SetActive(active);
        HomeButton.SetActive(active);
    }

    public void PushEatButton()
    {
        if (isSelectButton == true) return;
        isSelectButton = true;
        SetActiveButton(false);
        EatBoard.SetActive(true);
    }

    public void PushBrushButton()
    {
        if (isSelectButton == true) return;
        isSelectButton = true;
        SetActiveButton(false);

        if (loveLevel + 1 <= maxLoveLevel)
            loveLevel += 1;
        HeartManager.Change(loveLevel, maxLoveLevel);
        TalkText.text = "あぁん！！";

        isAnimation = true;
        animationType = Type.BRUSH;
        Brush.SetActive(true);
    }

    public void PushTalkButton()
    {
        if (isSelectButton == true) return;
        isSelectButton = true;
        SetActiveButton(false);
        loveLevel += 10;

        if (loveLevel > maxLoveLevel)
            loveLevel = maxLoveLevel;
        HeartManager.Change(loveLevel, maxLoveLevel);

        TalkText.text = "おぉん！！";
        isAnimation = true;
        animationType = Type.COMMENT;
        CommentBoard.SetActive(true);
    }

    public void PushBackButton()
    {

    }

    public void PushOfBackEatBoard()
    {
        isSelectButton = false;
        SetActiveButton(true);
        EatBoard.SetActive(false);
    }

    public void PushMeatType(int choiseFoodRank)
    {
        if (foodType == 0)
        {
            foodCommentType = FoodCommentType.LIKE;

            TalkText.text = "うまうま！！";
            switch (choiseFoodRank)
            {
                case 0:
                    satietyLelel += 1;
                    break;
                case 1:
                    satietyLelel += 2;
                    loveLevel += 1;
                    break;
                case 2:
                    satietyLelel += 3;
                    loveLevel += 2;
                    break;
            }
        }

        else
        {
            foodCommentType = FoodCommentType.DONT_LIKE;
            TalkText.text = "まずまず！！";
        }
        if (satietyLelel > maxSatietyLevel)
            satietyLelel = maxSatietyLevel;
        if (loveLevel > maxLoveLevel)
            loveLevel = maxLoveLevel;
        EatManager.Change(satietyLelel, maxSatietyLevel);
        EatBoard.SetActive(false);
        Food.SetActive(true);
        Food.GetComponent<Image>().sprite = foodImage[0];
        isAnimation = true;
        animationType = Type.EAT;
    }

    public void PushVegetableType(int choiseFoodRank)
    {
        if (foodType == 1)
        {
            foodCommentType = FoodCommentType.LIKE;
            TalkText.text = "うまうま！！";
            switch (choiseFoodRank)
            {
                case 0:
                    satietyLelel += 1;
                    break;
                case 1:
                    satietyLelel += 2;
                    loveLevel += 1;
                    break;
                case 2:
                    satietyLelel += 3;
                    loveLevel += 2;
                    break;
            }
        }
        else
        {
            foodCommentType = FoodCommentType.DONT_LIKE;
            TalkText.text = "まずまず！！";
        }
        if (satietyLelel > maxSatietyLevel)
            satietyLelel = maxSatietyLevel;
        if (loveLevel > maxLoveLevel)
            loveLevel = maxLoveLevel;
        EatManager.Change(satietyLelel, maxSatietyLevel);
        EatBoard.SetActive(false);
        Food.SetActive(true);
        Food.GetComponent<Image>().sprite = foodImage[1];
        isAnimation = true;
        animationType = Type.EAT;
    }


}
