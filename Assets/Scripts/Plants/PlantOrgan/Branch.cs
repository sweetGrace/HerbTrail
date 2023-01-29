using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class Branch : PlantOrgan
{
    public new static List<(PlantType, int)> resourcesList = new List<(PlantType, int)> {
        (PlantType.platformTree, 0),
        (PlantType.obstacleThorn, 0),
        (PlantType.harvestBush, 0),
        (PlantType.harvestVine, 0)
    };
    public new static List<(PlantType, List<int>)> spreadProbability = new List<(PlantType, List<int>)> {
        (PlantType.platformTree, new List<int>{1, 1, 1, 4, 10}),
        (PlantType.obstacleThorn, new List<int>{2, 2, 2, 4, 2}),
        (PlantType.harvestBush, new List<int>{2, 2, 3, 3, 2}),
        (PlantType.harvestVine, new List<int>{3, 1, 2, 4, 4})
    };
    public new PlantOrganType OrganType {get; private set;} = PlantOrganType.branch;
    /*
    public Branch(int Layer, int PlantId, PlantType mtype, PlantOrgan FatherNode, Lattice mlattice, Vector2 mrelativeDirection): 
    base(Layer, PlantId, mtype, FatherNode, mlattice, mrelativeDirection){}*/
    public override void InitMe(PlantOrgan copyMe){
        copyMe.fatherTwig?.InitMe(copyMe.fatherNode);
        this.layer = copyMe.layer;
        this.plant = copyMe.plant;
        if(!this.plant.plantOrgans.Contains(this))
            this.plant.plantOrgans.Add(this);
        this.fatherNode = copyMe.fatherNode;
        this.atLattice = copyMe.atLattice;
        if(!this.atLattice.plantOrgans.Contains(this))
            this.atLattice.plantOrgans.Add(this);
        this.relativeDirection = copyMe.relativeDirection;
        this.type = copyMe.type;
        this.fatherTwig = copyMe.fatherTwig;
    }
    public override void InitMe(int Layer, Plant Plant, PlantType mtype, PlantOrgan FatherNode, Lattice mlattice, Vector2 mrelativeDirection, Twig mfatherTwig){
        mfatherTwig?.InitMe(FatherNode);
        this.layer = Layer;
        this.plant = Plant;
        if(!this.plant.plantOrgans.Contains(this))
            this.plant.plantOrgans.Add(this);
        this.fatherNode = FatherNode;
        this.atLattice = mlattice;
        if(!this.atLattice.plantOrgans.Contains(this))
            this.atLattice.plantOrgans.Add(this);
        this.relativeDirection = mrelativeDirection;
        this.type = mtype;
        this.fatherTwig = mfatherTwig;
    }
    public override List<PlantOrgan> GenerateFruits(){
        List<PlantOrgan> generateList = new List<PlantOrgan>();
        Fruit tmp = new Fruit();
        switch(this.type){
            case PlantType.platformTree:
                tmp.InitMe(layer, plant, type, this, atLattice, Vector2.zero, new Twig());
                generateList.Add(tmp);;
                this.isGeneratingFruit = true;
                break;
            case PlantType.obstacleThorn:
                this.isGeneratingFruit = true;
                break;
            case PlantType.harvestBush:
                this.isGeneratingFruit = true;
                break;
            case PlantType.harvestVine:
                if(spreadOrgans.Where(p => p.OrganType == PlantOrganType.branch).ToList().Count == 3){
                    tmp.InitMe(layer, plant, type, this, atLattice, Vector2.zero, new Twig());
                    generateList.Add(tmp);
                    this.isGeneratingFruit = true;
                }
                break;
        }
        return generateList;
    }
    private void Start() {
        spreadOrgans = new List<PlantOrgan>();
        twigsList = new List<Twig>();
        this.Id = _IdCount++;
        this.resources = resourcesList.Where(p => p.Item1 == this.type).Select( p => p.Item2).ToArray()[0];
        statePicRenderer = this.gameObject.GetComponent<SpriteRenderer>();
        if(isPlanted == true && isGenerating == false){
            this.spreadOrgans =SpreadPlant();
            isGenerating = true;
        }
    }
}
