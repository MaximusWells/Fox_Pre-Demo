using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour {

    public static Teleport instance;

    [SerializeField]
    protected GameObject teleportTo;

    protected Transform tpUserTransform;
    protected bool canTp;
    protected bool tpAllowed;

    // Use this for initialization
    void Start() {

    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Tp();
        }

        if (Door.instance.isOpened == true)
        {
            tpAllowed = true;
        }
    }

    void Tp()
    {
        if (canTp == true && tpAllowed == true)
        {
            tpUserTransform.transform.position = teleportTo.transform.position;
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.name == "Player")
        {
            canTp = true;
            tpUserTransform = collider.GetComponent<Transform>();
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        canTp = false;
    }
}
