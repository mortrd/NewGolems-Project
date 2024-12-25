using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gold : MonoBehaviour
{
    [SerializeField] Text GoldText;
    public int GoldCount = 0;

    public void AddGold(int GoldAmmount)
    {
        GoldCount = GoldCount + GoldAmmount;
        GoldText.text = GoldCount.ToString();
    }
    public void Pay(int cost)
    {
        GoldCount -= cost;
        GoldText.text = GoldCount.ToString();
    }
}
