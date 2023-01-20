using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class Fruit : PlantOrgan
{
    public new PlantOrganType OrganType {get; private set;} = PlantOrganType.fruit;
    
    public new static List<(PlantType, int)> resourcesList = new List<(PlantType, int)> {
        (PlantType.platformTree, 0),
        (PlantType.obstacleThorn, 0),
        (PlantType.harvestBush, 0),
        (PlantType.harvestVine, 0)
    };
    public static int[] matureNutrient {get; private set;} = new int[(int)PlantType.length]{0, 100, 100, 100};//correspondence with PlantType
    public static int[] rotNutrient {get; private set;} = new int[(int)PlantType.length]{200, 200, 200, 200};//correspondence with PlantType
    public static int[] baseGrowNutrient {get; private set;} = new int[(int)PlantType.length]{0, 25, 25, 25};//correspondence with PlantType
    public int _currentNutrient {get; private set;}
    public int periodCount {get; private set;}
    public int maxPeriodCount {get; private set;}
    public Fruit(int Layer, int PlantId, PlantType mtype, PlantOrgan FatherNode, Lattice mlattice, Vector2 mrelativeDirection): 
    base(Layer, PlantId, mtype, FatherNode, mlattice, mrelativeDirection){
        this._currentNutrient = 0;
        this.periodCount = 0;
    }
    public bool SatisfyGrowingConditions(){
        bool growingFlag = true;
        if(this.type == PlantType.harvestBush && 
        Map.Instance.plantSet.Where(p => p.Id==this.plantId).ToList()[0].plant.Where(p => p.OrganType != PlantOrganType.fruit).Count() <= 10)//fathernode must be root
            growingFlag = false;
        return growingFlag;
    }
    public override List<PlantOrgan> GenerateFruits(){
        List<PlantOrgan> generateList = new List<PlantOrgan>();
        this.isGeneratingFruit = true;
        return generateList;
    }
    public int GetGrowNutrient(int fertility){
        return Fruit.baseGrowNutrient[(int)this.type]*(2 + fertility/1 + fertility);
    }
    public void PeriodUpdate(){
        if(periodCount < maxPeriodCount)
            periodCount++;
        else
            periodCount = 0;
    }
    public void GrowingUpdate(int fertility){
        if(SatisfyGrowingConditions()){
            if(_currentNutrient < Fruit.matureNutrient[(int)type] && _currentNutrient + GetGrowNutrient(fertility) >= Fruit.matureNutrient[(int)type]){
                PeriodUpdate();
                this._currentNutrient += GetGrowNutrient(fertility);
            }else if(_currentNutrient < Fruit.rotNutrient[(int)type] && _currentNutrient + GetGrowNutrient(fertility) >= Fruit.rotNutrient[(int)type]){
                PeriodUpdate();
                this._currentNutrient = 0;
            }else{
                this._currentNutrient += GetGrowNutrient(fertility);
            }
        }
        
        
    }
    new public void Harvest() {
        if(periodCount >= 1)
            PlayerInfo.Instance.AddResources(resourcesList.Where( p => p.Item1 == this.type).Select( p => p.Item2).ToArray()[0]);

    }
#region IRound
/*
    public override void OnRoundBegin(RoundManager round) {
        if(isPlanted == true && isGenerating == false){
            this.spreadOrgans = _SpreadPlant();
            isGenerating = true;
        } 
    } 
*/
#endregion
}
