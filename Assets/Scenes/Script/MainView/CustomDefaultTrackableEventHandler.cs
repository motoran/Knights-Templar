using UnityEngine;
using UnityEngine.Events;
using Vuforia;

//引用 https://qiita.com/kiyossy/items/3e351a2df4f165a9b2d3
public class CustomDefaultTrackableEventHandler : DefaultTrackableEventHandler
{

    public UnityEvent OnTrackingAction;
    public UnityEvent OffTrackingAction;

    protected override void OnTrackingFound()
    {
        base.OnTrackingFound();
        OnTrackingAction.Invoke();
    }

    protected override void OnTrackingLost()
    {
        base.OnTrackingLost();
        OffTrackingAction.Invoke();
    }
}