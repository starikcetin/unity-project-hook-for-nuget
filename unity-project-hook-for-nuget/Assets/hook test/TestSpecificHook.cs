using UnityEditor;
using UnityEngine;

namespace starikcetin.UnityProjectHookForNuget.hook_test
{
    [InitializeOnLoad]
    public class TestSpecificHook
    {
        static TestSpecificHook()
        {
            ProjectFileHookManager.HookSpecific("dummyassembly.csproj", HookHandler);
        }

        private static string HookHandler(string fileName, string fileContent)
        {
            Debug.Log($"Specific Hook\nFileName: {fileName}\nContent: {fileContent}");
            return fileContent;
        }
    }
}
