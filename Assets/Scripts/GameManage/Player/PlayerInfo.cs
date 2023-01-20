using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    public int resources{ get;  private set; } = 100;
    public static int maxResources{ get; set; } = 100;
    public int seed { get; private set; } = Random.Range(1,10000000);
    public int currentLayer{ get; private set; }
    public string playerName{ get; private set; }
    public static int baseHarvestCost{ get; private set; } = 5;
    public int currentHarvestCost{ get; private set; }
    public int harvestCount{ get; private set; } = 0;
    public static int roundCostResources{ get; private set; } = 10;
    public int currentCostResources{ get; private set; }
    public static PlayerInfo Instance { get; private set; } = null;
    public void AddResources(int dif){
        if(resources + dif <= maxResources)
            resources += dif;
        else
            resources = maxResources;
    }
    public int HarvestCostInc(){
        currentHarvestCost = baseHarvestCost * (1 + (++harvestCount));
        return currentHarvestCost;
    }
    public void SwitchLayer(){
        if(currentLayer == 1)
            currentLayer = 2;
        else
            currentLayer = 1;
    }
    private void Start() {
        if (Instance != null) {
            Debug.LogError("PlayerInfo already exists.");
            return;
        }
        Instance = this;
        Random.InitState( PlayerInfo.Instance.seed );
    }
}
