using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class Root : PlantOrgan
{
    public new PlantOrganType OrganType {get; private set;} = PlantOrganType.root;
    public new static List<(PlantType, int)> resourcesList = new List<(PlantType, int)> {
        (PlantType.platformTree, 15),
        (PlantType.obstacleThorn, 0),
        (PlantType.harvestBush, 10),
        (PlantType.harvestVine, 10)
    };
    public new static List<(PlantType, List<int>)> spreadProbability = new List<(PlantType, List<int>)> {
        (PlantType.platformTree, new List<int>{0, 1, 1, 3, 9}),
        (PlantType.obstacleThorn, new List<int>{0, 1, 1, 4, 2}),
        (PlantType.harvestBush, new List<int>{0, 2, 3, 3, 2}),
        (PlantType.harvestVine, new List<int>{0, 1, 2, 4, 4})
    };
    /*
    public Root(int Layer, int PlantId, PlantType mtype, Lattice mlattice, Vector2 mrelativeDirection): 
    base(Layer, PlantId, mtype, null, mlattice, mrelativeDirection){}*/
    public override void InitMe(PlantOrgan copyMe){
        copyMe.InitMe(copyMe);
        this.layer = copyMe.layer;
        this.plant = copyMe.plant;
        this.plant.plantOrgans.Add(this);
        this.fatherNode = copyMe.fatherNode;
        this.atLattice = copyMe.atLattice;
        this.relativeDirection = copyMe.relativeDirection;
        this.type = copyMe.type;
        this.fatherTwig = copyMe.fatherTwig;
    }
    public override void InitMe(int Layer, Plant Plant, PlantType mtype, PlantOrgan FatherNode, Lattice mlattice, Vector2 mrelativeDirection, Twig mfatherTwig){
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
    public override List<PlantOrgan> GenerateFruits(){
        List<PlantOrgan> generateList = new List<PlantOrgan>();
        Fruit tmp = new Fruit();
        switch(this.type){
            case PlantType.platformTree:
                tmp.InitMe(layer, plant, type, this, atLattice, Vector2.zero, new Twig());
                generateList.Add(tmp);
                this.isGeneratingFruit = true;
                break;
            case PlantType.obstacleThorn:
                this.isGeneratingFruit = true;
                break;
            case PlantType.harvestBush:
                tmp.InitMe(layer, plant, type, this, atLattice, Vector2.zero, new Twig());
                generateList.Add(tmp);
                this.isGeneratingFruit = true;
                break;
            case PlantType.harvestVine:
                if(spreadOrgans.Where( p => p.OrganType == PlantOrganType.branch).ToList().Count == 4){
                    tmp.InitMe(layer, plant, type, this, atLattice, Vector2.zero, new Twig());
                    generateList.Add(tmp);
                    this.isGeneratingFruit = true;
                }
                break;
        }
        
        return generateList;
    }
    private void Start() {
        this.Id = _IdCount++;
        this.resources = resourcesList.Where(p => p.Item1 == this.type).Select( p => p.Item2).ToArray()[0];
    }
}
