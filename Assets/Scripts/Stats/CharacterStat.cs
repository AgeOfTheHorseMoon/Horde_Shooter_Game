using System;
using System.Collections.Generic;

namespace Coruk.CharacterStats
{
    /// Code usage from: https://youtu.be/uvOSx5FzDnU?si=rCdt1YvaykAN13dF&t=497
    [Serializable]
    public class CharacterStat
    {
        public float BaseValue;

        public float Value
        {
            get
            {
                if (isDirty || BaseValue != _lastBaseValue)
                {
                    _lastBaseValue = BaseValue;
                    _value = CalculateFinalValue();
                    isDirty = false;
                }
                return _value;
            }
        }

        protected bool isDirty = true; // if the final value needs recalculating
        protected float _value; // Hold most recent calculation
        protected float _lastBaseValue = float.MinValue;

        protected readonly List<StatModifier> _statModifiers;
        public readonly IReadOnlyCollection<StatModifier> StatModifiers;

        public CharacterStat()
        {
            _statModifiers = new List<StatModifier>();
            StatModifiers = _statModifiers.AsReadOnly();
        }

        public CharacterStat(float baseValue) : this()
        {
            BaseValue = baseValue;
        }

        public virtual void AddModifier(StatModifier modifier)
        {
            isDirty = true; // Values need calculating
            _statModifiers.Add(modifier);
            _statModifiers.Sort(CompareModifierOrder); // Guiding Sort method
        }

        protected virtual int CompareModifierOrder(StatModifier a, StatModifier b)
        {
            if (a.Order < b.Order)
            {
                return -1; // a has priority
            }
            else if (a.Order > b.Order)
            {
                return 1; // b has priority
            }
            else // a.Order == b.Order
            {
                return 0;
            }
        }

        public virtual bool RemoveModifier(StatModifier modifier)
        {
            if (_statModifiers.Remove(modifier))
            {
                isDirty = true;
                return true;
            }
            return false;
        }

        public virtual bool RemoveAllModifiersFromSource(object source)
        {
            bool didRemove = false;

            // Removing reverse eliminates the need of moving items
            for (int i = _statModifiers.Count - 1; i >= 0; i--)
            {
                if (_statModifiers[i].Source == source)
                {
                    isDirty = true;
                    didRemove = true;
                    _statModifiers.RemoveAt(i);
                }
            }

            return didRemove;
        }

        protected virtual float CalculateFinalValue()
        {
            float finalValue = BaseValue;
            float sumPercentageAdditive = 0;

            for (int i = 0; i < _statModifiers.Count; i++)
            {
                StatModifier modifier = _statModifiers[i];

                if (modifier.Type == StatModifierType.Flat)
                {
                    finalValue += modifier.Value;
                }
                else if (modifier.Type == StatModifierType.PercentAdditive)
                {
                    sumPercentageAdditive += modifier.Value;
                    if (i + 1 >= _statModifiers.Count || _statModifiers[i + 1].Type != StatModifierType.PercentAdditive)
                    {
                        finalValue *= 1 + (sumPercentageAdditive / 100); // (%M + %N + %P) * previousValue = result
                        sumPercentageAdditive = 0; // 150 * (200+200+200)  -> 1050% * baseValue => result
                    }
                }
                else if (modifier.Type == StatModifierType.PercentageMultiplicative)
                {
                    finalValue *= 1 + (modifier.Value / 100); // (%M * previous Value) => (%N * newValue) => ... = result
                    // 5 <- base
                    // 5 * %100 => 10 * %200 -> 30...
                }
            }

            // Rounds the float calculation solution so this won't happen 12.00 != 12.01
            return (float)Math.Round(finalValue, 4);
        }
    }
}