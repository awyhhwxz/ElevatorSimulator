using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonStateHandler : MonoBehaviour {

    private float _buttonDownDuration = 0;
    private bool _isButtonDown = false;
    private float kMinButtonDownDuration = 0.3f;

    private int _emissionColorPropertyID = Shader.PropertyToID("_EmissionColor");
    
    public void ActiveInAFlash()
    {
        SetButtonState(true);
        SetButtonState(false);
    }

    public void SetButtonState(bool isDown)
    {
        if(isDown)
        {
            _buttonDownDuration = 0;
            this.enabled = false;
            if(_isButtonDown != isDown)
            {
                var meshRenderer = this.gameObject.GetComponent<Renderer>();
                MaterialPropertyBlock properties = new MaterialPropertyBlock();
                properties.SetColor(_emissionColorPropertyID, Color.red);
                meshRenderer.SetPropertyBlock(properties);
                meshRenderer.material.SetColor(_emissionColorPropertyID, Color.red);
                meshRenderer.sharedMaterial.EnableKeyword("_EMISSION");
                _isButtonDown = true;
            }

        }
        else
        {
            this.enabled = true;
        }
    }

    private void ResetButtonState()
    {
        var meshRenderer = this.gameObject.GetComponent<Renderer>();
        MaterialPropertyBlock properties = new MaterialPropertyBlock();
        properties.SetColor(_emissionColorPropertyID, Color.black);
        meshRenderer.SetPropertyBlock(properties);
        _isButtonDown = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (_buttonDownDuration > kMinButtonDownDuration)
        {
            ResetButtonState();
            this.enabled = false;
        }

        _buttonDownDuration += Time.deltaTime;
    }
}
