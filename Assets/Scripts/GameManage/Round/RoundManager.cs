using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;
using System;

public class RoundManager : MonoBehaviour {
    Tilemap tilemap;
    private int _currentRound = 0;
    public int roundCount { get => _currentRound; }
    public static RoundManager Instance { get; private set; } = null;
    public float Vector2Quaternion(Vector2 i){
        float ans;
        if(Vector2.up == i)
            ans = 0;
        else if(Vector2.left == i)
            ans = 270;
        else if(Vector2.down == i)
            ans = 180;
        else   
            ans = 90;
        return ans;
    }
    private void Start() {
        if (Instance != null) {
            Debug.LogError("RoundManager already exists.");
            return;
        }
        Instance = this;
        StartNextRound();
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
        Map.Instance.GeneratePlantsOnMap();
        CreateRange.Instance.CreateWarningRange();
        Map.Instance.generateOrganList.Where(q => q.layer == 1).ToList().ForEach(p => {
            Destroy(p.gameObject);
            p?.fatherNode?.twigsList.ForEach(r => r.ChangeStatePic(r.stateSpreadingPics));
            PlantOrganFactory.Instance.GeneratePlantOrgan(p.OrganType, p.type,tilemap.CellToWorld(new Vector3Int(Convert.ToInt32(p.atLattice.position.x), Convert.ToInt32(p.atLattice.position.y), 0)), Quaternion.identity)
            .GetComponent<PlantOrgan>().InitMe(p);
            p.twigsList.ForEach(q => q = PlantOrganFactory.Instance.GenerateTwig(p.type, tilemap.CellToWorld(new Vector3Int(Convert.ToInt32(p.atLattice.position.x), Convert.ToInt32(p.atLattice.position.y), 0)), Quaternion.Euler(0, 0, Vector2Quaternion(p.relativeDirection))).GetComponent<Twig>());
            p.plant.plantOrgans.Add(p);
        } );
        Map.Instance.generateOrganList.Where(q => q.layer == 2).ToList().ForEach(p => {
            Destroy(p.gameObject);
            p?.fatherNode?.twigsList.ForEach(r => r.ChangeStatePic(r.stateSpreadingPics));
            PlantOrganFactory.Instance.GeneratePlantOrgan(p.OrganType, p.type, CreateRange.Instance.CubeTreeTranslate((Vector3)p.atLattice.position), Quaternion.identity)
            .GetComponent<PlantOrgan>().InitMe(p);
            p.twigsList.ForEach(q => q = PlantOrganFactory.Instance.GenerateTwig(p.type, CreateRange.Instance.CubeTreeTranslate((Vector3)p.atLattice.position), Quaternion.Euler(0, 0, Vector2Quaternion(p.relativeDirection))).GetComponent<Twig>());
            p.plant.plantOrgans.Add(p);
        } );
        SoundManager.Instance.PlaySingle(ClipsType.plantGrow);
    }

    public void EndCurrentRound() {
        PlayerInfo.Instance.AddResources(-PlayerInfo.Instance.baseRoundCostResources) ;
        if(PlayerInfo.Instance.resources <= 0)
            ShowOverUI.Instance.Show();
        InfoPanel.Instance.UpdateHungerAndCost();
        Map.Instance.plantSet.ForEach(plant => plant.plantOrgans.Where(organ => organ.isGenerating == false).ToList().ForEach(p => p.spreadOrgans.AddRange(p.SpreadPlant())));
        Map.Instance.plantSet.ForEach(plant => plant.plantOrgans.Where(organ => organ.isGeneratingFruit == false).ToList().ForEach(p => p.spreadOrgans.AddRange(p.GenerateFruits())));
        Map.Instance.plantSet.ForEach(plant => plant.plantOrgans.OfType<Fruit>().ToList().ToList().ForEach(fruit => fruit.GrowingUpdate(fruit.atLattice.ground.fertilityDegree)));
        Map.Instance.ClearWithering();
        Map.Instance.GenerateWaterOnMap();
        SoundManager.Instance.PlaySingle(ClipsType.waterSpread);
        Map.Instance.GenerateOrgansOnMap();
        StartNextRound();
    }
}
