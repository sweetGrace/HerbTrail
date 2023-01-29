using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class PlayerInfo : MonoBehaviour
{
    public int resources{ get; private set; } = 100;
    public int maxResources{ get; set; } = 100;
    public int seed { get; private set; }
    public int currentLayer{ get; private set; } = 1;
    public string playerName{ get; private set; }
    public int baseHarvestCost{ get; private set; } = 5;
    public int currentHarvestCost{ get; private set; } = 5;
    public int harvestCount{ get; private set; } = 0;
    public int baseRoundCostResources{ get; private set; } = 10;
    public int currentRoundCostResources{ get; private set; } = 10;
    public int layerHeight{ get; private set; } = 10;
    public static PlayerInfo Instance { get; private set; } = null;
    public static float alphaDegree { get; private set; } = 0.5f;
    public void AddResources(int dif){
        if(resources + dif <= maxResources)
            resources += dif;
        else
            resources = maxResources;
    }
    public Func<Color, float, Color> AlterAlpha = (x, i) => {
        Color y = new Color(x.r, x.g, x.b, i);
        return y;
    };
    public int HarvestCostInc(){
        currentHarvestCost = baseHarvestCost * (1 + (++harvestCount));
        return currentHarvestCost;
    }
    public void SwitchLayer(){
        Action<int, int> SwitchTransparent = (a, b) =>{//a becomes transparent and b becomes opposite
            Map.Instance.plantSet.ForEach(p => {
                p.plantOrgans.Where(q => q.layer == a).ToList()
                .ForEach(r => {
                    if(r.statePicRenderer == null) 
                        Debug.Log("null");
                    if(r.gameObject == null)
                        Debug.Log("gb null");
                        r.statePicRenderer = r.gameObject.GetComponent<SpriteRenderer>();
                    r.statePicRenderer.color = AlterAlpha(r.statePicRenderer.color, alphaDegree);
                    });
                p.plantOrgans.Where(q => q.layer == b).ToList()
                .ForEach(r =>{
                    if(r.statePicRenderer == null) 
                        Debug.Log("null");
                    if(r.gameObject == null)
                        Debug.Log("gb null");
                        r.statePicRenderer = r.gameObject.GetComponent<SpriteRenderer>();
                    r.statePicRenderer.color = AlterAlpha(r.statePicRenderer.color, alphaDegree);
                    });
                
            });
        };
        if(currentLayer == 1){
            currentLayer = 2;
            SwitchTransparent(1, 2);
        }
        else{
            currentLayer = 1;
            SwitchTransparent(2, 1);
        }
    }
    public void ClearAll(){
        currentHarvestCost = baseHarvestCost;
        harvestCount = 0;
    }
    private void Start() {
        if (Instance != null) {
            Debug.LogError("PlayerInfo already exists.");
            return;
        }
        Instance = this;
        this.seed = UnityEngine.Random.Range(1,10000000);
        currentRoundCostResources = baseRoundCostResources;
        UnityEngine.Random.InitState(seed);
    }
}
