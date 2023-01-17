using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoundManager : MonoBehaviour {

    private int _currentRound = 0;
    public int roundCount { get => _currentRound; }
    public static RoundManager Instance { get; private set; }
    public static PlayerInfo playerInstance { get; private set; }

    private void Start() {
        if (Instance != null) {
            Debug.LogError("RoundManager already exists.");
            return;
        }
        Instance = this;
        StartNextRound();
    }

    public void StartNextRound() {

    }

    public void EndCurrentRound() {

    }
}
