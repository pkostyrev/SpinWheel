using DG.Tweening;
using UnityEngine;

public class RotationAnimator : MonoBehaviour
{
    [SerializeField] private Transform _wheel;

    IConfigManager _configManager;
    float _rewardOffsetAngle;
    float _startRotationAngle;

    public void Init(IConfigManager configManager)
    {
        _configManager = configManager;
        _rewardOffsetAngle = 360 / configManager.SectionCount;
        _startRotationAngle = _rewardOffsetAngle / 2;
    }

    public Tween Rotate(int rewardIndex, float time)
    {
        float rotationAngle = _startRotationAngle + 360 * _configManager.RotationSpead * time;
        var endValue = new Vector3(0, 0, rotationAngle + rewardIndex * _rewardOffsetAngle);
        return _wheel.DORotate(endValue, time, RotateMode.FastBeyond360)
        .SetEase(Ease.OutQuint);
    }
}
