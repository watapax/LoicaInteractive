using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CambiarDeEscena : MonoBehaviour
{

    public Animator animator;
    public float waitTime;
    bool transicionando;
    int indexScene;

    public void TransicionWithDelay(int _indexScene)
    {
        StartCoroutine(LoadSceneDelay(_indexScene));
    }

    IEnumerator LoadSceneDelay(int _indexScene)
    {
        yield return new WaitForSeconds(waitTime);
        TransicionA(_indexScene);
    }
    public void TransicionA(int _indexScene)
    {
        if (transicionando) return;
        animator.Play("transicionIn");
        indexScene = _indexScene;
        transicionando = true;
        Invoke(nameof(LoadEscena), 1);


    }
    void LoadEscena()
    {
        SceneManager.LoadScene(indexScene);
    }


}
