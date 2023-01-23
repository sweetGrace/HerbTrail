using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoundManager : MonoBehaviour {

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

    public void StartNextRound() {
        //TODO plant generate
        Map.Instance.generateOrganList.ForEach( p => {
            PlantOrganFactory.Instance.GeneratePlantOrgan(p.OrganType, p.type, p.atLattice.position, Quaternion.identity).GetComponent<PlantOrgan>().InitMe(p);
            p.twigsList.ForEach(q => q = PlantOrganFactory.Instance.GenerateTwig(p.type, p.atLattice.position, Quaternion.Euler(0, 0, Vector2Quaternion(p.relativeDirection))).GetComponent<Twig>());
            } );
        //TODO Sound play
    }

    public void EndCurrentRound() {
        PlayerInfo.Instance.AddResources(-PlayerInfo.Instance.baseHarvestCost) ;
        //judge if gameover
        Map.Instance.ClearWithering();
        Map.Instance.SpreadSea();
        Map.Instance.GenerateOrgansOnMap();


        StartNextRound();
    }
}
