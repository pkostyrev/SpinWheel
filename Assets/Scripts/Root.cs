using UnityEngine;

public class Root : MonoBehaviour
{

    [SerializeField] private Factory factory;

    private static Root instance;

    private void Awake()
    {
        Application.targetFrameRate = 60;

        instance = this;

        DontDestroyOnLoad(this.gameObject);
    }


    private IConfigManager configManager;
    public static IConfigManager ConfigManager
    {
        get
        {
            if (instance.configManager == null)
            {
                instance.configManager = instance.factory.CreateConfigManager();
                instance.configManager.Init();
            }
            return instance.configManager;
        }
    }
}
