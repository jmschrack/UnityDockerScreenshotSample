
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
public class ScreenshotPipeline : MonoBehaviour
{
    public static void TakeScreenshots(){
        var total=EditorSceneManager.sceneCountInBuildSettings;
        

        foreach(EditorBuildSettingsScene s in EditorBuildSettings.scenes){
            
            EditorSceneManager.OpenScene(s.path);
            if(!s.enabled) continue;
            var list=GameObject.FindGameObjectsWithTag("Screenshot");
            Debug.LogFormat("ScreenshotPipeline:: Loaded Scene {0}, Found {1} Screenshot Positions",s.path,list.Length);
            int count=0;
            foreach(GameObject go in list){
                var st=go.GetComponent<ScreenshotTaker>();
                st?.TakeScreenshot(count);
                count++;
            }
        }
    }
}
