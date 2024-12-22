using UnityEngine;

public class Factory : MonoBehaviour
{
    [SerializeField] private Transform instancesParent;
    [SerializeField] private GameObject configManager;

    public IConfigManager CreateConfigManager() => Instantiate(configManager, instancesParent).GetComponent<IConfigManager>();
}
