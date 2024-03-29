using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{

    public GameObject respawnPoint;

    public float respawnDepth = -100f;
        public bool fallDeath = true;
    public float respawnDistance = 1000f;
        public bool distanceDeath = false;
    
    public Vector3 direction = new Vector3(0, 0, 0);

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
        if (transform.position.y < respawnDepth && fallDeath) {

            RespawnMe();

        }

        if (Vector3.Distance(respawnPoint.transform.position, transform.position) > respawnDistance && distanceDeath) {

            RespawnMe();

        }

    }

    void RespawnMe() {

        if (GetComponent<Rigidbody>()) {

            GetComponent<Rigidbody>().velocity = new Vector3 (0f, 0f, 0f);

        }

        transform.position = respawnPoint.transform.position;
        //transform.rotation = Quaternion.Euler(direction);

        Debug.Log("Respawned");

    }

    void OnTriggerEnter(Collider other) {

        if (other.tag == "Respawn") {

            respawnPoint = other.transform.parent.gameObject;

        }

    }
}
