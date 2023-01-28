using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Ground : MonoBehaviour
{
    // Start is called before the first frame update
    public static int maxFertilityDegree { get; private set; } = 2;
    public static int resources;
    public GroundType type { get; set; }
    public Lattice atLattice { get; set; }
    public int fertilityDegree { get; private set; } = 0;
    public bool isPlanted { get; private set; } = false;
    public bool isInShadow { get; private set; } = false;
    //public Vector2 position { get { return transform.position; } }
    public void AddFertilityDegree(int dif){
        if(fertilityDegree + dif >= maxFertilityDegree)
            fertilityDegree = maxFertilityDegree;
        else
            fertilityDegree += dif;
    }

    public void TurnPlain()//turn into plain
    {
        type = GroundType.plain;
        fertilityDegree = 0;
        isPlanted = false;
        isInShadow = false;
        Map.Instance.tilemap.SetTile(new Vector3Int(Convert.ToInt32(atLattice.position.x), Convert.ToInt32(atLattice.position.y), 0), Map.Instance.plainTile);

    }

    public void TurnWater()
    {
        type = GroundType.seawater;
        fertilityDegree = 0;
        isPlanted = false;
        Map.Instance.tilemap.SetTile(new Vector3Int(Convert.ToInt32(atLattice.position.x), Convert.ToInt32(atLattice.position.y), 0), Map.Instance.seawaterTile);
    }
}
