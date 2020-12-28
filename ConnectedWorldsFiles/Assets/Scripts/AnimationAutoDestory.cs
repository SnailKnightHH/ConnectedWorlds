using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationAutoDestory : MonoBehaviour
{
    [SerializeField] float dalay = 0.5f;
    void Start()
    {
        Destroy(gameObject, dalay);
    }
}
