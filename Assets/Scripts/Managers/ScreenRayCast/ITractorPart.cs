using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITractorPart
{
    string Name { get; set; }

    void SetProgress(float progress, float scale);
}
