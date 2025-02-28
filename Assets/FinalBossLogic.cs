using UnityEngine;
using UnityEngine.Playables;

public class FinalBossLogic : MonoBehaviour
{
    public PlayableDirector director;
    


    private void Start()
    {
        director.Play();
    }
}
