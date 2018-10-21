using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITractorDriver
{
    string Name { get; set; }

    float Scale { get; set; }

    void RegistrySlaveTractor(ITractorDriver driver);

    void RegistryPart(string partName, ITractorPart part);

    void SetProgress(float progress, float scale = 1.0f);
}
