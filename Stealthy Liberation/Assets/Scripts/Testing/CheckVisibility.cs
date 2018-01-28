using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Testing
{
    public class CheckVisibility : MonoBehaviour
    {
        public GameObject targetToCheck;
        public float pollFrequency = .5f;

        private float pollTimer = 0;

        void Update()
        {
            if (!targetToCheck)
                return;

            if (pollTimer < pollFrequency)
                pollTimer += Time.deltaTime;
            else
            {
                pollTimer = 0;
                Debug.Log("Hide probability for " + targetToCheck.name + ": " + LightState.Instance.GetHideProbability(targetToCheck));
            }
        }
    }
}