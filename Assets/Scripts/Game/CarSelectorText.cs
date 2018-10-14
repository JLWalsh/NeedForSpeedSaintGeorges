using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CarSelectorText : MonoBehaviour {

    public TextMeshProUGUI carNameText;
    public TextMeshProUGUI carSpecsText;

    private CarSelector carSelector;

    private void Awake() {
        carSelector = FindObjectOfType<CarSelector>();
    }

    private void Update() {
        CarSelector.SelectableCar car = carSelector.GetCurrentCar();

        string carSpecs = "";
        carSpecs += "Handling " + GetTextForCarSpecAmount(car.handling);
        carSpecs += "\nTop Speed " + GetTextForCarSpecAmount(car.topSpeed);
        carSpecs += "\nAcceleration " + GetTextForCarSpecAmount(car.acceleration);

        carSpecsText.text = carSpecs;
        carNameText.text = car.displayName;
    }

    private string GetTextForCarSpecAmount(CarSelector.CarSpecAmount amount) {
        switch(amount) {
            case CarSelector.CarSpecAmount.GOOD:
                return "+++";
            case CarSelector.CarSpecAmount.ACCEPTABLE:
                return "++";
            case CarSelector.CarSpecAmount.BAD:
                return "+";
        }

        return "";
    }
}
