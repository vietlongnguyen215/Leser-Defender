using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spiner : MonoBehaviour
{
    [SerializeField] float speedSpinner = 1f;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, speedSpinner * Time.deltaTime);
    }
}
