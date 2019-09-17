using UnityEngine;
using System.Xml.Linq;
using starikcetin.UnityProjectHookForNuget.utils;
using SyntaxTree.VisualStudio.Unity.Bridge;
using UnityEditor;

namespace starikcetin.UnityProjectHookForNuget
{
    [InitializeOnLoad]
    public class ProjectFileHook
    {
        static ProjectFileHook()
        {
            ProjectFilesGenerator.ProjectFileGeneration += (name, content) =>
            {
                Debug.Log($"name: {name}");

                // parse the document and make some changes
                var document = XDocument.Parse(content);
                XNamespace xmlns = "http://schemas.microsoft.com/developer/msbuild/2003";
                var itemGroup = new XElement(xmlns + "ItemGroup");
                itemGroup.Add(new XElement(xmlns + "Analyzer", new XAttribute("Include", "packages\\StyleCop.Analyzers.1.0.2\\analyzers\\dotnet\\cs\\StyleCop.Analyzers.CodeFixes.dll")));
                itemGroup.Add(new XElement(xmlns + "Analyzer", new XAttribute("Include", "packages\\StyleCop.Analyzers.1.0.2\\analyzers\\dotnet\\cs\\StyleCop.Analyzers.dll")));
                document.Root.Add(itemGroup);
                document.Root.Add(new XElement(xmlns + "ItemGroup", new XElement(xmlns + "AdditionalFiles", new XAttribute("Include", "stylecop.json"))));

                // save the changes using the Utf8StringWriter
                var str = new Utf8StringWriter();
                document.Save(str);

                return str.ToString();
            };
        }
    }
}
