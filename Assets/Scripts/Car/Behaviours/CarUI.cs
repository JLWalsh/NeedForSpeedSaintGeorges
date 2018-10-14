using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarUI : MonoBehaviour {

    public Texture2D rpmOverlay;
    public Image rpmOverlayImage;

    public float relativeRpm;

    private void OnGUI() {
        relativeRpm = GUI.HorizontalSlider(new Rect(0, 0, 100, 20), relativeRpm, 0f, 1.0f);
    }

    private void Update() {
        rpmOverlayImage.sprite = GenerateRpmOverlayForRpm();
        rpmOverlayImage.rectTransform.sizeDelta = new Vector2(relativeRpm * 100f, 100f);
    }

    private Sprite GenerateRpmOverlayForRpm() {
        float width = Mathf.Max(1f, rpmOverlay.width * relativeRpm); // Image must have a width > 0

        Rect cropRect = new Rect(0, 0, width, rpmOverlay.height);

        return Sprite.Create(rpmOverlay, cropRect, Vector3.zero);
    }
}
