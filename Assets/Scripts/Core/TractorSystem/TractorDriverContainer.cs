using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TractorDriverContainer {

    public const string kMainDriverName = "MainDriver";

    private Dictionary<string, ITractorDriver> _tractorDriverDic = new Dictionary<string, ITractorDriver>();
    private ITractorDriver _mainDriver = new TractorDriver();

    private float _progress = 0;

    public float Progress
    {
        get
        {
            return _progress;
        }
        set
        {
            _progress = Mathf.Max(value, MinProgress);
            _progress = Mathf.Min(_progress, MaxProgress);
            _mainDriver.SetProgress(_progress, 1.0f);
        }
    }

    public float MaxProgress = float.MaxValue;
    public float MinProgress = float.MinValue;


    public void MoveOffset(float offset)
    {
        Progress = _progress + offset;
    }

    public TractorDriverContainer()
    {
        _tractorDriverDic[kMainDriverName] = _mainDriver;
    }

    public void RegistrySlaveTractor(string tractorName, ITractorDriver driver)
    {
        if (!_tractorDriverDic.ContainsKey(tractorName))
        {
            _tractorDriverDic.Add(tractorName, driver);
        }
    }

    public void RegistryPart(string tractorName, string partName, ITractorPart part)
    {
        ITractorDriver driver = null;
        if (_tractorDriverDic.TryGetValue(tractorName, out driver))
        {
            driver.RegistryPart(partName, part);
        }
    }
}
