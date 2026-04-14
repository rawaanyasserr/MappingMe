using UnityEngine;
using TMPro;

public class TimelineItemUI : MonoBehaviour
{
    public TMP_Text actionText;
    public TMP_Text metaText;

    public void Setup(TimelineEntry entry)
    {
        actionText.text = entry.actionText;
        metaText.text = entry.metaText;
    }
}