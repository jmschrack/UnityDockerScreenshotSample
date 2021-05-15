# UnityDockerScreenshotSample
This is a sample of how to use Github Actions to take screenshots of your project.

 - Add some empty game objects to your scenes.  A screenshot will be taken at their position&rotation.
 - Tag these game objects with the "Screenshot" tag
 - Add the "ScreenshotTaker" component to each of these, adjusting the settings as desired.
 - Run the .github/workflows/screenshots.yml Action on github.
 
 For more in-depth look into using Github Actions with Unity, I highly recommend checking out [Game.CI](https://game.ci/docs)
