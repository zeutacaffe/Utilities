using UnityEngine;

public class SmoothCameraControllerV2 : UniversalCameraController
{
    [Header("Smooth Stop")]
    public bool AllowSmoothStop = true;
    public float StopTimer = .5f;
    public float ZoomTimer = .5f;
    public float RotationTimer = .25f;

    private float _horizontalstop;
    private bool _horizontal;
    private Vector3 _horizontalVelocity;

    private float _verticalstop; 
    private bool _vertical;
    private Vector3 _verticalVelocity;

    private float _rotationstop;
    private bool _rotation;
    private Vector3 _rotationVelocity;

    public override void Move(Vector3 target)
    {
        _horizontal = true;
        _horizontalVelocity = base.HorizontalDirection();
        base.Move(target);
    }

    public override void Zoom(Vector3 target)
    {
        _vertical = true;
        _verticalVelocity = base.VerticalDirection();
        base.Zoom(target);
    }

    public override void Rotate(Vector3 target)
    {
        _rotation = true;
        _rotationVelocity = base.RotationDirection();
        base.Rotate(target);
    }

    public override void Update()
    {
        if (AllowSmoothStop && !isMoving() && _horizontal)
        {
            StopHorizontal();
        } else
        {
            _horizontalstop = 0;
        }

        if(AllowSmoothStop && !isHeight() && _vertical)
        {
            StopVertical();
        } else
        {
            _verticalstop = 0;
        }

        if (AllowSmoothStop && !isRotating() && _rotation)
        {
            StopRotation();
        }else
        {
            _rotationstop = 0;
        }

        base.Update();
    }

    public void StopHorizontal()
    {
        Vector3 target = transform.position + _horizontalVelocity * (1 - _horizontalstop / StopTimer);
        _horizontalstop += Time.deltaTime;

        if (_horizontalstop >= StopTimer)
        {
            _horizontalstop = 0;
            _horizontal = false;
        }
        base.Move(target);
    }

    public void StopVertical()
    {
        Vector3 target = transform.position + _verticalVelocity * (1 - _verticalstop / StopTimer);
        _verticalstop += Time.deltaTime;

        if (_verticalstop >= ZoomTimer)
        {
            _verticalstop = 0;
            _vertical = false;
        }
        base.Zoom(target);
    }

    public void StopRotation()
    {
        Vector3 target = transform.eulerAngles + _rotationVelocity * (1 - _rotationstop / StopTimer);
        _rotationstop += Time.deltaTime;

        if (_rotationstop >= RotationTimer)
        {
            _rotationstop = 0;
            _rotation = false;
        }
        base.Rotate(target);
    }
}