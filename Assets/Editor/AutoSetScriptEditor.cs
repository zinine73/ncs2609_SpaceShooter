using System.IO;
using Unity.CodeEditor;
using UnityEditor;

[InitializeOnLoad]
public static class AutoSetScriptEditor
{
    // 사용할 에디터 경로 (PC마다 다를 수 있음)
    static readonly string[] Candidates =
    {
        @"C:\Program Files\Microsoft Visual Studio\2022\Community\Common7\IDE\devenv.exe",
        @"C:\Users\" + "%USERNAME%" + @"\AppData\Local\Programs\Microsoft VS Code\Code.exe",
        "/Applications/Visual Studio Code.app",
    };

    static AutoSetScriptEditor()
    {
        // 이미 지정되어 있으면 건드리지 않음
        if (!string.IsNullOrEmpty(CodeEditor.CurrentEditorInstallation) &&
            File.Exists(CodeEditor.CurrentEditorInstallation))
            return;

        foreach (var path in Candidates)
        {
            var p = System.Environment.ExpandEnvironmentVariables(path);
            if (File.Exists(p) || Directory.Exists(p))   // macOS의 .app은 폴더
            {
                CodeEditor.SetExternalScriptEditor(p);
                break;
            }
        }
    }
}