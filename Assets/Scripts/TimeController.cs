using UnityEngine;
using UnityEngine.UI;

public class TimeController : MonoBehaviour
{
    public float timeScaleValue = 1f;

    void Update()
    {
        // timeScale ���� ������ ������ ����
        Time.timeScale = timeScaleValue;
    }
}