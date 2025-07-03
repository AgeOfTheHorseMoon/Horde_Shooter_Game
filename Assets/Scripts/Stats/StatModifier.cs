using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Coruk.CharacterStats
{
    public enum StatModifierType
    {   // You can put priority flexibly when gaps are big between main priorities, thats why its not default 0,1,2,..
        Flat = 100,
        PercentAdditive = 200,
        PercentageMultiplicative = 300
    }

    public class StatModifier
    {
        public readonly float Value;
        public readonly StatModifierType Type;
        public readonly int Order;
        public readonly object Source;

        public StatModifier(float value, StatModifierType type, int order, object source)
        {
            Value = value;
            Type = type;
            Order = order;
            Source = source;
        }

        // 2nd constructor for automatic order assigning based on enum alignment/order
        // Calls the first constructor when only 2 parameters are given etc..
        public StatModifier(float value, StatModifierType type) : this(value, type, (int)type, null) { }
        public StatModifier(float value, StatModifierType type, int order) : this(value, type, order, null) { }
        public StatModifier(float value, StatModifierType type, object source) : this(value, type, (int)type, source) { }
    }
}