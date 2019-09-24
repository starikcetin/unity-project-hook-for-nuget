using starikcetin.UnityProjectHookForNuget;
using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
public class TestAgnosticHook
{
    static TestAgnosticHook()
    {
        ProjectFileHookManager.HookAgnostic(AgnosticHookHandler);
    }

    private static string AgnosticHookHandler(string fileName, string fileContent)
    {
        Debug.Log($"Agnostic Hook\nFileName: {fileName}\nContent: {fileContent}");
        return fileContent;
    }
}
