using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CalorieCounter.Managers {

    public class CustomSceneManager : MonoBehaviour {

        public void LoadScene(Scene scene) {
            StartCoroutine(LoadSceneAsync(scene));
        }

        private IEnumerator LoadSceneAsync(Scene scene) {
            AsyncOperation async = SceneManager.LoadSceneAsync(scene.ToString());
            yield return new WaitUntil(() => async.isDone);
        }
    }
}