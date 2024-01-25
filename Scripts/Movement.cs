using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    private Rigidbody rb;
    
    public float moveSpeed = 1f;
    
    // Start is called before the first frame update
    void Start()
    {

        rb = GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
        rb.MovePosition(transform.position 
            + (transform.forward * Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime) 
            + (transform.right * Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime));

    }

}
