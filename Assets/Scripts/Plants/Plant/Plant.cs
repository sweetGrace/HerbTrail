using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{

    private static int _IdCount = 0;
    public List<PlantOrgan> plant;
    public PlantType type { get; private set;}
    public int Id { get; private set; }
    public Plant() {
        this.Id = _IdCount++;
    }

}
