using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Root : PlantOrgan
{
    public new static List<(PlantType, List<float>)> spreadProbability = new List<(PlantType, List<float>)>{};
}
