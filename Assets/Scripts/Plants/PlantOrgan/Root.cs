using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Root : PlantOrgan
{
    public new PlantOrganType OrganType {get; private set;} = PlantOrganType.root;
    public new static List<(PlantType, List<float>)> spreadProbability = new List<(PlantType, List<float>)>{};

    public Root(int Layer, int PlantId, PlantOrgan FatherNode): base(Layer, PlantId, FatherNode){}
}
