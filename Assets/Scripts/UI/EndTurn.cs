using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndTurn : MonoBehaviour
{
    Button endTurnButton;
    private void Awake()
    {
        endTurnButton = gameObject.GetComponent<Button>();
        endTurnButton.onClick.AddListener(delegate ()
        {
            RoundManager.Instance.EndCurrentRound();
        });
    }
}
