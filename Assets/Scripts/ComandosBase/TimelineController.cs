using UnityEngine;
using UnityEngine.Playables;
using System;

public class TimelineController : MonoBehaviour
{
    public PlayableDirector diretor;
    public Action aoTerminarTimeline;

    public void IniciarTimeline()
    {
        diretor.Play();
        diretor.stopped += OnTimelineTerminou;
    }

    private void OnTimelineTerminou(PlayableDirector d)
    {
        aoTerminarTimeline?.Invoke();
        diretor.stopped -= OnTimelineTerminou;
    }
}
