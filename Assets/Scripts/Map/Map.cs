using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class Map : MonoBehaviour
{
    public Plant[] plantSet;
    public Lattice[,] latticeMap = new Lattice[513, 513];//each quadrant is 256*256
    public static Map Instance {get; private set;}
    private void _SpreadSea(){
        for(int i = 1; i < 512; i++){
            for(int j = 1; j <512; j++){

            }
        }
    }
    private void _ClearWithering(){
        foreach(var lattice in Map.Instance.latticeMap){
            List<PlantOrgan> witheringList = lattice.plantOrgans.Where( organ => organ.isWithering == true).ToList();
            witheringList.ForEach( organ => lattice.plantOrgans.Remove(organ) );
            lattice.ground.AddFertilityDegree(witheringList.Count);
        }
    }
    private void _HarvestPlant(Lattice lattice, List<PlantOrgan> organs){

    } 
    // Start is called before the first frame update
    void Start()
    {
        if (Instance != null) {
            Debug.LogError("Map already exists.");
            return;
        }
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
