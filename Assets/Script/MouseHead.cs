using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseHead : MonoBehaviour
{

    [SerializeField] float speedx,speedy;
    [SerializeField] float MaxYAngle;
    float xm, ym;

    // Update is called once per frame
    void Update()
    {
        FPSCamera();
    }

    void FPSCamera() {
        xm = Input.GetAxis("Mouse X") * speedx * Time.deltaTime;
        ym -= Input.GetAxis("Mouse Y") * speedy * Time.deltaTime;
        ym = Mathf.Clamp(ym, -MaxYAngle, MaxYAngle);
        transform.localRotation = Quaternion.Euler(ym, 0, 0);
        transform.parent.Rotate(Vector3.up * xm);
    }
}
