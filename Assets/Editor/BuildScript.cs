using System;
using System.Collections.Generic;
using UnityEditor;

class BuildScript {
	static string[] SCENES = FindEnabledEditorScenes();

	static string APP_NAME = "Math Ninja";
	static string TARGET_DIR = "Build";

	[MenuItem("Custom/CI/Build Mac OS X")]
	static void PerformMacOSXBuild() {
		string target_dir = APP_NAME + ".app";
		GenericBuild(SCENES, TARGET_DIR + "/" + target_dir,
			BuildTarget.StandaloneOSXIntel, BuildOptions.None);
	}

	[MenuItem("Custom/CI/Build Android")]
	static void PerformAndroidBuild() {
		string target_dir = "Android/" + APP_NAME + ".apk";
		GenericBuild(SCENES, TARGET_DIR + "/" + target_dir,
			BuildTarget.Android, BuildOptions.None);
	}

	[MenuItem("Custom/CI/Build Windows 64")]
	static void PerformWindows64Build() {
		string target_dir = "Windows/" + APP_NAME + "64.exe";
		GenericBuild(SCENES, TARGET_DIR + "/" + target_dir,
			BuildTarget.StandaloneWindows64, BuildOptions.None);
	}

	[MenuItem("Custom/CI/Build Windows 32")]
	static void PerformWindows32Build() {
		string target_dir = "Windows/" + APP_NAME + ".exe";
		GenericBuild(SCENES, TARGET_DIR + "/" + target_dir,
			BuildTarget.StandaloneWindows, BuildOptions.None);
	}

	[MenuItem("Custom/CI/Build Linux 64")]
	static void PerformLinux64Build() {
		string target_dir = "Linux/" + APP_NAME + "64.sh";
		GenericBuild(SCENES, TARGET_DIR + "/" + target_dir,
			BuildTarget.StandaloneLinux64, BuildOptions.None);
	}

	[MenuItem("Custom/CI/Build Linux 32")]
	static void PerformLinux32Build() {
		string target_dir = "Linux/" + APP_NAME + ".sh";
		GenericBuild(SCENES, TARGET_DIR + "/" + target_dir,
			BuildTarget.StandaloneLinux, BuildOptions.None);
	}

	private static string[] FindEnabledEditorScenes() {
		List<string> EditorScenes = new List<string>();
		foreach (EditorBuildSettingsScene scene in EditorBuildSettings.scenes) {
			if (!scene.enabled) {
				continue;
			}
			EditorScenes.Add(scene.path);
		}
		return EditorScenes.ToArray();
	}

	static void GenericBuild(string[] scenes, string target_dir,
		BuildTarget build_target, BuildOptions build_options) {
		EditorUserBuildSettings.SwitchActiveBuildTarget(build_target);
		string res = BuildPipeline.BuildPlayer(scenes, target_dir, build_target, build_options);
		if (res.Length > 0) {
			throw new Exception("BuildPlayer failure: " + res);
		}
	}
}
