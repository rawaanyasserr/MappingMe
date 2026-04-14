using UnityEngine;

public class TimelineManager : MonoBehaviour
{
    public Transform content;
    public GameObject timelineItemPrefab;

    void Start()
    {
        LoadTimeline();
    }

    void LoadTimeline()
    {
        foreach (Transform child in content)
            Destroy(child.gameObject);

        foreach (var entry in UserData.Instance.timelineEntries)
        {
            GameObject item = Instantiate(timelineItemPrefab, content);
            item.GetComponent<TimelineItemUI>().Setup(entry);
        }
    }
}