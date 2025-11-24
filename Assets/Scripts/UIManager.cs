using UnityEngine;

public class UIManager : MonoBehaviour
{
    public void QuitGame()
    {
        Application.Quit();

        // 에디터에서 테스트할 때도 종료처럼 보이게
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
