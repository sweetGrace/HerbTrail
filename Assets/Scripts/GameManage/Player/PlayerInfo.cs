using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    public int resources{ get; set; }
    public string playerName{ get; private set; }
    public static int baseHarvestCost{ get; private set; }
    public int currentHarvestCost{ get; private set; }
    public static PlayerInfo Instance { get; private set; } = null;
    private void Start() {
        if (Instance != null) {
            Debug.LogError("PlayerInfo already exists.");
            return;
        }
        Instance = this;
    }
}
