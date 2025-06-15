using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "EffectLib", menuName = "Effect/EffectLib")]

public class EffectsLibSO : ScriptableObject
{
    [SerializeField]
    private List<EffectData> effects; 

    private Dictionary<string, EffectData> effectDictionary; // Dictionary for quick lookup by name.

    private void OnEnable()
    {

        effectDictionary = new Dictionary<string, EffectData>();
        if (effects == null)return;
        foreach (var effect in effects)
        {
            if (!effectDictionary.ContainsKey(effect.EffectName))
            {
                effectDictionary.Add(effect.EffectName, effect);
            }

        }
    }


    public EffectData GetEffect(string effectName)
    {
        if (effectDictionary.TryGetValue(effectName, out var effectData))
        {
            return effectData;
        }

        Debug.LogError($"Effect '{effectName}' not found in library.");
        return null;
    }
}
