using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{

    [SerializeField]
    private InputAction camControlls;
    private Vector3 moveDir = Vector3.zero;
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float rotSpeed;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        camControlls.Enable();
    }

    private void Update()
    {
        moveDir = camControlls.ReadValue<Vector3>();
    }

    private void FixedUpdate()
    {
        Vector3 toConvert = new Vector3(moveDir.x, 0, moveDir.y);
        Vector3 dir = IsoConverter.IsoVectorConvert(toConvert);
        rb.velocity = dir * moveSpeed;
    }

    private void OnDisable()
    {
        camControlls.Disable();
    }
}
