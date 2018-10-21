using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopScreenHandler {

    private Material _numberMaterial;
    private Material _directionMaterial;
    public const string kDisplayNumberPathFormat = "Textures/LayerDisplay/displaynumber{0:D3}";
    public const string kDisplayDirectionPathFormat = "Textures/LayerDisplay/displaydirection_{0}";

    public CopScreenHandler()
    {
        CarCommandReciever.Instance.OnRunTrendChanged += OnRunTrendChangedFunction;
        CarMoveManager.Instance.OnCurrentLayerChanged += OnCurrentLayerChangedFunction;

        _numberMaterial = Resources.Load<Material>("Materials/DisplayNumber");
        _directionMaterial = Resources.Load<Material>("Materials/DisplayDirection");
    }

    private void OnRunTrendChangedFunction(CarMoveManager.Direction direction)
    {
        var tex = Resources.Load<Texture>(string.Format(kDisplayDirectionPathFormat, direction));
        _directionMaterial.SetTexture("_MainTex", tex);
    }

    private void OnCurrentLayerChangedFunction(int layer)
    {
        var tex = Resources.Load<Texture>(string.Format(kDisplayNumberPathFormat, layer));
        _numberMaterial.SetTexture("_MainTex", tex);
    }
}
