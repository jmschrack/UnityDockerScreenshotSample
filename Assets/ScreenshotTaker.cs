using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using UnityEngine;

public class ScreenshotTaker : MonoBehaviour
{
    int width = 1920;
    int height = 1080;
    string folder = "Screenshots";
    [SerializeField] string filenamePrefix = "screenshot";
    bool ensureTransparentBackground=false;

    // Start is called before the first frame update
    

    [ContextMenu("Take Screenshot")]
    public void TakeScreenshot(int count=-1){
        folder = GetSafePath(folder.Trim('/'));
        filenamePrefix = GetSafeFilename(filenamePrefix);
        string dir=new DirectoryInfo(folder).FullName;
       
        Directory.Exists(dir);
        Directory.CreateDirectory(dir);
        //string dir = Application.dataPath + "/" + folder + "/";
        string filename;
        if(count>=0)
            filename = filenamePrefix + "_" + DateTime.Now.ToString("yyMMdd_HHmmss")+"_"+count + ".png";
        else
            filename = filenamePrefix + "_" + DateTime.Now.ToString("yyMMdd_HHmmss")+ ".png";
        string path = Path.Combine(dir, filename);
        
        Camera cam =gameObject.AddComponent<Camera>();

        // Create Render Texture with width and height.
        RenderTexture rt = new RenderTexture(width, height, 0);
        
        // Assign Render Texture to camera.
        cam.targetTexture = rt;

        // save current background settings of the camera
        CameraClearFlags clearFlags = cam.clearFlags;
        Color backgroundColor = cam.backgroundColor;

        // make the background transparent when enabled
        if(ensureTransparentBackground) {
            cam.clearFlags = CameraClearFlags.SolidColor;
            cam.backgroundColor = new Color(); // alpha is zero
        }

        // Render the camera's view to the Target Texture.
        cam.Render();

        // restore the camera's background settings if they were changed before rendering
        if(ensureTransparentBackground) {
            cam.clearFlags = clearFlags;
            cam.backgroundColor = backgroundColor;
        }

        // Save the currently active Render Texture so we can override it.
        RenderTexture currentRT = RenderTexture.active;

        // ReadPixels reads from the active Render Texture.
        RenderTexture.active = cam.targetTexture;

        // Make a new texture and read the active Render Texture into it.
        Texture2D screenshot = new Texture2D(width, height);
        screenshot.ReadPixels(new Rect(0, 0, width, height), 0, 0, false);

        // Apply the changes to the screenshot texture.
        screenshot.Apply(false);

        // Save the screnshot.
        Directory.CreateDirectory(dir);
        byte[] png = screenshot.EncodeToPNG();
        File.WriteAllBytes(path, png);

        // Remove the reference to the Target Texture so our Render Texture is garbage collected.
        cam.targetTexture = null;

        // Replace the original active Render Texture.
        RenderTexture.active = currentRT;

        Debug.Log("Screenshot saved to:\n" + path);
        DestroyImmediate(cam);
    }

     public string GetSafePath(string path) {
        return string.Join("_", path.Split(Path.GetInvalidPathChars()));
    }

    public string GetSafeFilename(string filename) {
        return string.Join("_", filename.Split(Path.GetInvalidFileNameChars()));
    }
}
