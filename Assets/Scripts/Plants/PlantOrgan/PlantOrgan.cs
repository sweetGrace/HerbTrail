using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlantOrgan : MonoBehaviour
{
    // Start is called before the first frame update
    private static int _IdCount = 0;
    public static List<(PlantType, List<float>)> spreadProbability;
    public int Id { get; private set; }
    public int plantId { get; private set; }
    public PlantType type {get; private set;}
    public PlantOrgan fatherNode {get; private set;}
    public List<PlantOrgan> spreadOrgans;
    public List<Vector2> twigDirections;
    public bool isPlanted = false;
    public bool isWithering = true;
    public int resources { get; private set;}
    public int layer { get; private set;}
    public Vector2 position { get { return transform.position; } }

    public PlantOrgan(){
        this.Id = _IdCount++ ;
    }

    protected (List<Vector2>, List<PlantOrgan>) _SpreadPlant();

}
