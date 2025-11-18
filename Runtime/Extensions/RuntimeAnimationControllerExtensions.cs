using UnityEngine;

namespace KendirStudios.CustomPackages.Utilities.Extensions
{
    public static class RuntimeAnimationControllerExtensions
    {
        /// <summary>
        /// Finds a AnimationClip by name on the RuntimeAnimationController and returns its duration.
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="animationClipName"></param>
        /// <returns></returns>
        public static float GetAnimationClipLength(this RuntimeAnimatorController controller, string animationClipName)
        {
            foreach (var animationClip in controller.animationClips)
            {
                if (animationClip.name == animationClipName)
                {
                    // Returns the animation duration in seconds
                    return animationClip.length;
                }
            }

            Debug.LogError("AnimationClip not found, returning 0.");

            return 0f;
        }

        /// <summary>
        /// Finds a AnimationClip by name on the RuntimeAnimationController and returns it.
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="animationClipName"></param>
        /// <returns></returns>
        public static AnimationClip GetAnimationClip(this RuntimeAnimatorController controller, string animationClipName)
        {
            foreach (var animationClip in controller.animationClips)
            {
                if (animationClip.name == animationClipName)
                {
                    return animationClip;
                }
            }

            Debug.LogError("AnimationClip not found, returning null.");

            return null;
        }
    }
}