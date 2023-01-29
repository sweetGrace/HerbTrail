using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchPanel : MonoBehaviour
{
    Button upButton,downButton;
    
    private void Awake()
    {
        upButton = gameObject.transform.GetChild(0).GetComponent<Button>();
        downButton = gameObject.transform.GetChild(1).GetComponent<Button>();

        upButton.onClick.AddListener(delegate ()
        {
            downButton.interactable = true;
            upButton.interactable = false;
            PlayerInfo.Instance.SwitchLayer();
        });

        downButton.onClick.AddListener(delegate ()
        {
            upButton.interactable = true;
            downButton.interactable = false;
            PlayerInfo.Instance.SwitchLayer();
        });
    }
}
