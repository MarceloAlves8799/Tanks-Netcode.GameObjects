using System;
using UnityEditor;
using UnityEditor.SceneManagement;

namespace Tanks
{
    [InitializeOnLoad]
    public static class StartupSceneLoader
    {
        static StartupSceneLoader()
        {
            EditorApplication.playModeStateChanged += LoadStartupScene;
        }

        private static void LoadStartupScene(PlayModeStateChange state)
        {
            if (state.Equals(PlayModeStateChange.ExitingEditMode))
            {
                EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
            }

            if (state.Equals(PlayModeStateChange.EnteredPlayMode))
            {
                if(EditorSceneManager.GetActiveScene().buildIndex != 0)
                {
                    EditorSceneManager.LoadScene(0);
                }
            }
        }
    }
}
