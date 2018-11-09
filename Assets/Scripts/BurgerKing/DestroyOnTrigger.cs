using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnTrigger : MonoBehaviour {

    public GameObject objectDestroy;
    public GameObject explosion;

    public AudioSource sonExplosion;

    public bool detruit = false;


    void Start()
    {
        explosion.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && detruit == false)
        {
            explosion.SetActive(true);
            sonExplosion.Play();
            Instantiate(explosion, explosion.transform.position, explosion.transform.rotation);
            Destroy(objectDestroy);
            detruit = true;
        }
    }

}
