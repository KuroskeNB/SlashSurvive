using UnityEngine;

public class ParticleDestroy : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("Animator component not found on this GameObject.");
        }
        else
        {
            // Найти длину анимации и уничтожить объект после её завершения
            AnimatorClipInfo[] clipInfo = animator.GetCurrentAnimatorClipInfo(0);
            float clipLength = clipInfo[0].clip.length;
            Destroy(gameObject, clipLength);
        }
    }
}
