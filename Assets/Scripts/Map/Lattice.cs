using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
public class Lattice : MonoBehaviour
{
    public Ground ground;
    public List<PlantOrgan> plantOrgans { get; private set; }
    public Vector2 position { get; private set; }

    public Lattice(Vector2 mpositon){
        this.position = mpositon;
    }
#region DisplayInfo
    private GroundType _DisplayGroundType(){ return ground.type; }
    private int _DisplayFertility(){ return ground.fertilityDegree; }
    
#endregion
#region RoundEndUpdate
    private void _UpdateFruitState(){
        List<Fruit> fruitList = plantOrgans.OfType<Fruit>().ToList();
        fruitList.ForEach( fruit => fruit.GrowingUpdate(ground.fertilityDegree));
    }
#endregion
}
