using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideAction : MonoBehaviour
{
    public bool activated = false;
    [SerializeField] private PushAndPull pap;

    public GameObject target;

    public float slidePower = 50f;

    // Start is called before the first frame update
    void Start()
    {

        activated = false;
        pap = GetComponent<PushAndPull>();
        
    }

    // Update is called once per frame
    void Update()
    {

        var move = Input.GetAxis("Mouse Y");

        if (activated) {

            var dir = transform.forward * move * Time.deltaTime * slidePower;
            target.transform.position += dir;

        }
        
    }

    void OnDisable()
    {
        
        if (activated) {
            
            activated = false;

        }

    }

    void OnEnable()
    {

        pap.DisableControl(false, false, false);
        activated = true;

    }

}
