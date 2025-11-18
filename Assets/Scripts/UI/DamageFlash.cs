using UnityEngine;
using UnityEngine.UI;

public class DamageFlash : MonoBehaviour
{
    [SerializeField] private Image flashImage;
    [SerializeField] private float flashDuration = 0.15f;

    private Color flashColor;
    private Color clearColor;
    private float t;
    private bool isFlashing;

    private void Start()
    {
        flashColor = new Color(1f, 0f, 0f, 0.35f);  // red with transparency
        clearColor = new Color(1f, 0f, 0f, 0f);      // fully transparent
        flashImage.color = clearColor;
    }

    public void TriggerFlash()
    {
        t = 0f;
        isFlashing = true;
        flashImage.color = flashColor;
    }

    private void Update()
    {
        if (!isFlashing) return;

        t += Time.deltaTime / flashDuration;
        flashImage.color = Color.Lerp(flashColor, clearColor, t);

        if (t >= 1f)
        {
            isFlashing = false;
        }
    }
}