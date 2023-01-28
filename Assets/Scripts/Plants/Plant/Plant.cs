using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{

    private static int _IdCount = 0;
    public List<PlantOrgan> plantOrgans = new List<PlantOrgan>();
    public PlantType type { get; private set;}
    public int Id { get; private set; }
    public void InitMe(PlantType mtype){
        this.type = mtype;
    }
    private void Start() {
        this.Id = _IdCount++;
    }
}
