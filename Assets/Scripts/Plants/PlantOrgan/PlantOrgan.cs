using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
public abstract class PlantOrgan : MonoBehaviour
{

    protected static int _IdCount = 0;
    public static List<(PlantType, List<int>)> spreadProbability = new List<(PlantType, List<int>)> {
        (PlantType.obstacleThorn, new List<int>{}),
        (PlantType.platformTree, new List<int>{}),
        (PlantType.harvestBush, new List<int>{}),
        (PlantType.harvestVine, new List<int>{})
    };//corresponding with SpreadType
    public static List<(PlantType, int)> resourcesList = new List<(PlantType, int)> {
        (PlantType.platformTree, 0),
        (PlantType.obstacleThorn, 0),
        (PlantType.harvestBush, 0),
        (PlantType.harvestVine, 0),
    };
    public SpriteRenderer statePicRenderer;
    public Sprite[] statePics;
    public Lattice atLattice{ get; protected set; }
    public int Id { get; protected set; }
    public Plant plant { get; protected set; }
    public PlantType type {get; protected set;}
    public PlantOrganType OrganType {get; protected set;}
    public Vector2 relativeDirection {get; protected set;} //The direction of this node relative to its parent node
    public PlantOrgan fatherNode {get; protected set;}
    public List<Twig> twigsList {get; protected set;}
    public Twig fatherTwig {get; protected set;}
    public List<PlantOrgan> spreadOrgans {get; protected set;}
    public bool isPlanted = false ;
    public bool isWithering = false;
    public bool isGenerating = false;
    public bool isGeneratingFruit = false;
    public int resources { get; protected set;}
    public int layer { get; protected set;}
    //public Vector2 position { get { return transform.position; } }
    public Lattice lattice { get; }
/*
    public PlantOrgan(int Layer, int PlantId, PlantType mtype, PlantOrgan FatherNode, Lattice mlattice, Vector2 mrelativeDirection){
        this.Id = _IdCount++;
        this.layer = Layer;
        this.plantId = PlantId;
        this.resources = resourcesList.Where(p => p.Item1 == this.type).Select( p => p.Item2).ToArray()[0];
        this.fatherNode = FatherNode;
        this.atLattice = mlattice;
        this.relativeDirection = mrelativeDirection;
        this.type = mtype;
    }//Don't use constructed function when Classes is inheriting from base classes
*/  public virtual void InitMe(PlantOrgan copyMe){
        copyMe.InitMe(copyMe);
        this.layer = copyMe.layer;
        this.plant = copyMe.plant;
        this.plant.plantOrgans.Add(this);
        this.fatherNode = copyMe.fatherNode;
        this.atLattice = copyMe.atLattice;
        this.atLattice.plantOrgans.Add(this);
        this.relativeDirection = copyMe.relativeDirection;
        this.type = copyMe.type;
        this.fatherTwig = copyMe.fatherTwig;
    }
    public virtual void InitMe(int Layer, Plant Plant, PlantType mtype, PlantOrgan FatherNode, Lattice mlattice, Vector2 mrelativeDirection,Twig mfatherTwig){  
        mfatherTwig?.InitMe(FatherNode);
        this.layer = Layer;
        this.plant = Plant;
        this.plant.plantOrgans.Add(this);
        this.fatherNode = FatherNode;
        this.atLattice = mlattice;
        this.atLattice.plantOrgans.Add(this);
        this.relativeDirection = mrelativeDirection;
        this.type = mtype;
        this.fatherTwig = mfatherTwig;
    }
    public virtual void ClearMe(){
        this.twigsList.ForEach(p => Destroy(p.gameObject));
        this.twigsList.ForEach(p => p.InitMe(null));
        this.twigsList.Clear();
        Destroy(this.gameObject);
        plant.plantOrgans.Remove(this);
        atLattice.plantOrgans.Remove(this);
        fatherNode.spreadOrgans.Remove(this);
    }
    public void ChangeStatePic(PlantType type){
        statePicRenderer.sprite = statePics[(int)type];
    }
    public void ChangeStatePic(){
        statePicRenderer.sprite = statePics[(int)this.type];
    }
    public virtual List<PlantOrgan> GenerateFruits(){
        List<PlantOrgan> generateList = new List<PlantOrgan>();
        return generateList;
    }
    public List<PlantOrgan> SpreadPlant(){
        List<PlantOrgan> generateList = new List<PlantOrgan>();
        List<int> probilityList = resourcesList.Where(p => p.Item1 == type).Select(p => p.Item2).ToList();
        int totalProbility = probilityList.Sum();
        int randomResult = UnityEngine.Random.Range(1000000, 10000000) % totalProbility + 1; 
        for(int i = 0; randomResult > 0 && i <= 4; i++){
            randomResult -= probilityList[i];
            if(randomResult <= 0){
                switch(i){
                    case 0:
                        break;
                    case 1:
                        Branch tmp1 = new Branch();
                        Twig twigtmp1 = new Twig();
                        tmp1.InitMe(layer, plant, type, this, atLattice, Lattice.directionList.
                        Where(p => p != new Vector2(-relativeDirection.x, -relativeDirection.y)).OrderBy(p => Guid.NewGuid()).Take(1).ToList()[0], twigtmp1);
                        generateList.Add(tmp1);
                        twigsList.Add(twigtmp1);
                        break;
                    case 2:
                        foreach(var item in Lattice.directionList.
                        Where(p => p != new Vector2(-relativeDirection.x, -relativeDirection.y)).OrderBy(p => Guid.NewGuid()).Take(2).ToList()){
                            Branch tmp2 = new Branch();
                            Twig twigtmp2 = new Twig();
                            tmp2.InitMe(layer, plant, type, this, atLattice, item, twigtmp2);
                            generateList.Add(tmp2);
                            twigsList.Add(twigtmp2);
                        }
                        break;
                    case 3:
                        foreach(var item in Lattice.directionList.
                        Where(p => p != new Vector2(-relativeDirection.x, -relativeDirection.y)).Take(3).ToList()){
                            Branch tmp2 = new Branch();
                            Twig twigtmp2 = new Twig();
                            tmp2.InitMe(layer, plant, type, this, atLattice, item, twigtmp2);
                            twigsList.Add(twigtmp2);
                            generateList.Add(tmp2);
                        }
                        break;
                    case 4:
                        foreach(var item in Lattice.directionList){
                            Branch tmp2 = new Branch();
                            Twig twigtmp2 = new Twig();
                            tmp2.InitMe(layer, plant, type, this, atLattice, item, twigtmp2);
                            generateList.Add(tmp2);
                            twigsList.Add(twigtmp2);
                        }
                        break;
                }
            }
        }
        this.isGenerating = true;
        return generateList;
    }
    public void Harvest()
    {
        //harvest plant
        if(isPlanted == false){
            Debug.LogError("harvest not planted");
            return;
        }
        if (layer == 1)
        {
            this.Wither();
            ClearMe();
        }
        if (layer == 2)
        {
            this.Fall();
            ClearMe();
        }
    }
    public void Wither()//organ and all son wither
    {
        if(isPlanted == false)
            return;
        this.isWithering= true;
        ChangeStatePic();
        if (twigsList.Count != 0)
        {
            foreach (var a in twigsList)
                a.Wither();
        }
        if (spreadOrgans.Count()!= 0)
        {
            foreach (PlantOrgan organ in spreadOrgans)
            {
                organ.Wither();
            }
        }
    }
    public void Fall()//organ and all son wither
    {
        if(isPlanted == false)
            return;
        lattice.ground.TurnPlain();
        if (spreadOrgans.Count()!= 0)
        {
            foreach (PlantOrgan organ in spreadOrgans)
            {
                organ.Fall();
            }
        }
        ClearMe();
    }
    void Start() {
        statePicRenderer = GetComponent<SpriteRenderer>();
        this.Id = _IdCount++;
        this.resources = resourcesList.Where(p => p.Item1 == this.type).Select( p => p.Item2).ToArray()[0];
        if(isPlanted == true && isGenerating == false){
            this.spreadOrgans =SpreadPlant();
            isGenerating = true;
        }
    }
}

