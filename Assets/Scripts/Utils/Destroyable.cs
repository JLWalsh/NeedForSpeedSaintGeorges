using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Destroyable : MonoBehaviour {

    private static readonly string DESTROY_FOR_TAG = "Player";

    private void Awake() {
        Rigidbody rigidbody = GetComponent<Rigidbody>();

        if (rigidbody != null)
            Destroy(rigidbody);

        BoxCollider boxCollider = GetComponent<BoxCollider>();

        if (boxCollider == null)
            gameObject.AddComponent<BoxCollider>();
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.rigidbody == null)
            return;

        if (collision.collider.tag == DESTROY_FOR_TAG) {
            Rigidbody rigidbody = GetComponent<Rigidbody>();

            if(rigidbody == null) {
                rigidbody = gameObject.AddComponent<Rigidbody>();
            }

            rigidbody.AddForce(collision.relativeVelocity * 100f);
        }
    }

}
