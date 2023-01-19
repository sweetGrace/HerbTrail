using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IRound
{
    void OnRoundBegin(RoundManager round);
    void OnRoundEnd(RoundManager round);
}
