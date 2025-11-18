using UnityEngine;

namespace KendirStudios.CustomPackages.Utilities.Extensions
{
    public static class AnimatorExtensions
    {
        /// <summary>
        /// Finds a AnimationClip by name on the RuntimeAnimationController and returns its duration.
        /// </summary>
        /// <param name="animator"></param>
        /// <param name="animationClipName"></param>
        /// <returns></returns>
        public static float GetAnimationClipLength(this Animator animator, string animationClipName)
        {
            if (animator.runtimeAnimatorController == null)
            {
                Debug.LogError("Animator's RuntimeAnimationController is null, returning null");

                return 0f;
            }

            return animator.runtimeAnimatorController.GetAnimationClipLength(animationClipName);
        }

        /// <summary>
        /// Finds a AnimationClip by name on the RuntimeAnimationController and returns it.
        /// </summary>
        /// <param name="animator"></param>
        /// <param name="animationClipName"></param>
        /// <returns></returns>
        public static AnimationClip GetAnimationClip(this Animator animator, string animationClipName)
        {
            if (animator.runtimeAnimatorController == null)
            {
                Debug.LogError("Animator's RuntimeAnimationController is null,  returning null");

                return null;
            }

            return animator.runtimeAnimatorController.GetAnimationClip(animationClipName);
        }
    }
}