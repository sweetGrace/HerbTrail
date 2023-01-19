using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    // Start is called before the first frame update
    public static int maxFertilityDegree { get; private set; } = 2;
    public GroundType type { get; private set; }
    public int fertilityDegree { get; private set; } = 0;
    public bool isPlanted { get; private set; } = false;
    public bool isInShadow { get; private set; }
    public Vector2 position { get { return transform.position; } }

    public void AddFertilityDegree(int dif){
        if(fertilityDegree + dif >= 2)
            fertilityDegree = 2;
        else
            fertilityDegree += dif;
    }

    public void TurnPlain()//��ground���Plain
    {
        type = GroundType.plain;
        fertilityDegree = 0;
        isPlanted = false;
        isInShadow = false;

    }

    public void TurnWater()
    {
        type = GroundType.seawater;
        fertilityDegree = 0;
        isPlanted = false;
        isInShadow = false;
    }
}
