using UnityEngine;

public class InputController : MonoBehaviour
{
    //Camera
    public string cam_left = "a", cam_right = "d", cam_forward = "w", cam_backwards = "s";
    public string cam_rotate_l = "z", cam_rotate_r = "x";
    public string cam_up, cam_down;

    public static InputController controller;

    void Awake()
    {
        controller = this;
    }
}