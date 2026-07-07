using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Spell", menuName = "Scriptable Objects/Spell")]
public abstract class Spell : ScriptableObject
{
    public String spellName;
    [TextArea] public string spellDescription;
    public float baseCost;
    
    //What this spell will do
    public abstract void Cast(Transform castPoint, float powerMultiplier);
}