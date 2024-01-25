using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PushAndPull : MonoBehaviour
{

    public FirstPersonCamera fpc;
        public Movement movement;
        public CustomGravity cg;
            public Rigidbody rb;
            public Collider playerCol;

    public List<MonoBehaviour> behaviors;
        [SerializeField] private bool behaviorActivated;
        public GrappleAction ga;
        public RotateAction ra;
    
    public Camera cam;
        public GameObject cameraPivot;

    public GameObject crosshair;
        public Image crosshairImage;

    [SerializeField] public GameObject target;

    // Start is called before the first frame update
    void Start()
    {

        cam = Camera.main;

        fpc = cam.gameObject.GetComponent<FirstPersonCamera>();
        movement = GetComponent<Movement>();
        cg = GetComponent<CustomGravity>();
            rb = GetComponent<Rigidbody>();
            playerCol = GetComponent<Collider>();
        
        crosshairImage = crosshair.GetComponent<Image>();

        ga = GetComponent<GrappleAction>();
        ra = GetComponent<RotateAction>();
        ga.enabled = false;
        ra.enabled = false;

    }

    // Update is called once per frame
    void Update()
    {

        BehaviorManagement();
        
        RaycastHit hit;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit)) {

            var hitObject = hit.transform.gameObject;

            var behaviorFound = FindABehavior(hitObject);

            // change crosshair color based on whether or not object has behavior
            if (behaviorFound) {

                crosshairImage.color = Color.red;

            } else {

                crosshairImage.color = Color.white;

            }

        } else {

            crosshairImage.color = Color.white;

        }

    }

    bool FindABehavior(GameObject hitObject) {

        var objectFound = false;

        // if grapple point found
        if (hitObject.GetComponent<GrapplePoint>()) {

            objectFound = true;
            target = hitObject;

            if (Input.GetMouseButtonDown(1)) {

                behaviorActivated = true;
                ga.enabled = true;

            }

        }

        // if rotating component found
        if (hitObject.GetComponent<RotateComponent>()) {

            objectFound = true;
            target = hitObject;

            if (Input.GetMouseButtonDown(1)) {

                behaviorActivated = true;
                ra.enabled = true;

            }

        }


        // was a behavior found?
        if (objectFound || behaviorActivated) {
            return true;
        } else {
            return false;
        }

    }

    public void DisableControl(bool move, bool cam, bool grav, bool col = false) {

        //cameraPivot.transform.parent = null;

        movement.enabled = move;
        fpc.enabled = cam;
        cg.enabled = grav;
        playerCol.enabled = col;
        
        if (!grav) {

            rb.velocity = Vector3.zero;

        }

    }

    public void EnableControl() {

        behaviorActivated = false;
        movement.enabled = true;
        fpc.enabled = true;
        cg.enabled = true;
        playerCol.enabled = true;

    }

    public void BehaviorManagement() {

        if (Input.GetMouseButtonUp(1) && behaviorActivated) {

            ga.enabled = false;
            ra.enabled = false;
            EnableControl();

            //CameraLookAtTarget();
            //cameraPivot.transform.parent = transform;
            //transform.rotation = Quaternion.Euler(transform.localEulerAngles.x, -cam.transform.localEulerAngles.y, transform.localEulerAngles.z);

        }

        else if (behaviorActivated) {

            //CameraLookAtTarget();
            //cameraPivot.transform.position = transform.position;

        }

    }

    public void CameraLookAtTarget() {

        Vector3 relativePos = target.transform.position - cam.gameObject.transform.position;
        Quaternion rotation = Quaternion.LookRotation(relativePos, transform.up);
        cam.transform.rotation = rotation;

        //cam.transform.rotation = Quaternion.Euler(cam.transform.rotation.x, cam.transform.rotation.y, 0f);

    }

}
