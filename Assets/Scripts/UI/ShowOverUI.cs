using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowOverUI : MonoBehaviour
{
    [SerializeField]
    GameObject gameOverPanel,startPanel,gamePanel;

    public static ShowOverUI Instance { get; private set; } = null;

    private void Start()
    {
        if (Instance != null)
        {
            Debug.LogError("ShowOverUI already exists.");
            return;
        }
        Instance = this;
    }

    public void Show()
    {
        startPanel.SetActive(false);
        gamePanel.SetActive(false);
        gameOverPanel.SetActive(true);
        SoundManager.Instance.DisableManager();
    }
}
