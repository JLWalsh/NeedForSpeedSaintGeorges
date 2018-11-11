using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Destroyable : MonoBehaviour {

    public float knockSpeed = 100f;
    public AudioClip knockSound;

    private static readonly string DESTROY_FOR_TAG = "Player";

    private AudioSource backgroundAudio;

    private void Awake() {
        Rigidbody rigidbody = GetComponent<Rigidbody>();

        if (rigidbody != null)
            Destroy(rigidbody);

        BoxCollider boxCollider = GetComponent<BoxCollider>();

        if (boxCollider == null)
            gameObject.AddComponent<BoxCollider>();

        SetCollidersTrigger(true);
    }

    private void Start() {
        backgroundAudio = GameObject.FindGameObjectWithTag("EffectsAudio").GetComponent<AudioSource>();
    }

    private void SetCollidersTrigger(bool isTrigger) {
        foreach (Collider collider in GetComponents<Collider>())
            collider.isTrigger = isTrigger;
    }

    private void OnCollisionEnter(Collision collision) {
        if(collision.collider.tag == DESTROY_FOR_TAG) {
            backgroundAudio.PlayOneShot(knockSound);
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == DESTROY_FOR_TAG) {
            Rigidbody rigidbody = GetComponent<Rigidbody>();

            if(rigidbody == null) {
                rigidbody = gameObject.AddComponent<Rigidbody>();
            }

            Vector3 forceDirection = transform.position - other.transform.position;
            rigidbody.AddForce(forceDirection * knockSpeed);

            SetCollidersTrigger(false);

        }
    }

}
