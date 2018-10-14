using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CarSelector : MonoBehaviour {

    public enum CarSpecAmount {
        BAD,
        ACCEPTABLE,
        GOOD
    }

    public SelectableCar[] cars;
    public string drivingSceneName;
    public float turnSpeed;

    private int selectedCar = 1;
    private GameObject displayedCar = null;

    private CarSpawner carSpawner;

    private void Awake() {
        carSpawner = FindObjectOfType<CarSpawner>();
    }

    private void Start() {
        InstantiateCurrentCar();
    }

    void Update () {
        displayedCar.transform.Rotate(Vector3.up * turnSpeed);
	}

    public void ViewNextCar() {
        selectedCar++;

        if (selectedCar >= cars.Length)
            selectedCar = 0;

        InstantiateCurrentCar();
    }

    public void ViewPreviousCar() {
        selectedCar--;

        if (selectedCar < 0)
            selectedCar = cars.Length - 1;

        InstantiateCurrentCar();
    }

    public SelectableCar GetCurrentCar() {
        return cars[selectedCar];
    }

    public void SelectCar() {
        carSpawner.DriveWithCar(drivingSceneName, GetCurrentCar().driveablePrefab);
    }

    private void InstantiateCurrentCar() {
        if (displayedCar != null)
            Destroy(displayedCar);

        displayedCar = Instantiate(GetCurrentCar().displayPrefab, transform.position, transform.rotation);
    }

    [System.Serializable]
    public struct SelectableCar {
        public string displayName;

        public GameObject driveablePrefab;
        public GameObject displayPrefab;

        public CarSpecAmount handling;
        public CarSpecAmount topSpeed;
        public CarSpecAmount acceleration;
    }
}
