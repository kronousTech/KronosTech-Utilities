using KendirStudios.CustomPackages.Utilities.Extensions;
using UnityEngine;
using UnityEngine.Events;

namespace KendirStudios.CustomPackages.Utilities.Tools
{
    [RequireComponent(typeof(CanvasGroup))]
    public class CanvasGroupUtility : MonoBehaviour
    {
        [Header("UnityEvents")]
        public UnityEvent OnShow = new();
        public UnityEvent OnHide = new();

        private CanvasGroup m_canvasGroup;

        private void Awake()
        {
            m_canvasGroup = GetComponent<CanvasGroup>();
        }

        public void SetVisibility(bool visible)
        {
            if (m_canvasGroup == null)
            {
                if (!Application.isPlaying)
                {
                    // if we are on the editor the awake hasn't been called yet
                    m_canvasGroup = GetComponent<CanvasGroup>();
                }
                else
                {
                    Debug.LogError($"{nameof(CanvasGroupUtility)}.cs: " +
                        $"Failed to get the {nameof(CanvasGroup)} component." +
                        $"Check if the {nameof(CanvasGroup)} component is on the GameObject.");

                    return;
                }

            }

            m_canvasGroup.SetVisibility(visible);

            if (visible)
            {
                OnShow?.Invoke();
            }
            else
            {
                OnHide?.Invoke();
            }
        }

        [NaughtyAttributes.Button("Show CanvasGroup")]
        public void Show()
        {
            SetVisibility(true);
        }

        [NaughtyAttributes.Button("Hide CanvasGroup")]
        public void Hide()
        {
            SetVisibility(false);
        }
    }
}