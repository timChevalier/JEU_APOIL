#if UNITY_STANDALONE_WIN || (UNITY_EDITOR && UNITY_STANDALONE_WIN) || (UNITY_EDITOR && UNITY_XBOX360) || (UNITY_EDITOR && UNITY_PS3) || (UNITY_EDITOR && UNITY_ANDROID)
//////////////////////////////////////////////////////////////////////
//
// Copyright (c) 2013 Audiokinetic Inc. / All Rights Reserved
//
//////////////////////////////////////////////////////////////////////

using UnityEngine;
using UnityEditor;
using System.Diagnostics;
using System.IO;
using System.Collections.Generic;

// Unity Pro-only Feature

// This example assumes files AkSoundEngineWin32.dll and AkSoundEngineX64.dll
// should exist in Assets/Plugins folder.
// This script needs to be adapted to fit user project (e.g., various names and scenes).

public class AkUnityCustomBuildWindows : MonoBehaviour 
{	
// #if UNITY_EDITOR && UNITY_STANDALONE_WIN
	
    [MenuItem("Wwise Build/Build and Run (Windows 32bit)")]
    public static void BuildGameWin32 ()
    {
		Win32Builder builder = new Win32Builder();
		if ( ! builder.Build() )
			return;

		builder.Run();
    }

    [MenuItem("Wwise Build/Build and Run (Windows 64bit)")]
    public static void BuildGameX64 ()
    {
		X64Builder builder = new X64Builder();
		if ( ! builder.Build() )
			return;
		
		builder.Run();
    }
// #endif // #if UNITY_EDITOR && UNITY_STANDALONE_WIN
}

public abstract class PlatformBuilderWindows
{
	protected string AppBaseName = "AkUnityGame";
	protected string[] ScenesToBuild = new string[] {"Assets/AK/Demo/Scenes/RTPCScene.unity"};
	
	protected BuildTarget platform = BuildTarget.StandaloneWindows;
	protected string dataDir = "Undefined_Platform_dataPath";
	protected string appExtension = "Undefined_Platform_AppExtension";
	protected string appDir = "Undefined_Platform_appDir";
	protected string pluginDir = Path.Combine(Application.dataPath, "Plugins");
	
	public virtual void Prebuild()
	{
		UnityEngine.Debug.Log("Implement the method in subclasses.");
	}

	public bool Build()
	{
		// Get filename.
        appDir = EditorUtility.SaveFolderPanel("Choose Locaton of Built Game", ".", "");

        bool isUserCancelledBuild = appDir == "";
        if (isUserCancelledBuild)
        {
        	UnityEngine.Debug.Log("User cancelled the build. Abort the rest.");
        	return false;
        }
		
        Prebuild();

        // Build player.
		string AppFullPath = GetPlatformAppLocationPath(appDir);
		UnityEngine.Debug.Log("BuildTarget Application location: "+AppFullPath);
		
        BuildPipeline.BuildPlayer(ScenesToBuild, AppFullPath, platform, BuildOptions.None);

        return true;

	}
	
	public virtual void Run()
	{
		// Run the game (Process class from System.Diagnostics).
        Process proc = new Process();
        proc.StartInfo.FileName = GetPlatformAppExecPath(appDir);
        proc.Start();
	}
	
	protected virtual string GetPlatformAppLocationPath(string AppDir)
	{
		string path = Path.Combine(AppDir, AppBaseName+appExtension);
		AkBankPath.ConvertToPosixPath(ref path);
		return path;
	}
	
	protected virtual string GetPlatformAppExecPath(string AppDir)
	{
		return GetPlatformAppLocationPath(AppDir);
	}
	
}

public class Win32Builder : PlatformBuilderWindows
{
	protected string destPlugin = "AkSoundEngine.dll";
	protected string srcPlugin = "AkSoundEngineWin32.dll";

	public Win32Builder()
	{
		platform = BuildTarget.StandaloneWindows;
		dataDir = "_Data";
		appExtension = ".exe";		
	}
	
	public override void Prebuild()
	{

		string srcPath = Path.Combine(pluginDir, srcPlugin);
		FileInfo src = new FileInfo(srcPath);

		// If there is a backup for current architecture, we use it.
		if (src.Exists)
		{
			string destPath = Path.Combine(pluginDir, destPlugin);
			bool IsToOverwrite = true;
			src.CopyTo(destPath, IsToOverwrite);
		}
		else
		{
			throw new FileNotFoundException("Source plugin not found: " + srcPlugin);
		}
		
	}

}

public class X64Builder : Win32Builder
{
	public X64Builder()
	{
		platform = BuildTarget.StandaloneWindows64;
		srcPlugin = "AkSoundEngineX64.dll";
	}

}
#endif // #if UNITY_STANDALONE_WIN || (UNITY_EDITOR && UNITY_STANDALONE_WIN) || (UNITY_EDITOR && UNITY_XBOX360) || (UNITY_EDITOR && UNITY_PS3) || (UNITY_EDITOR && UNITY_ANDROID)