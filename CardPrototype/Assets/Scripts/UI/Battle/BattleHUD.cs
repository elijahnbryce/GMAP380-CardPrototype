using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleHUD : MonoBehaviour
{
    [SerializeField] private Slider hpSlider;

    public void SetHUD(DeckMaster master)
    {
        return;
    }

    public void SetHP(int hp)
    {
        hpSlider.value = hp;
    }
}