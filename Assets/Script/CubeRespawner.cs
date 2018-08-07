using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeRespawner : MonoBehaviour {

    [SerializeField]
    private Vector2 respawnAt;

    const float pointWhenNeedToRespawn = -0.5f;

    void Update()
    {
        if (transform.position.y < pointWhenNeedToRespawn)
        {
            transform.position = respawnAt;
        }
    }
}
