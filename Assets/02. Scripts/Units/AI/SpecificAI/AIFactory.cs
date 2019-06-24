using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;
using System.Linq;

public class AIFactory
{
    public static UnitAI GetAI(string name, Unit u)
    {
        // Elephants, Archers, Spearmen
        if (name == "Archers" || name == "Catapult")
        {
            return new DistanceAI(u);
        }
        if (name == "Spearmen" || name == "Elephants")
        {
            return new MeleeAI(u);
        }

        return null;
    }
}
