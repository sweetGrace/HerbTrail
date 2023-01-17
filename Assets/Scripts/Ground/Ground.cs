using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    // Start is called before the first frame update
    public GroundType type { get; private set; }
    public int fertilityDegree { get; private set; }
    public bool isPlanted { get; private set; }
    public bool isInShadow { get; private set; }
    public Vector2 position { get { return transform.position; } }

}
