using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
public class Map : MonoBehaviour
{
    #region PRESETVALUE
    //planst generate possbilities
    [Range(0, 1)]
    private double _pTreePossibility = 0.1;
    [Range(0, 1)]
    private double _oThornPossibility = 0.3;
    [Range(0, 1)]
    private double _hBushPossibility = 0.2;
    [Range(0, 1)]
    private double _hVinePossibility = 0.2;
    [Range(1, 255)]
    private int _initialRange = 5;
    [Range(0, 1)]
    private double _P = 0.5;// water generate probability, the bigger the more possible 

    #endregion
    public List<Plant> plantSet;
    public List<PlantOrgan> generateOrganList = new List<PlantOrgan>();
    public List<Lattice> generateWaterList = new List<Lattice>();
    public Lattice[,] latticeMap = new Lattice[513, 513];//each quadrant is 256*256
    public static Map Instance { get; private set; } = null;
    public void InitLattice()
    {
        for (int i = 0; i < 513; i++)
        {
            for (int j = 0; j < 513; j++)
            {
                if(i<256- _initialRange||i>256+ _initialRange||j<256- _initialRange||j>256+ _initialRange)
                {
                    latticeMap[i,j].ground.type = GroundType.seawater;
                    latticeMap[i,j].ground.atLattice = latticeMap[i,j];
                    latticeMap[i, j].position = new Vector2(i, j);
                }
                else
                {
                    latticeMap[i,j].ground.type = GroundType.plain;
                    latticeMap[i,j].ground.atLattice = latticeMap[i,j];
                    latticeMap[i, j].position = new Vector2(i, j);
                }
            }
        }
    }
    public void GenerateOrgansOnMap()
    {
        generateOrganList.Clear();
        plantSet.ForEach(p => p.plantOrgans.ForEach(q => generateOrganList.AddRange(q.spreadOrgans
        .Where(r => r.isPlanted == false && latticeMap[Convert.ToInt32(r.atLattice.position.x), Convert.ToInt32(r.atLattice.position.y)].ground.type != GroundType.seawater
        && latticeMap[Convert.ToInt32(r.atLattice.position.x), Convert.ToInt32(r.atLattice.position.y)].plantOrgans
        .Where(s => s.layer == r.layer).ToList().Count == 0 && (generateOrganList.Where(t => t.layer == r.layer &&
        generateOrganList.Where(u => Convert.ToInt32(u.atLattice.position.x) == Convert.ToInt32(r.atLattice.position.x) &&
        Convert.ToInt32(u.atLattice.position.y) == Convert.ToInt32(r.atLattice.position.y)).ToList().Count == 0).ToList().Count == 0)))));
    }
    public void GeneratePlantsOnMap()
    {
        if (_pTreePossibility + _oThornPossibility + _hBushPossibility + _hVinePossibility > 1)
        {
            Debug.LogError("Error at map.GeneratePlantsOnMap:Possibility sum more than 1");
            return;
        }
        if (RoundManager.Instance.roundCount == 1)
        {
            List<Lattice> planeList = new List<Lattice>();
            for (int i = 0; i < 513; i++)
            {
                for (int j = 0; j < 513; j++)
                {
                    if (latticeMap[i, j].ground.type == GroundType.plain)
                        planeList.Add(latticeMap[i, j]);
                }
            }

            if (planeList.Count == 0)
                Debug.LogError("Error at map.GeneratePlantsOnMap:nowhere to plant on 1 round");
            else
            {
                int r = new System.Random().Next(planeList.Count);
                Plant tmpPlant = new Plant();
                tmpPlant.InitMe(PlantType.platformTree);
                Map.Instance.plantSet.Add(tmpPlant);
                Root tmpRoot = new Root();
                tmpPlant.plantOrgans.Add(tmpRoot);
                tmpRoot.InitMe(2, tmpPlant, PlantType.platformTree, null, planeList[r], Vector2.zero, null);
                PlantOrganFactory.Instance.GeneratePlantOrgan(PlantOrganType.root, PlantType.platformTree, (Vector3)planeList[r].position, Quaternion.identity)
                .GetComponent<PlantOrgan>().InitMe(tmpRoot);
            }

        }
        for (int i = 0; i < 513; i++)
        {
            for (int j = 0; j < 513; j++)
            {
                if (latticeMap[i, j].ground.type == GroundType.plain)
                {
                    System.Random random = new System.Random();
                    double t = random.NextDouble();
                    if (t < _pTreePossibility)
                    {
                        Plant tmpPlant = new Plant();
                        tmpPlant.InitMe(PlantType.platformTree);
                        Map.Instance.plantSet.Add(tmpPlant);
                        Root tmpRoot = new Root();
                        tmpPlant.plantOrgans.Add(tmpRoot);
                        tmpRoot.InitMe(2, tmpPlant, PlantType.platformTree, null, latticeMap[i,j], Vector2.zero, null);
                        PlantOrganFactory.Instance.GeneratePlantOrgan(PlantOrganType.root, PlantType.platformTree, (Vector3)latticeMap[i,j].position, Quaternion.identity)
                        .GetComponent<PlantOrgan>().InitMe(tmpRoot);
                    }
                    else if (t < _pTreePossibility + _oThornPossibility)
                    {
                        Plant tmpPlant = new Plant();
                        tmpPlant.InitMe(PlantType.obstacleThorn);
                        Map.Instance.plantSet.Add(tmpPlant);
                        Root tmpRoot = new Root();
                        tmpPlant.plantOrgans.Add(tmpRoot);
                        tmpRoot.InitMe(2, tmpPlant, PlantType.obstacleThorn, null, latticeMap[i,j], Vector2.zero, null);
                        PlantOrganFactory.Instance.GeneratePlantOrgan(PlantOrganType.root, PlantType.obstacleThorn, (Vector3)latticeMap[i,j].position, Quaternion.identity)
                        .GetComponent<PlantOrgan>().InitMe(tmpRoot);
                    }
                    else if (t < _pTreePossibility + _oThornPossibility + _hBushPossibility)
                    {
                        Plant tmpPlant = new Plant();
                        tmpPlant.InitMe(PlantType.harvestBush);
                        Map.Instance.plantSet.Add(tmpPlant);
                        Root tmpRoot = new Root();
                        tmpPlant.plantOrgans.Add(tmpRoot);
                        tmpRoot.InitMe(2, tmpPlant, PlantType.harvestBush, null, latticeMap[i,j], Vector2.zero, null);
                        PlantOrganFactory.Instance.GeneratePlantOrgan(PlantOrganType.root, PlantType.harvestBush, (Vector3)latticeMap[i,j].position, Quaternion.identity)
                        .GetComponent<PlantOrgan>().InitMe(tmpRoot);
                    }
                    else if (t < _pTreePossibility + _oThornPossibility + _hBushPossibility + _hVinePossibility)
                    {
                        Plant tmpPlant = new Plant();
                        tmpPlant.InitMe(PlantType.harvestVine);
                        Map.Instance.plantSet.Add(tmpPlant);
                        Root tmpRoot = new Root();
                        tmpPlant.plantOrgans.Add(tmpRoot);
                        tmpRoot.InitMe(2, tmpPlant, PlantType.harvestVine, null, latticeMap[i,j], Vector2.zero, null);
                        PlantOrganFactory.Instance.GeneratePlantOrgan(PlantOrganType.root, PlantType.harvestVine, (Vector3)latticeMap[i,j].position, Quaternion.identity)
                        .GetComponent<PlantOrgan>().InitMe(tmpRoot);
                    }
                }
            }
        }
    }
    public void GenerateWaterOnMap()//generate predictive water lattice in a list
    {
        generateWaterList.Clear();
        for (int i = 1; i < 512; i++)
        {
            for (int j = 1; j < 512; j++)
            {
                //each of these number refers to the lattices adjacent to the center
                int a1 = latticeMap[i - 1, j - 1].IsWater();//top left
                int a2 = latticeMap[i, j - 1].IsWater();//top
                int a3 = latticeMap[i + 1, j - 1].IsWater();//top right
                int a4 = latticeMap[i - 1, j].IsWater();// left
                int a5 = latticeMap[i, j].IsWater();//center
                int a6 = latticeMap[i + 1, j].IsWater();//right
                int a7 = latticeMap[i - 1, j + 1].IsWater();//bottom left
                int a8 = latticeMap[i, j + 1].IsWater();//bottom
                int a9 = latticeMap[i + 1, j + 1].IsWater();//bottom right
                if (a5 == 0 && latticeMap[i, j].plantOrgans.Exists(p => p.type == PlantType.obstacleThorn))
                {
                    //cross have two ("L" and "---")
                    if (a2 + a4 + a6 + a8 >= 2)
                    {
                        generateWaterList.Add(latticeMap[i, j]);
                    }
                    //diagonal "\" and "/"
                    else if (a1 + a9 == 2 || a3 + a7 == 2)
                    {
                        generateWaterList.Add(latticeMap[i, j]);
                    }
                    // threesome
                    else if (a1 + a2 + a3 == 3 || a1 + a4 + a7 == 3 || a3 + a6 + a9 == 3 || a7 + a8 + a9 == 3)
                    {
                        System.Random random = new System.Random();
                        if (random.NextDouble() < _P)
                        {
                            generateWaterList.Add(latticeMap[i, j]);
                        }
                    }
                }
            }
        }

    }
    public void PutWaterOnMap()
    {
        foreach (var p in generateWaterList)
        {
            p.Watered();
        }
    }
    public void ClearWithering()
    {
        foreach (var lattice in Map.Instance.latticeMap)
        {
            List<PlantOrgan> witheringList = lattice.plantOrgans.Where(organ => organ.isWithering == true).ToList();
            witheringList.ForEach(organ =>
            {
                organ.twigsList.ForEach(p => Destroy(p.gameObject));
                Destroy(organ.gameObject);
                lattice.plantOrgans.Remove(organ);
            });
            lattice.ground.AddFertilityDegree(witheringList.Count);

        }
    }

    public void HarvestPlant(Lattice lattice, PlantOrgan morgan = null)
    {
        bool flag = true;//to judge if a second floor fall
        if (lattice == morgan.lattice)
        {
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
                        if (organ.layer == 2 && (organ.OrganType == PlantOrganType.root || organ.OrganType == PlantOrganType.branch))//second floor is root or branch
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
                    if (organ.layer == 2 && (organ.OrganType == PlantOrganType.root || organ.OrganType == PlantOrganType.branch))//second floor is root or branch
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
            RoundManager.Instance.StateUpdateInRound();//todo :check if correct
        }
        else
        {
            Debug.LogError("Error at map.harvestplant:Lattice and organ not match");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (Instance != null)
        {
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
