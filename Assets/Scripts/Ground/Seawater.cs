using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seawater : Ground
{
    public new static int resources = 0;//should not be used
    public Seawater(Lattice mlattice) :base(GroundType.seawater, mlattice){   }
}
