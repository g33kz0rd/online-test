using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private CharacterController characterController;

    // Update is called once per frame
    void Update()
    {
        float speed = Vector3.Magnitude(characterController.velocity);
        animator.SetFloat("Speed", speed);
    }

    private void OnGUI()
    {
        float speed = Vector3.Magnitude(characterController.velocity);
        GUI.TextField(new Rect(10, 10, 200, 50), $"Speed {speed}");
    }
}
