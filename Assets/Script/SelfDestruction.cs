using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruction : MonoBehaviour {

    [SerializeField]
    protected float waitToDestroy;

	void Start () {
        Destroy(gameObject, waitToDestroy);
    }
}
