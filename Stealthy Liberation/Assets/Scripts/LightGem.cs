using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LightGem : MonoBehaviour {

    public Image canvasImage;
    public Sprite partiallyHiddenSprite;
    public Sprite visibleSprite;

    private float latestHiddenLevel = 1;

    private void Update()
    {
        // get hidden level
        latestHiddenLevel = LightState.Instance.GetHideProbability(gameObject);
    }

    void OnGUI () {
        // set light gem
        if (latestHiddenLevel < .5)
            canvasImage.overrideSprite = visibleSprite;
        else if (latestHiddenLevel < 1)
            canvasImage.overrideSprite = partiallyHiddenSprite;
        else
            canvasImage.overrideSprite = null;
    }
}
