using UnityEngine;

public class AntIKController : MonoBehaviour
{
    private Animator animator;
    public Transform footTarget; // La posición final de la pata

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void OnAnimatorIK(int layerIndex)
    {
        if (animator)
        {
            animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 1);
            animator.SetIKPosition(AvatarIKGoal.LeftFoot, footTarget.position);
        }
    }
}
