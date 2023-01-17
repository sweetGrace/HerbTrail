using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public List<Plant> plantSet;
    public Lattice[] latticeMap;
    private void _SpreadSea();
    private void _ClearWithering();
    private void _HarvestPlant(Lattice lattice); 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
