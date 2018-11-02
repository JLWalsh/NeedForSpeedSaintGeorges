using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CarUI : MonoBehaviour {

    public Texture2D rpmOverlay;
    public Image rpmOverlayImage;

    public TextMeshProUGUI speedText;
    public TextMeshProUGUI rpmText;
    public TextMeshProUGUI gearText;
    public TextMeshProUGUI nitroRemainingText;
    public TextMeshProUGUI nitroReloadText;

    private Vitesse vitesse;
    private Engine engine;
    private Transmission transmission;
    private Rigidbody targetRigidbody;

    private void Update() {
        if (!engine || !transmission || !targetRigidbody)
            return;

        UpdateRpmImage();
        UpdateText();
    }

    public void RenderFor(GameObject target) {
        engine = target.GetComponent<CombustionEngine>();
        transmission = target.GetComponent<Transmission>();
        targetRigidbody = target.GetComponent<Rigidbody>();
        vitesse = target.GetComponent<Vitesse>();
    }

    private void UpdateText() {
        speedText.text = RenderFloat(targetRigidbody.velocity.magnitude * 3.6f);
        rpmText.text = RenderFloat(engine.rpm);
        gearText.text = GetTransmissionText();
        nitroRemainingText.text = vitesse.NitroRestantes.ToString();
        nitroReloadText.text = "||||||||||||||||".Substring(0, Mathf.FloorToInt(vitesse.ProgressionRechargeNitro * 16));
    }

    private string RenderFloat(float value) {
        return Mathf.FloorToInt(value).ToString();
    }

    private string GetTransmissionText() {
        if (transmission.GetDrive() == Transmission.Drive.FORWARD)
            return (transmission.GetCurrentGear() + 1).ToString();

        if (transmission.GetDrive() == Transmission.Drive.NEUTRAL)
            return "N";

        if (transmission.GetDrive() == Transmission.Drive.REVERSE)
            return "R";

        return "";
    }

    private void UpdateRpmImage() {
        rpmOverlayImage.sprite = GenerateRpmOverlayForRpm();
        rpmOverlayImage.rectTransform.sizeDelta = new Vector2(engine.GetRelativeRpm() * 100f, 100f);
    }

    private Sprite GenerateRpmOverlayForRpm() {
        float width = Mathf.Max(1f, rpmOverlay.width * engine.GetRelativeRpm()); // Image must have a width > 0

        Rect cropRect = new Rect(0, 0, width, rpmOverlay.height);

        return Sprite.Create(rpmOverlay, cropRect, Vector3.zero);
    }
}
