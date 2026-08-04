# AGENTS.md

## Cursor Cloud specific instructions

### What this repo is
A **Unity 6 (`6000.0.61f1`) 2D (URP)** game project. The actual Unity project lives in
the `Social/` subdirectory (that folder contains `Assets/`, `Packages/`, and
`ProjectSettings/`). As of this writing it is close to the default 2D URP template
(no `.cs` gameplay scripts yet) — the main scene is `Social/Assets/Scenes/SampleScene.unity`.

### Unity Editor (pre-installed in the VM snapshot)
The matching Unity Editor is installed at `/opt/unity/Editor/Unity` and symlinked to
`/usr/local/bin/unity` (so `unity` is on `PATH`). It is **not** reinstalled on startup —
it is baked into the snapshot. `unity -version` prints `6000.0.61f1`.

- Unity is a native desktop app; for headless runs always wrap it in `xvfb-run -a` and pass
  `-batchmode -nographics`. Example (import + compile the project, then quit):
  ```bash
  xvfb-run -a unity -batchmode -nographics -projectPath Social -logFile - -quit
  ```
- Opening the project regenerates the gitignored `Social/Library/` cache and may auto-touch
  a few files under `Social/ProjectSettings/` (e.g. `ShaderGraphSettings.asset`). Revert any
  unintended edits to committed files afterwards (`git checkout -- Social/ProjectSettings/...`).

### Licensing is required and is the main gotcha
Unity refuses to open/build/test without an activated license. Symptom in the log when
unlicensed: `Found 0 entitlement groups and 0 free entitlements`. There is **no license
baked into the snapshot** unless one was activated during environment setup.

Activate before doing anything else in a fresh VM. Two supported paths:

1. Credentials (Unity Personal, free) — needs `UNITY_EMAIL` / `UNITY_PASSWORD` secrets
   (and `UNITY_SERIAL` only for Plus/Pro):
   ```bash
   xvfb-run -a unity -batchmode -nographics -quit -logFile - \
     -username "$UNITY_EMAIL" -password "$UNITY_PASSWORD"   # add -serial "$UNITY_SERIAL" for Plus/Pro
   ```
2. Manual license file — generate an activation request, exchange it at
   <https://license.unity3d.com/manual> for a `.ulf`, then:
   ```bash
   xvfb-run -a unity -batchmode -nographics -quit -createManualActivationFile   # -> Unity_v6000.0.61f1.alf
   xvfb-run -a unity -batchmode -nographics -quit -manualLicenseFile /path/to/Unity_lic.ulf
   ```

The license activation state is stored on the filesystem; if you activate during a setup
session that is snapshotted, later agents inherit it. Otherwise re-activate per VM.

### Test / build (all require an active license + xvfb)
- Run EditMode tests via the Unity Test Framework (`com.unity.test-framework` is a dependency):
  ```bash
  xvfb-run -a unity -batchmode -nographics -projectPath Social -runTests \
    -testPlatform EditMode -testResults /tmp/results.xml -logFile -
  ```
  Use `-testPlatform PlayMode` for play-mode tests. (There are no tests in the repo yet, so
  this currently just validates the toolchain.)
- Building a standalone player needs an editor build method invoked with
  `-executeMethod <Class.Method>` (the project has no build script yet), or use Unity's
  build UI. There is no CLI build target wired up in the repo.

### Notes
- There is no Node/Python/etc. toolchain here — Unity manages package restoration itself the
  first time the project opens (populating `Social/Library/`), so the only heavy dependency is
  the Unity Editor itself.
