using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
public class Map : MonoBehaviour
{
    public List<Plant> plantSet;
    public List<PlantOrgan> generateOrganList = new List<PlantOrgan>();
    public Lattice[,] latticeMap = new Lattice[513, 513];//each quadrant is 256*256
    public static Map Instance {get; private set;} = null;
    
    public void GenerateOrgansOnMap(){
        plantSet.ForEach( p => p.plantOrgans.ForEach( q => generateOrganList.AddRange(q.spreadOrgans
        .Where( r => r.isPlanted == false && latticeMap[Convert.ToInt32(r.atLattice.position.x), Convert.ToInt32(r.atLattice.position.y)].ground.type != GroundType.seawater
         && latticeMap[Convert.ToInt32(r.atLattice.position.x), Convert.ToInt32(r.atLattice.position.y)].plantOrgans
        .Where( s => s.layer == r.layer).ToList().Count == 0 && (generateOrganList.Where( t => t.layer == r.layer &&
        generateOrganList.Where( u => Convert.ToInt32(u.atLattice.position.x) == Convert.ToInt32(r.atLattice.position.x) &&
        Convert.ToInt32(u.atLattice.position.y) == Convert.ToInt32(r.atLattice.position.y) ).ToList().Count == 0).ToList().Count == 0)))  ));
    }
    public void SpreadSea(){
        for(int i = 1; i < 512; i++){
            for(int j = 1; j < 512; j++){

                if (latticeMap[i, j].IsWater()==0) {
                    //第一种与第二种
                    if (latticeMap[i - 1, j].IsWater() + latticeMap[i + 1, j].IsWater() + latticeMap[i, j - 1].IsWater() + latticeMap[i, j + 1].IsWater() >= 2)
                    {
                        //未完成
                    }
                }
            }
        }
    }
    public void ClearWithering(){
        foreach(var lattice in Map.Instance.latticeMap){
            List<PlantOrgan> witheringList = lattice.plantOrgans.Where( organ => organ.isWithering == true).ToList();
            witheringList.ForEach( organ => lattice.plantOrgans.Remove(organ) );
            lattice.ground.AddFertilityDegree(witheringList.Count);
        }
    }
    public void HarvestPlant(Lattice lattice, PlantOrgan morgan=null){
        bool flag = true;//用来判断是否有第二层落下
        if (lattice == morgan.lattice) {
            if (morgan)
            {
                if (morgan.layer == 2 || morgan.OrganType == PlantOrganType.fruit)
                {
                    morgan.Harvest();
                }
                else
                {
                    foreach (var organ in lattice.plantOrgans)
                    {
                        if (organ.layer == 2 && (organ.OrganType == PlantOrganType.root || organ.OrganType == PlantOrganType.branch))//第二层且为根或者branch
                        {
                            organ.Harvest();
                            flag = false;
                        }

                    }
                    if (flag == true)
                    {
                        foreach (var organ in lattice.plantOrgans)
                        {
                            organ.Harvest();
                        }
                    }
                }
            }
            else//harvest whole lattice
            {
                foreach (var organ in lattice.plantOrgans)
                {
                    if (organ.layer == 2 && (organ.OrganType == PlantOrganType.root || organ.OrganType == PlantOrganType.branch))//第二层且为根或者branch
                    {
                        organ.Harvest();
                        flag = false;
                    }

                }
                if (flag == true)
                {
                    foreach (var organ in lattice.plantOrgans)
                    {
                        organ.Harvest();
                    }
                }
            }
                //
        }
        else
        {
            Debug.LogError("Lattice and organ not match");
        }
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
