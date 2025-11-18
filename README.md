This package contains extensions scripts for Unity's base classes.

Transform Extensions:
- DestroyChildren();
- ShuffleChildren();
- GetComponentsInChildrenExclusive<T>(bool includeInactive = false);
- GetRectTransfrom();
- TryGetComponentInChildren<T>(out T value, bool includeInactive = false);
- TryGetComponentInParent<T>(out T value, bool includeInactive = false);
- GetScenePath();

Animator Extensions:
- GetAnimationClipLength(string animationClipName);
- GetAnimationClip(string animationClipName);

Animation RuntimeController:
- GetAnimationClipLength(string animationClipName);
- GetAnimationClip(string animationClipName);

GameObject Extensions:
- GetComponentsInChildrenExclusive<T>();
- TryGetComponentInChildren<T>(out T value, bool includeInactive = false);
- TryGetComponentInParent<T>(out T value, bool includeInactive = false);
- GetScenePath();

MonoBehaviour Extensions:
- GetComponentsInChildrenExclusive<T>();
- TryGetComponentInChildren<T>(out T value, bool includeInactive = false);
- TryGetComponentInParent<T>(out T value, bool includeInactive = false);

List Extensions:
- Swap<T>(int indexA, int indexB);
- Shuffle<T>();
- IsValidIndex<T>(int index);
- GetRandomElement<T>();
- TryAdd<T>(T element);

CanvasGroup Extensions:
- SetVisibility(bool visible);

Array Extensions:
- Swap<T>(int indexA, int indexB);
- IsValidIndex<T>(int index);
- GetRandomElement<T>();

Color Extensions:
- GetHexCode();

String Extensions:
- Censor();
- RemoveWhitespace();
- IsValidEmail();

Json Extensions
- DeserializeOrDefault<T>(;

Tools:
- CanvasGroupUtility;
- OnStartUnityEvent;

EditorTools:
- Create CustomPackage Prefabs Variant.

EditorWindow:
- Find Missing Scripts.
- Find Scripts GUIDS.
- Scene Geometry Statistics.
- Replace GUID.
- Update Packages.
