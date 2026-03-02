using HEngine.Core;
using UnityEngine.SceneManagement;

namespace HUnity.Systems
{
    public class HUnitySceneSystem : HSystem
    {
        public string GetCurrentSceneName()
        {
            Scene currentScene = SceneManager.GetActiveScene();
            return currentScene.name;
        }

        public HResultCode ChangeScene(string sceneName)
        {
            if (GetCurrentSceneName() == sceneName)
            {
                return HResultCode.Success;
            }
            
            // TODO: 비동기 로딩 및 로딩 UI 처리
            SceneManager.LoadScene(sceneName);
            return HResultCode.Success;
        }
    }
}