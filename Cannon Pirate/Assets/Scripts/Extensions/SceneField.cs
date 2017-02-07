using UnityEngine;

/// <summary>
/// Scene field.
/// BASED ON: http://answers.unity3d.com/answers/1204071/view.html
/// </summary>
[System.Serializable]
public class SceneField
{
    [SerializeField]
    private Object m_SceneAsset;

    [SerializeField]
    private string m_SceneName = "";

    public SceneField(Object m_SceneAsset, string m_SceneName)
    {
        this.m_SceneAsset = m_SceneAsset;
        this.m_SceneName = m_SceneName;
    }

    public Object SceneAsset
    {
        get { return m_SceneAsset; }
    }

    public string SceneName
    {
        get { return m_SceneName; }
    }

    // Makes it work with the existing Unity methods (LoadLevel/LoadScene)
    public static implicit operator string(SceneField sceneField)
    {
        return sceneField.SceneName;
    }
}