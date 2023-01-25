using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
public class Fruit : PlantOrgan
{
    public new PlantOrganType OrganType {get; private set;} = PlantOrganType.fruit;
    public Sprite[] stateWitheringPics;
    public Sprite[] stateMaturePics;
    public new static List<(PlantType, int)> resourcesList = new List<(PlantType, int)> {
        (PlantType.platformTree, 15),
        (PlantType.obstacleThorn, 0),
        (PlantType.harvestBush, 20),
        (PlantType.harvestVine, 12)
    };
    public new static List<(PlantType, List<int>)> spreadProbability = new List<(PlantType, List<int>)> {
        (PlantType.platformTree, new List<int>{0, 0, 0, 0, 0}),
        (PlantType.obstacleThorn, new List<int>{0, 0, 0, 0, 0}),
        (PlantType.harvestBush, new List<int>{0, 0, 0, 0, 0}),
        (PlantType.harvestVine, new List<int>{0, 0, 0, 0, 0})
    };
    public static int[] matureNutrient {get; private set;} = new int[(int)PlantType.length]{0, 100, 100, 100};//correspondence with PlantType
    public static int[] rotNutrient {get; private set;} = new int[(int)PlantType.length]{200, 200, 200, 200};//correspondence with PlantType
    public static int[] baseGrowNutrient {get; private set;} = new int[(int)PlantType.length]{0, 25, 25, 25};//correspondence with PlantType
    public int currentNutrient {get; private set;}
    public int periodCount {get; private set;}
    public int maxPeriodCount {get; private set;}
    /*
    public Fruit(int Layer, int PlantId, PlantType mtype, PlantOrgan FatherNode, Lattice mlattice, Vector2 mrelativeDirection): 
    base(Layer, PlantId, mtype, FatherNode, mlattice, mrelativeDirection){
        this._currentNutrient = 0;
        this.periodCount = 0;
    }*/
    public void ChangeStatePic(PlantType type, Sprite[] pics){
        statePicRenderer.sprite = pics[(int)type];
    }
    public void ChangeStatePic(Sprite[] pics){
        statePicRenderer.sprite = pics[(int)this.type];
    }
    public override void InitMe(PlantOrgan copyMe){
        copyMe.InitMe(copyMe);
        this.layer = copyMe.layer;
        this.plant = copyMe.plant;
        this.plant.plantOrgans.Add(this);
        this.fatherNode = copyMe.fatherNode;
        this.atLattice = copyMe.atLattice;
        this.atLattice.plantOrgans.Add(this);
        this.relativeDirection = copyMe.relativeDirection;
        this.type = copyMe.type;
        this.fatherTwig = copyMe.fatherTwig;
    }
    public override void InitMe(int Layer, Plant Plant, PlantType mtype, PlantOrgan FatherNode, Lattice mlattice, Vector2 mrelativeDirection, Twig mfatherTwig){
        mfatherTwig?.InitMe(FatherNode);
        this.layer = Layer;
        this.plant = Plant;
        this.plant.plantOrgans.Add(this);
        this.fatherNode = FatherNode;
        this.atLattice = mlattice;
        this.atLattice.plantOrgans.Add(this);
        this.relativeDirection = mrelativeDirection;
        this.type = mtype;
        this.fatherTwig = mfatherTwig;
    }
    public bool SatisfyGrowingConditions(){
        bool growingFlag = true;
        if(this.type == PlantType.harvestBush && 
        Map.Instance.plantSet.Where(p => p==this.plant).ToList()[0].plantOrgans.Where(p => p.OrganType != PlantOrganType.fruit).Count() <= 10)//fathernode must be root
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
            if(currentNutrient < Fruit.matureNutrient[(int)type] && currentNutrient + GetGrowNutrient(fertility) >= Fruit.matureNutrient[(int)type]){
                PeriodUpdate();
                this.currentNutrient += GetGrowNutrient(fertility);
            }else if(currentNutrient < Fruit.rotNutrient[(int)type] && currentNutrient + GetGrowNutrient(fertility) >= Fruit.rotNutrient[(int)type]){
                PeriodUpdate();
                this.currentNutrient = 0;
            }else{
                this.currentNutrient += GetGrowNutrient(fertility);
            }
        }
    }
    new public void Wither()//organ and all son wither
    {
        if(isPlanted == false)
            return;
        this.isWithering = true;
        ChangeStatePic(stateWitheringPics);
        if (twigsList.Count != 0)
        {
            foreach (var a in twigsList)
                a.Wither();
        }
        if (spreadOrgans.Count() != 0)
        {
            foreach (PlantOrgan organ in spreadOrgans)
            {
                organ.Wither();
            }
        }
    }
    new public void Harvest() {
        if(isPlanted == false)
            return;
        if(periodCount >= 1)
            PlayerInfo.Instance.AddResources(Convert.ToInt32(resourcesList.Where( p => p.Item1 == this.type).Select( p => p.Item2).ToArray()[0]/2));
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
    private void Start() {
        this.currentNutrient = 0;
        this.periodCount = 0;
        this.resources = resourcesList.Where(p => p.Item1 == this.type).Select( p => p.Item2).ToArray()[0];
        this.Id = _IdCount++;
    }
}
