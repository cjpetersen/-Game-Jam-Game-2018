using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightState : MonoBehaviour {

    public LayerMask visibleMask;

    public static LightState Instance;

    private Dictionary<GameObject, List<LightHideChance>> _objectLightSources;

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
            _objectLightSources = new Dictionary<GameObject, List<LightHideChance>>();
        }
    }

    public void ReportObjectLit(LightSource source, GameObject litObject, float hideChance)
    {
        List<LightHideChance> objectLightList;
        if (!_objectLightSources.TryGetValue(litObject, out objectLightList))
        {
            objectLightList = new List<LightHideChance>();
            _objectLightSources.Add(litObject, objectLightList);
        }

        objectLightList.Add(new LightHideChance { HideChance = hideChance, LightSource = source });
    }

    public float GetHideProbability(GameObject gameObject)
    {
        float hideProbability = 1;
        List<LightHideChance> lightHideChances;
        if (_objectLightSources.TryGetValue(gameObject, out lightHideChances))
        {
            foreach( var lightHideChance in lightHideChances)
            {
                hideProbability *= lightHideChance.HideChance;
            }
        }
        return hideProbability;
    }

    private void LateUpdate()
    {
        _objectLightSources.Clear();
    }

    private class LightHideChance
    {
        public LightSource LightSource { get; set; }
        public float HideChance { get; set; }
    }
}
