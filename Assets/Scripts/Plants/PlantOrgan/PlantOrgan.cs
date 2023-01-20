using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
public abstract class PlantOrgan : MonoBehaviour, IRound
{

    private static int _IdCount = 0;
    public static List<(PlantType, List<int>)> spreadProbability = new List<(PlantType, List<int>)> {
        (PlantType.harvestBush, new List<int>{}),
        (PlantType.harvestVine, new List<int>{}),
        (PlantType.obstacleThorn, new List<int>{}),
        (PlantType.platformTree, new List<int>{})
    };//corresponding with SpreadType
    public static List<(PlantType, int)> resourcesList = new List<(PlantType, int)> {
        (PlantType.platformTree, 0),
        (PlantType.obstacleThorn, 0),
        (PlantType.harvestBush, 0),
        (PlantType.harvestVine, 0)
    };
    public Lattice atLattice{ get; private set; }
    public int Id { get; private set; }
    public int plantId { get; private set; }
    public PlantType type {get; private set;}
    public PlantOrganType OrganType {get; private set;}
    public Vector2 relativeDirection {get; private set;} //The direction of this node relative to its parent node
    public PlantOrgan fatherNode {get; private set;}
    public List<PlantOrgan> spreadOrgans;
    public bool isPlanted = false ;
    public bool isWithering = true;
    public bool isGenerating = false;
    public bool isGeneratingFruit = false;
    public int resources { get; private set;}
    public int layer { get; private set;}
    //public Vector2 position { get { return transform.position; } }
    public Lattice lattice { get; }

    public PlantOrgan(int Layer, int PlantId, PlantType mtype, PlantOrgan FatherNode, Lattice mlattice, Vector2 mrelativeDirection){
        this.Id = _IdCount++;
        this.layer = Layer;
        this.plantId = PlantId;
        this.resources = resourcesList.Where(p => p.Item1 == this.type).Select( p => p.Item2).ToArray()[0];
        this.fatherNode = FatherNode;
        this.atLattice = mlattice;
        this.relativeDirection = mrelativeDirection;
        this.type = mtype;
    }
    public virtual List<PlantOrgan> GenerateFruits(){
        List<PlantOrgan> generateList = new List<PlantOrgan>();
        return generateList;
    }
    protected List<PlantOrgan> _SpreadPlant(){
        List<PlantOrgan> generateList = new List<PlantOrgan>();
        List<int> probilityList = resourcesList.Where(p => p.Item1 == type).Select(p => p.Item2).ToList();
        int totalProbility = probilityList.Sum();
        int randomResult = UnityEngine.Random.Range(1, 10000000) % totalProbility + 1; 
        for(int i = 0; randomResult > 0 && i <= 4; i++){
            randomResult -= probilityList[i];
            if(randomResult <= 0){
                switch(i){
                    case 0:
                        break;
                    case 1:
                        generateList.Add(new Branch(layer, plantId, type, this, atLattice, Lattice.directionList.
                        Where(p => p != new Vector2(-relativeDirection.x, -relativeDirection.y)).OrderBy(p => Guid.NewGuid()).Take(1).ToList()[0]));

                        break;
                    case 2:
                        foreach(var item in Lattice.directionList.
                        Where(p => p != new Vector2(-relativeDirection.x, -relativeDirection.y)).OrderBy(p => Guid.NewGuid()).Take(2).ToList()){
                            generateList.Add(new Branch(layer, plantId, type, this, atLattice, item));
                        }
                        break;
                    case 3:
                        foreach(var item in Lattice.directionList.
                        Where(p => p != new Vector2(-relativeDirection.x, -relativeDirection.y)).Take(3).ToList()){
                            generateList.Add(new Branch(layer, plantId, type, this, atLattice, item));
                        }
                        break;
                    case 4:
                        foreach(var item in Lattice.directionList){
                            generateList.Add(new Branch(layer, plantId, type, this, atLattice, item));
                        }
                        break;
                }
            }
        }
        this.isGenerating = true;
        return generateList;
    }
#region IRound

    public virtual void OnRoundBegin(RoundManager round) {
        if(isPlanted == true && isGenerating == false){
            this.spreadOrgans = _SpreadPlant();
            isGenerating = true;
        } 
    }
    public virtual void OnRoundEnd(RoundManager round) {
        if(isPlanted == true){
            List<PlantOrgan> spreadList = spreadOrgans.Where(p => p.isPlanted == false).ToList();
        }
    }
#endregion
    public void Harvest()
    {
        //�ڴ�ɾ��twig
        if (layer == 1)
        {
            this.Wither();
            lattice.plantOrgans.Remove(this);
        }
        if (layer == 2)
        {
            this.Fall();
            lattice.plantOrgans.Remove(this);
        }
    }
    public void Wither()//��ή��ǰֲ�����������ֲ���ή
    {
        this.isWithering= true;
        if (spreadOrgans.Count()!= 0)
        {
            foreach (PlantOrgan organ in spreadOrgans)
            {
                organ.Wither();
            }
        }
    }
    public void Fall()
    {   
        //�ӵڶ������µķ���
        lattice.ground.TurnPlain();
        lattice.plantOrgans.Clear();
        if (spreadOrgans.Count()!= 0)
        {
            foreach (PlantOrgan organ in spreadOrgans)
            {
                organ.Fall();
            }
        }

    }
}

