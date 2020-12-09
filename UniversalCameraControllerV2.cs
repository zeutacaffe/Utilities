using System.Reflection;
using UnityEngine;

public class UniversalCameraControllerV2 : MonoBehaviour
{

    [Header("Constraints")]
    public bool AllowMovement = true;
    public bool AllowRotation = true;
    public bool AllowZooming = true;
    public bool AllowTilt = true;

    [Header("Speed and Rotation")]
    public float Speed = 10;
    public float Zooming = 15; 
    public float Rotation = 1;

    [Header("Options")]
    public float MinimumHeight;
    public float MaximumHeight; 

    public float MinimumTilt;
    public float MaximumTilt; 

    public virtual void Update()
    {
        if (isMoving() && AllowMovement)
            Move(MoveTarget());

        if (isRotating() && AllowRotation)
            Rotate(RotationOffset());

        if (isHeight() && AllowZooming)
        {
            Zoom(ZoomTarget());
           // Tilt();
        }
    }

    public virtual void Move(Vector3 target)
    {
        transform.position = Vector3.Lerp(transform.position, target, Speed * Time.deltaTime);
    }

    public virtual void Rotate(Vector3 rotation)
    {
        transform.eulerAngles -= rotation * Rotation;
    }

    public virtual void Zoom(Vector3 zoom)
    {
        transform.position = Vector3.Lerp(transform.position, zoom, Zooming * Time.deltaTime);
    }

    //-------------------------------
    protected virtual Vector3 MoveOffset()
    {
        //returns the basic values, if it's 0 than it's not active.
        Vector3 direction = new Vector3();
        if (Input.GetKey(InputController.controller.cam_forward))
        {

            Vector3 forward = transform.forward;
            Vector3 projection = Vector3.ProjectOnPlane(forward, Vector3.up).normalized;
            direction += projection;
        }

        if (Input.GetKey(InputController.controller.cam_backwards))
        {
            Vector3 forward = transform.forward;
            Vector3 projection = Vector3.ProjectOnPlane(forward, Vector3.up).normalized;
            direction -= projection;
        }

        if (Input.GetKey(InputController.controller.cam_left))
        {
            Vector3 right = transform.right;
            Vector3 projection = Vector3.ProjectOnPlane(right, Vector3.up).normalized;
            direction -= projection;
        }

        if (Input.GetKey(InputController.controller.cam_right))
        {
            Vector3 right = transform.right;
            Vector3 projection = Vector3.ProjectOnPlane(right, Vector3.up).normalized;
            direction += projection;
        }


        return direction.normalized;
    }

    protected virtual Vector3 MoveTarget()
    {
        Vector3 offset = MoveOffset();
        return transform.position + offset;
    }

    protected virtual Vector3 RotationOffset()
    {
        Vector3 p_Rotation = new Vector3();

        if (Input.GetKey(InputController.controller.cam_rotate_r)) { p_Rotation += new Vector3(0, -1, 0); }
        if (Input.GetKey(InputController.controller.cam_rotate_l)) { p_Rotation += new Vector3(0, 1, 0); }

        return p_Rotation.normalized;
    }

    protected virtual Vector3 ZoomTarget()
    {
        Vector3 offset = ZoomOffset();
        return transform.position + offset;
    }

    protected virtual Vector3 ZoomOffset()
    {
        Vector3 p_Velocity = new Vector3();
        if (Input.mouseScrollDelta.y != 0)
        {
            p_Velocity += new Vector3(0, Input.mouseScrollDelta.y, 0).normalized;
        }

        return p_Velocity;
    }


    //-------------------------------
    //Replace mouseScroll with input controller
    public bool isMoving()
    {
        return Input.GetKey(InputController.controller.cam_forward)
            || Input.GetKey(InputController.controller.cam_backwards)
            || Input.GetKey(InputController.controller.cam_left)
            || Input.GetKey(InputController.controller.cam_right);
        
        /*
        return Input.GetKey(KeyCode.W)
            || Input.GetKey(KeyCode.S)
            || Input.GetKey(KeyCode.A)
            || Input.GetKey(KeyCode.D);
        */
    }

    public bool isRotating()
    {
        return Input.GetKey(InputController.controller.cam_rotate_r)
            || Input.GetKey(InputController.controller.cam_rotate_l);
        
        /*
        return Input.GetKey(KeyCode.Q)
           || Input.GetKey(KeyCode.E);*/
    }

    public bool isHeight()
    {
        return Input.mouseScrollDelta.y != 0;
    }
}
