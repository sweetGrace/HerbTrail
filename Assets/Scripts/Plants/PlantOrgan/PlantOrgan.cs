using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public abstract class PlantOrgan : MonoBehaviour
{

    private static int _IdCount = 0;
    public static List<(PlantType, List<float>)> spreadProbability = new List<(PlantType, List<float>)> {
        (PlantType.harvestBush, new List<float>{}),
        (PlantType.harvestVine, new List<float>{}),
        (PlantType.obstacleThorn, new List<float>{}),
        (PlantType.platformTree, new List<float>{})
    };
    public static List<(PlantType, int)> resourcesList = new List<(PlantType, int)> {
        (PlantType.harvestBush, 0),
        (PlantType.harvestVine, 0),
        (PlantType.obstacleThorn, 0),
        (PlantType.platformTree, 0)
    };
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

    public PlantOrgan(int Layer, int PlantId){
        this.Id = _IdCount++;
        this.layer = Layer;
        this.plantId = PlantId;
        this.resources = resourcesList.Where( p => p.Item1 == this.type).Select( p => p.Item2).ToArray()[0];
    }

    protected (List<Vector2>, List<PlantOrgan>) _SpreadPlant();

}
