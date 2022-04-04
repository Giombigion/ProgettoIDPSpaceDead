using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseHead : MonoBehaviour
{
    [Range(1, 360)]
    [SerializeField] float sensibilityX,sensibilityY;
    [Range(1, 120)]
    [SerializeField] float MaxYAngle;
    float xm, ym;


    // Update is called once per frame
    void Update()
    {
        FPSCamera();
    }

    void FPSCamera() {
        xm = Input.GetAxis("Mouse X") * sensibilityX * Time.deltaTime;
        ym -= Input.GetAxis("Mouse Y") * sensibilityY * Time.deltaTime;
        ym = Mathf.Clamp(ym, -MaxYAngle, MaxYAngle);
        transform.localRotation = Quaternion.Euler(ym, 0, 0);
        transform.parent.Rotate(Vector3.up * xm);
    }
}
