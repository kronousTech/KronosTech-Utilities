All notable changes to this project will be documented in this file.

## [2.11.4] - 10-11-2025 - João Santos
### Added
- Added casting to JsonExtensions DeserializeOrDefault<T>.

## [2.11.3] - 10-11-2025 - João Santos
### Fixed
- Null check on custom package setup in case object is null.

## [2.11.2] - 05-11-2025 - João Santos
### Fixed
- Fixed Newtonsoft dependency.

## [2.11.1] - 05-11-2025 - João Santos
### Added
- Added colored buttons to EditorCustomPackageSetup to help with readability.

## [2.11.0] - 03-11-2025 - João Santos
### Added
- Added GetScenePath to TransformExtensions.
- Added GetScenePath to GameObjectExtensions.

## [2.10.1] - 29-10-2025 - João Santos
### Added
- Added improvements to CustomPackages setup editor window.

## [2.10.0] - 29-10-2025 - João Santos
### Added
- Added new CustomPackage setup system.

## [2.9.1] - 29-10-2025 - João Santos
### Fixed
- Fixed EditorWindowUpdatePackages.

## [2.9.0] - 04-09-2025 - João Santos
### Added
- Added CreatePrefabByResources

## [2.8.0] - 04-09-2025 - João Santos
### Added
- Added UnityEvents to CanvasGroupUtility.cs.

## [2.7.0] - 04-09-2025 - João Santos
### Added
- Added JsonExtensions.cs.

## [2.6.0] - 13-08-2025 - João Santos
### Added
- Added IsValidEmail() to StringExtensions.cs.

## [2.5.0] - 13-08-2025 - João Santos
### Added
- Added EditorToolCreateCustomPackagePrefabsVariant.cs.

## [2.4.0] - 05-08-2025 - João Santos
### Added
- Added RemoveWhitespace() to StringExtensions.cs.

## [2.3.0] - 05-08-2025 - João Santos
### Added
- Added CreatePathFolders(string path) to EditorHelper.

## [2.2.0] - 30-07-2025 - Pedro Ermida
### Added
- Added StringExtensions.cs.
- Added Censor() to StringExtensions.cs.

## [2.1.0] - 29-07-2025 - João Santos
### Added
- Added Swap<T> to ListExtensions.cs.
- Added Swap<T> to ArrayExtensions.cs.

## [2.0.0] - 12-06-2025 - João Santos
### Added
- Added CanvasGroupUtility.cs.
- Added OnStartUnityEvent.cs.
### Changed
- Merged the EditorTools package.
- Renamed from Extensions to utilities.
- Renamed most of the namespaces.

## [1.10.0] - 12-06-2025 - João Santos
### Added
- Added TryAdd<T> to ListExtensions.cs.

## [1.9.0] - 05-06-2025 - João Santos
### Added
- Added GetRandomElement<T> to ListExtensions.cs.
- Added GetRandomElement<T> to ArrayExtensions.cs.

## [1.8.0] - 04-06-2025 - João Santos
### Added
- Added GetHexCode() to ColorExtensions.cs.

## [1.7.1] - 23-04-2025 - João Santos
### Fixed
- Fixed Stack Overlow bug on method TryGetComponentInChildren<T> on GameObjectExtensions.cs.
- Fixed Stack Overlow bug on method TryGetComponentInParent<T> on GameObjectExtensions.cs.
- Fixed Stack Overlow bug on method TryGetComponentInChildren<T> on MonoBehaviourExtensions.cs.
- Fixed Stack Overlow bug on method TryGetComponentInParent<T> on MonoBehaviourExtensions.cs.

## [1.7.0] - 23-04-2025 - João Santos
### Added
- Added IsValidIndex<T>(int index) to ArrayExtensions.cs.
- Added ArrayExtensions.cs.

## [1.6.0] - 23-04-2025 - João Santos
### Added
- Added SetVisibility(bool visible) to CanvasGroupExtensions.cs.
- Added CanvasGroupExtensions.cs.

## [1.5.0] - 07-04-2025 - João Santos
### Added
- Added ListExtensions.cs.
- Added Shuffle<T>() to ListExtensions.cs.

## [1.4.0] - 02-04-2025 - João Santos
### Added
- Added TryGetComponentInChildren<T>(out T value, bool includeInactive = false) to TransformExtension.cs.
- Added TryGetComponentInParent<T>(out T value, bool includeInactive = false) to TransformExtension.cs.
- Added TryGetComponentInChildren<T>(out T value, bool includeInactive = false) to GameObjectExtension.cs.
- Added TryGetComponentInParent<T>(out T value, bool includeInactive = false) to GameObjectExtension.cs.
- Added TryGetComponentInChildren<T>(out T value, bool includeInactive = false) to MonoBehaviourExtension.cs.
- Added TryGetComponentInParent<T>(out T value, bool includeInactive = false) to MonoBehaviourExtension.cs.

## [1.3.0] - 31-03-2025 - João Santos
### Added
- Added script icons.
- Added GetRectTransform to TransformExtensions.cs.

## [1.2.1] - 05-03-2025 - João Santos
### Changed
- Rewrote method commentaries.
- Rewrote log errors.
- Renamed GetClipByName(string clipName) to GetAnimationClip(string animationClipName).
- Renamed GetClipDuration(string clipName) to GetAnimationClipLength(string animationClipName).

## [1.2.0] - 05-03-2025 - João Santos
### Added
- Added correct url to CHANGELOG.
- Added GetClipByName(string clipName) to AnimatorExtensions.cs.
- Added GetClipByName(string clipName) to RuntimeAnimationController.cs.
### Changed
- Renamed GetAnimationDuration to GetClipDuration;

## [1.1.0] - 27-02-2025 - João Santos
### Added
- Added GetComponentsInChildrenExclusive() to TransformExtensions.cs.
- Added GameObjectExtensions.cs.
- Added MonoBehaviourExtensions.cs.
- Added more info to the package description.

## [1.0.0] - 27-02-2025 - João Santos
### Added
- Added TransformExtensions.cs.
- Added AnimatorExtensions.cs.