using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameInfo : MonoBehaviour
{
    public Transform barrelAttachmentTransform;
    public Transform clipAttachmentTransform;
    public Transform triggerAttachmentTransform;

    public Transform GetBarrelAttachmentTransform()
    {
        return barrelAttachmentTransform;
    }

    public Transform GetClipAttachmentTransform()
    {
        return clipAttachmentTransform;
    }

    public Transform GetTriggerAttachmentTransform()
    {
        return triggerAttachmentTransform;
    }
}