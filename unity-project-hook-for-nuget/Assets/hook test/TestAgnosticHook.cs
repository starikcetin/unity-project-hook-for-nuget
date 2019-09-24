using starikcetin.UnityProjectHookForNuget;
using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
public class TestAgnosticHook
{
    static TestAgnosticHook()
    {
        ProjectFileHookManager.HookAgnostic(HookHandler);
    }

    private static string HookHandler(string fileName, string fileContent)
    {
        Debug.Log($"Agnostic Hook\nFileName: {fileName}\nContent: {fileContent}");
        return fileContent;
    }
}
