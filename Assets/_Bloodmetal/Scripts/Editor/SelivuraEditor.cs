using UnityEditor.SceneManagement;
using UnityEditor;

namespace Selivura
{
    public class SelivuraEditor
    {
#if UNITY_EDITOR
        [MenuItem("selivura/Play from main scene")]
        public static void PlayFromMainScene()
        {
            EditorApplication.ExitPlaymode();
            EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
            EditorSceneManager.CloseScene(EditorSceneManager.GetActiveScene(), true);
            EditorSceneManager.OpenScene("Assets/_Bloodmetal/Scenes/Main.unity");
            EditorApplication.EnterPlaymode();
        }
    }
#endif
}
