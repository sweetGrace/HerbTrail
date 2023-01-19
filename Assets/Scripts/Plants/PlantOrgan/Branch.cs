using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Branch : PlantOrgan
{
    public new static PlantOrganType OrganType {get; private set;} = PlantOrganType.branch;
    public Branch(int Layer, int PlantId, PlantOrgan FatherNode): base(Layer, PlantId, FatherNode){}

}
