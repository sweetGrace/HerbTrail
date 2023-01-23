using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowOverUI : MonoBehaviour
{
    [SerializeField]
    GameObject gameOverPanel,startPanel,gamePanel;

    public static ShowOverUI instance { get; private set; } = null;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void Show()
    {
        startPanel.SetActive(false);
        gamePanel.SetActive(false);
        gameOverPanel.SetActive(true);
    }
}
