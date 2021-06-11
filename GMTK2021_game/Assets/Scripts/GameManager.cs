using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Scene and Music manager

    [FMODUnity.EventRef]
    public string StageMusicEvent = "";

    FMOD.Studio.EventInstance stageMusic;
    FMOD.Studio.PARAMETER_ID itemParameterId;

    void Start()
    {
        stageMusic = FMODUnity.RuntimeManager.CreateInstance(StageMusicEvent);
        stageMusic.start();

        FMOD.Studio.EventDescription itemEventDescription;
        stageMusic.getDescription(out itemEventDescription);

        FMOD.Studio.PARAMETER_DESCRIPTION itemParameterDescription;
        itemEventDescription.getParameterDescriptionByName("PARAMETER NAME", out itemParameterDescription);
        itemParameterId = itemParameterDescription.id;
    }

    // Update is called once per frame
    void Update()
    {
        // stageMusic.setParameterByID(itemParameterId, 0);
    }
}
