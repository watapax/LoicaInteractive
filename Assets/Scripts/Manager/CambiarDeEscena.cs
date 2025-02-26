using UnityEngine;
using UnityEngine.SceneManagement;
public class CambiarDeEscena : MonoBehaviour
{
    public Animator animator;

    bool transicionando;
    int indexScene;
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
