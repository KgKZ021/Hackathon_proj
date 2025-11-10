using System;
using UnityEngine;

public class EmojiAnimator : MonoBehaviour
{

    private const string PLACEHOLDER_CLIP_NAME = "Placeholder_Click";
    private const string IDLE_STATE_NAME = "Idle";
    private const string EMOJI_ANIMATE = "AnimateEmoji";
    private AnimatorOverrideController overrideController;
    private Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();

        overrideController = new AnimatorOverrideController(animator.runtimeAnimatorController);
        animator.runtimeAnimatorController = overrideController;
    }
    
    public void SetupAnimation(AnimationClip clipToPlay)
    {
        if (clipToPlay == null)
        {
            //Debug.LogError("SetupAnimation was called, but clipToPlay is NULL! Check your ScriptableObject.", this);
            return;
        }

        
            overrideController[PLACEHOLDER_CLIP_NAME] = clipToPlay;
            //Debug.Log("Successfully swapped '" + PLACEHOLDER_CLIP_NAME + "' with '" + clipToPlay.name + "'", this);
        
        
    }

    public void PlayClickAnimation()
    {
        if (animator != null)
        {
            animator.SetTrigger(EMOJI_ANIMATE);
        }
    }

    public void ResetVisual()
    {
        if (animator != null)
        {
            animator.Play(IDLE_STATE_NAME);
        }
    }


}
