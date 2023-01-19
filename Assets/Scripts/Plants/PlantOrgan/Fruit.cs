using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : PlantOrgan
{
    public new static PlantOrganType OrganType {get; private set;} = PlantOrganType.fruit;
    
    public static int[] matureNutrient {get; private set;} = new int[(int)PlantType.length]{100, 100, 100, 100};//correspondence with PlantType
    public static int[] rotNutrient {get; private set;} = new int[(int)PlantType.length]{200, 200, 200, 200};//correspondence with PlantType
    public static int[] baseGrowNutrient {get; private set;} = new int[(int)PlantType.length]{25, 25, 25, 25};//correspondence with PlantType
    public int _currentNutrient {get; private set;}
    public int periodCount {get; private set;}
    
    public Fruit(int Layer, int PlantId, PlantOrgan FatherNode): base(Layer, PlantId, FatherNode){
        this._currentNutrient = 0;
        this.periodCount = 0;
    }
    public int GetGrowNutrient(int fertility){
        return Fruit.baseGrowNutrient[(int)this.type]*(2 + fertility/1 + fertility);
    }
    public void PeriodUpdate(){
        if(_currentNutrient < 2)
            _currentNutrient++;
        else
            _currentNutrient = 0;
    }
    public void GrowingUpdate(int fertility){
        
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
