using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EndSlide", menuName = "Text SO/End Slide")]
public class EndSceneSlideText : ScriptableObject
{
    public EndSceneState type;

    public string
        Title,
        SubTitle,
        Body;
}
