using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CarSpawner : MonoBehaviour {

    private void Awake() {
        CarSpawner[] spawners = FindObjectsOfType<CarSpawner>();

        if(spawners.Length > 1) {
            Destroy(this);
        } else {
            DontDestroyOnLoad(this);
        }
    }

    public void Spawn(GameObject prefab) {
        Instantiate(prefab, transform.position, transform.rotation);
    }
}
