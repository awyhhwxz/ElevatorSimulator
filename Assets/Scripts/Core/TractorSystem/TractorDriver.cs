using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TractorDriver : ITractorDriver
{

    private List<ITractorDriver> _slaveTractorDriverTree = new List<ITractorDriver>();
    private Dictionary<string, ITractorPart> _partDic = new Dictionary<string, ITractorPart>();

    public void RegistrySlaveTractor(ITractorDriver driver)
    {
        _slaveTractorDriverTree.Add(driver);
    }

    public void RegistryPart(string partName, ITractorPart part)
    {
        if (!_partDic.ContainsKey(partName))
        {
            _partDic.Add(partName, part);
        }
    }
    
    public string Name { get; set; }

    public float Scale { get; set; }

    public void SetProgress(float progress, float scale)
    {
        foreach(var driver in _slaveTractorDriverTree)
        {
            driver.SetProgress(progress, scale * Scale);
        }

        foreach (var part in _partDic.Values)
        {
            part.SetProgress(progress, scale * Scale);
        }
    }
}
