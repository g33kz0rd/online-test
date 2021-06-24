using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private CharacterController characterController;

    void Update()
    {
        float speed = Vector3.Magnitude(characterController.velocity);
        animator.SetFloat("Speed", speed);
    }
}
