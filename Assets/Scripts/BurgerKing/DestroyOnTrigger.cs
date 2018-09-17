using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnTrigger : MonoBehaviour {

    public GameObject objectDestroy;
    public GameObject explosion;

    public bool detruit = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "CanDestroy" && detruit == false)
        {
            print(other.gameObject.tag);
            Instantiate(explosion, explosion.transform.position, explosion.transform.rotation);
            Destroy(objectDestroy);
            detruit = true;
        }
    }

}
