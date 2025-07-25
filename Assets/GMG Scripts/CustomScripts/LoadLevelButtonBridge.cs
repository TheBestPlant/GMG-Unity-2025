using UnityEngine;

public class LoadLevelButtonBridge : MonoBehaviour
{
    public LoadLevelAction loadLevelAction;

    public void LoadLevel()
    {
        if (loadLevelAction != null)
        {
            loadLevelAction.ExecuteAction(gameObject); // or null if you don't need dataObject
        }
    }
}
