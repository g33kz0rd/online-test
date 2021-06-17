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
}
