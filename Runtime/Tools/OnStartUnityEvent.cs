using UnityEngine;
using UnityEngine.Events;

namespace KendirStudios.CustomPackages.Utilities.Tools
{
    public class OnStartUnityEvent : MonoBehaviour
    {
        [Header("Unity Events")]
        [SerializeField] private UnityEvent OnStart;

        private void Start()
        {
            OnStart.Invoke();
        }
    }
}