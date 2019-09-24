using System.Collections.Generic;
using System.IO;
using SyntaxTree.VisualStudio.Unity.Bridge;
using UnityEditor;

namespace starikcetin.UnityProjectHookForNuget
{
    [InitializeOnLoad]
    public class ProjectFileHookManager
    {
        private static readonly Dictionary<string, List<ProjectFileHookHandler>> SpecificHookHandlers = new Dictionary<string, List<ProjectFileHookHandler>>();
        private static readonly List<ProjectFileHookHandler> AgnosticHookHandlers = new List<ProjectFileHookHandler>();

        static ProjectFileHookManager()
        {
            ProjectFilesGenerator.ProjectFileGeneration += (name, content) => ProcessHooks(Path.GetFileName(name), content);
        }

        private static string ProcessHooks(string fileName, string fileContent)
        {
            if (SpecificHookHandlers.ContainsKey(fileName))
            {
                var validSpecificHookHandlers = SpecificHookHandlers[fileName];

                foreach (var hookHandler in validSpecificHookHandlers)
                {
                    fileContent = hookHandler(fileName, fileContent);
                }
            }

            foreach (var hookHandler in AgnosticHookHandlers)
            {
                fileContent = hookHandler(fileName, fileContent);
            }

            return fileContent;
        }

        public static void HookSpecific(string fileName, ProjectFileHookHandler hookHandler)
        {
            if (!SpecificHookHandlers.ContainsKey(fileName))
            {
                SpecificHookHandlers[fileName] = new List<ProjectFileHookHandler> { hookHandler };
            }
            else
            {
                SpecificHookHandlers[fileName].Add(hookHandler);
            }
        }

        public static void HookAgnostic(ProjectFileHookHandler hookHandler)
        {
            AgnosticHookHandlers.Add(hookHandler);
        }
    }
}
