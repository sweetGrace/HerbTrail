using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchPanel : MonoBehaviour
{
    Button switchButton;
    
    private void Awake()
    {
        switchButton = gameObject.transform.GetComponent<Button>();

        switchButton.onClick.AddListener(delegate ()
        {
            PlayerInfo.Instance.SwitchLayer();
        });

    }
}
