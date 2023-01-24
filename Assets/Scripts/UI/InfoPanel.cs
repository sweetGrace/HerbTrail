using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoPanel : MonoBehaviour
{
    public static InfoPanel Instance { get; private set; } = null;

    Text hungerText, costText;

    private void Start()
    {
        if (Instance != null)
        {
            Debug.LogError("InfoPanel already exists.");
            return;
        }
        Instance = this;
        hungerText = this.transform.GetChild(0).GetComponent<Text>();
        costText = this.transform.GetChild(1).GetComponent<Text>();
    }

    public void UpdateHungerAndCost()
    {
        hungerText.text ="饥饿值：" + PlayerInfo.Instance.resources.ToString();
        costText.text = "本次收获消耗饥饿值：" + PlayerInfo.Instance.currentRoundCostResources.ToString();
    }
}
