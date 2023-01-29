using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;
using System;

public class RoundManager : MonoBehaviour {
    public Tilemap tilemap;
    public Tile seawaterTile;
    public Tile plainTile;
    private int _currentRound = 0;
    public int roundCount { get => _currentRound; }
    public static RoundManager Instance { get; private set; } = null;
    public float Vector2Quaternion(Vector2 i){
        float ans;
        if(Vector2.up == i)
            ans = 180;
        else if(Vector2.left == i)
            ans = 90;
        else if(Vector2.down == i)
            ans = 0;
        else   
            ans = 270;
        return ans;
    }
    private void Start() {
        if (Instance != null) {
            Debug.LogError("RoundManager already exists.");
            return;
        }
        Instance = this;        
        
    }
    public void StateUpdateInRound(){
        Map.Instance.GenerateOrgansOnMap();
        Map.Instance.generateOrganList.Where(q => q.layer == 1).ToList().ForEach(p => {
            PlantOrganFactory.Instance.GeneratePlantOrgan(p.OrganType, p.type,tilemap.CellToWorld(new Vector3Int(Convert.ToInt32(p.atLattice.position.x), Convert.ToInt32(p.atLattice.position.y), 0)), Quaternion.identity)
            .GetComponent<PlantOrgan>().InitMe(p);
            p.gameObject.GetComponent<SpriteRenderer>().color = PlayerInfo.Instance.AlterAlpha(p.gameObject.GetComponent<SpriteRenderer>().color, 0.3f);
        });
        Map.Instance.generateOrganList.Where(q => q.layer == 2).ToList().ForEach(p => {
            PlantOrganFactory.Instance.GeneratePlantOrgan(p.OrganType, p.type, CreateRange.Instance.CubeTreeTranslate((Vector3)p.atLattice.position), Quaternion.identity)
            .GetComponent<PlantOrgan>().InitMe(p);
            p.gameObject.GetComponent<SpriteRenderer>().color = PlayerInfo.Instance.AlterAlpha(p.gameObject.GetComponent<SpriteRenderer>().color, 0.3f);
        });
    }
    public void StartNextRound() {
        _currentRound++;
        InfoPanel.Instance.UpdateHungerAndCost();
        Map.Instance.GenerateWaterOnMap();
        Map.Instance.GeneratePlantsOnMap();
        CreateRange.Instance.CreateWarningRange();
        Map.Instance.generateOrganList.Where(q => q.layer == 1).ToList().ForEach(p => {
            Destroy(p.gameObject);
            p?.fatherNode?.twigsList.ForEach(r => r.ChangeStatePic(r.stateSpreadingPics));
            PlantOrganFactory.Instance.GeneratePlantOrgan(p.OrganType, p.type,tilemap.CellToWorld(new Vector3Int(Convert.ToInt32(p.atLattice.position.x), Convert.ToInt32(p.atLattice.position.y), 0)), Quaternion.identity)
            .GetComponent<PlantOrgan>().InitMe(p);
            p.twigsList.ForEach(q => q = PlantOrganFactory.Instance.GenerateTwig(p.type, tilemap.CellToWorld(new Vector3Int(Convert.ToInt32(p.atLattice.position.x), Convert.ToInt32(p.atLattice.position.y), 0)), Quaternion.Euler(0, 0, Vector2Quaternion(p.relativeDirection))).GetComponent<Twig>());
            p.plant.plantOrgans.Remove(p);
        } );
        Map.Instance.generateOrganList.Where(q => q.layer == 2).ToList().ForEach(p => {
            Destroy(p.gameObject);
            p?.fatherNode?.twigsList.ForEach(r => r.ChangeStatePic(r.stateSpreadingPics));
            PlantOrganFactory.Instance.GeneratePlantOrgan(p.OrganType, p.type, CreateRange.Instance.CubeTreeTranslate((Vector3)p.atLattice.position), Quaternion.identity)
            .GetComponent<PlantOrgan>().InitMe(p);
            p.statePicRenderer.color = PlayerInfo.Instance.AlterAlpha(p.statePicRenderer.color, PlayerInfo.alphaDegree);
            p.twigsList.ForEach(q => q = PlantOrganFactory.Instance.GenerateTwig(p.type, CreateRange.Instance.CubeTreeTranslate((Vector3)p.atLattice.position), Quaternion.Euler(0, 0, Vector2Quaternion(p.relativeDirection))).GetComponent<Twig>());
            p.plant.plantOrgans.Remove(p);
        } );
        SoundManager.Instance.PlaySingle(ClipsType.plantGrow);
        Map.Instance.GenerateOrgansOnMap();
    }

    public void EndCurrentRound() {
        PlayerInfo.Instance.AddResources(-PlayerInfo.Instance.baseRoundCostResources) ;
        if(PlayerInfo.Instance.resources <= 0)
            ShowOverUI.Instance.Show();
        PlayerInfo.Instance.ClearAll();
        //InfoPanel.Instance.UpdateHungerAndCost();
        /*
        Map.Instance.plantSet.ForEach(plant => {
            if(plant.plantOrgans == null)
                Debug.Log("1 null");
            plant.plantOrgans.ForEach(p => {if(p == null) Debug.Log("2 null");});
            plant.plantOrgans.Where(organ => organ.isGenerating == false).ToList().ForEach(p => {
                if(p.spreadOrgans == null)
                    Debug.Log("3 null");
                p.spreadOrgans.AddRange(p.SpreadPlant());
            });
        });*/
        Map.Instance.plantSet.ForEach(plant => plant.plantOrgans.Where(organ => organ.isGenerating == false).ToList().ForEach(p => p.spreadOrgans.AddRange(p.SpreadPlant())));
        Map.Instance.plantSet.ForEach(plant => plant.plantOrgans.Where(organ => organ.isGeneratingFruit == false).ToList().ForEach(p => p.spreadOrgans.AddRange(p.GenerateFruits())));
        Map.Instance.plantSet.ForEach(plant => plant.plantOrgans.OfType<Fruit>().ToList().ToList().ForEach(fruit => fruit.GrowingUpdate(fruit.atLattice.ground.fertilityDegree)));
        Map.Instance.ClearWithering();
        Map.Instance.GenerateWaterOnMap();
        Map.Instance.generateWaterList.ForEach(p => tilemap.SetTile(new Vector3Int(Convert.ToInt32(p.position.x), Convert.ToInt32(p.position.y), 0), seawaterTile));
        SoundManager.Instance.PlaySingle(ClipsType.waterSpread);
        Map.Instance.GenerateOrgansOnMap();
        StartNextRound();
    }
}
