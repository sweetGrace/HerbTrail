using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartGame : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        Button startButton = this.GetComponent<Button>();

        startButton.onClick.AddListener(delegate ()
        {
            SoundManager.Instance.ActivateManager();
            RoundManager.Instance.StartNextRound();
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
