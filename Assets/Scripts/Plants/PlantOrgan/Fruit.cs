using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : PlantOrgan
{
    
    public int dayCount;
    public int periodCount;
    private (List<Vector2>, List<PlantOrgan>) _ProduceFruit();
}
