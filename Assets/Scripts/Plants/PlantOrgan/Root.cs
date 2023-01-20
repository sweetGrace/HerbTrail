using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class Root : PlantOrgan
{
    public new PlantOrganType OrganType {get; private set;} = PlantOrganType.root;
    public new static List<(PlantType, List<float>)> spreadProbability = new List<(PlantType, List<float>)>{};

    public Root(int Layer, int PlantId, PlantType mtype, Lattice mlattice, Vector2 mrelativeDirection): 
    base(Layer, PlantId, mtype, null, mlattice, mrelativeDirection){}
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
                generateList.Add(new Fruit(layer, plantId, type, this, atLattice, Vector2.zero));
                this.isGeneratingFruit = true;
                break;
            case PlantType.harvestVine:
                if(spreadOrgans.Where( p => p.OrganType == PlantOrganType.branch).ToList().Count == 4){
                    generateList.Add(new Fruit(layer, plantId, type, this, atLattice, Vector2.zero));
                    this.isGeneratingFruit = true;
                }
                break;
        }
        
        return generateList;
    }
}
