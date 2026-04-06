using UnityEngine;
using TMPro;

public class TimelineManager : MonoBehaviour
{
    public Transform content;
    public GameObject timelineItemPrefab;

    void Start()
    {
        Debug.Log("TimelineManager started");

        if (UserData.Instance == null)
        {
            Debug.LogError("UserData.Instance is NULL in Timeline.");
            return;
        }

        Debug.Log("Timeline entries count: " + UserData.Instance.timelineEntries.Count);

        if (content == null)
        {
            Debug.LogError("Content is NOT assigned in TimelineManager.");
            return;
        }

        if (timelineItemPrefab == null)
        {
            Debug.LogError("Timeline prefab is NOT assigned in TimelineManager.");
            return;
        }

        foreach (Transform child in content)
        {
            Destroy(child.gameObject);
        }

        foreach (TimelineEntry entry in UserData.Instance.timelineEntries)
        {
            Debug.Log("Creating item: " + entry.actionText);

            GameObject item = Instantiate(timelineItemPrefab, content);

            TMP_Text action = item.transform.Find("TextContainer/ActionText")?.GetComponent<TMP_Text>();
            TMP_Text meta = item.transform.Find("TextContainer/MetaText")?.GetComponent<TMP_Text>();

            if (action == null || meta == null)
            {
                Debug.LogError("Could not find ActionText or MetaText in prefab.");
                return;
            }

            action.text = entry.actionText;
            meta.text = entry.metaText;
        }
    }
}