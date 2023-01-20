using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plain : Ground
{
    public new static int resources = 10;
    public Plain(Lattice mlattice) :base(GroundType.plain, mlattice){   }
}
