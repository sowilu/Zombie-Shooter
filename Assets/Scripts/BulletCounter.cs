using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class BulletCounter : MonoBehaviour
{
    public static BulletCounter instance;

    private TextMeshProUGUI counterBox;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
        
        counterBox = GetComponent<TextMeshProUGUI>();
    }

    public void ShowBulletCount(int bullets)
    {
        counterBox.text = bullets.ToString();
    }
}
