using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
public class Lattice : MonoBehaviour
{
    public static List<Vector2> directionList { get; private set; }
    = new List<Vector2> { Vector2.up, Vector2.right, Vector2.down, Vector2.left };
    public Ground ground;
    public List<PlantOrgan> plantOrgans { get; private set; }
    public Vector2 position { get; private set; }
    #region DisplayInfo
    private GroundType _DisplayGroundType() { return ground.type; }
    private int _DisplayFertility() { return ground.fertilityDegree; }

    #endregion
    #region RoundEndUpdate
    public void UpdateFruitState()
    {
        List<Fruit> fruitList = plantOrgans.OfType<Fruit>().ToList();
        fruitList.ForEach(fruit => fruit.GrowingUpdate(ground.fertilityDegree));
    }
    #endregion

    public int IsWater()//judge if water, if so return 1, not return 0
    {
        if (ground.type == GroundType.seawater)
            return 1;
        else
            return 0;
    }
    public void Watered()
    { //turn this into water
        if (this.IsWater()==0)
        {
            plantOrgans.ForEach(p => p.ClearMe());
            this.plantOrgans.Clear();
            ground.TurnWater();
        }
    }
    private void Start() {
        this.position = (Vector2)this?.plantOrgans?[0]?.gameObject.transform.position;
    }
}
