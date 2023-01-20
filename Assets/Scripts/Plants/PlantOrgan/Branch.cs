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
    public new PlantOrganType OrganType {get; private set;} = PlantOrganType.branch;
    public Branch(int Layer, int PlantId, PlantType mtype, PlantOrgan FatherNode, Lattice mlattice, Vector2 mrelativeDirection): 
    base(Layer, PlantId, mtype, FatherNode, mlattice, mrelativeDirection){}
    public override List<PlantOrgan> GenerateFruits(){
        List<PlantOrgan> generateList = new List<PlantOrgan>();
        switch(this.type){
            case PlantType.platformTree:
                generateList.Add(new Fruit(layer, plantId, type, this, atLattice, Vector2.zero));
                this.isGeneratingFruit = true;
                break;
            case PlantType.obstacleThorn:
                this.isGeneratingFruit = true;
                break;
            case PlantType.harvestBush:
                this.isGeneratingFruit = true;
                break;
            case PlantType.harvestVine:
                if(spreadOrgans.Where( p => p.OrganType == PlantOrganType.branch).ToList().Count == 3){
                    generateList.Add(new Fruit(layer, plantId, type, this, atLattice, Vector2.zero));
                    this.isGeneratingFruit = true;
                }
                break;
        }
        return generateList;
    }

}
