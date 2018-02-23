using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugControllerMovement : MonoBehaviour {

    public float speed;

    private float verticalMovement;
    private float horizontalMovement;
    private float forwardMovement;

    private float roll;
    private float pitch;
    private float yaw;

	void Update () {

        verticalMovement = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        horizontalMovement = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        forwardMovement = Input.GetAxis("Forward") * speed * Time.deltaTime;

        transform.Translate(Vector3.forward * forwardMovement);
        transform.Translate(Vector3.right * horizontalMovement);
        transform.Translate(Vector3.up * verticalMovement);

        roll  = Input.GetAxis("Roll") * speed * 100 * Time.deltaTime;
        pitch = Input.GetAxis("Pitch") * speed * 100 * Time.deltaTime;
        yaw = Input.GetAxis("Yaw") * speed * 100 * Time.deltaTime;

        transform.Rotate(Vector3.forward * roll);
        transform.Rotate(Vector3.right * pitch);
        transform.Rotate(Vector3.up * yaw);

    }
}
