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

    public void DriveWithCar(string drivingSceneName, GameObject carPrefab) {
        StartCoroutine(LoadLevelWithCar(drivingSceneName, carPrefab));
    }

    private IEnumerator LoadLevelWithCar(string drivingSceneName, GameObject prefab) {
        Scene currentScene = SceneManager.GetActiveScene();

        AsyncOperation loading = SceneManager.LoadSceneAsync(drivingSceneName, LoadSceneMode.Additive);

        while (!loading.isDone)
            yield return null;

        Scene drivingScene = SceneManager.GetSceneByName(drivingSceneName);
        SceneManager.SetActiveScene(drivingScene);

        AsyncOperation unloading = SceneManager.UnloadSceneAsync(currentScene);
        while (!unloading.isDone)
            yield return null;

        Instantiate(prefab, transform.position, transform.rotation);
    }

}
