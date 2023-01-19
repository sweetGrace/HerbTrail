using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Branch : PlantOrgan
{
        public new static List<(PlantType, int)> resourcesList = new List<(PlantType, int)> {
        (PlantType.platformTree, 0),
        (PlantType.obstacleThorn, 0),
        (PlantType.harvestBush, 0),
        (PlantType.harvestVine, 0)
    };
    public new PlantOrganType OrganType {get; private set;} = PlantOrganType.branch;
    public Branch(int Layer, int PlantId, PlantOrgan FatherNode): base(Layer, PlantId, FatherNode){}

}
