using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlidersManager : MonoBehaviour
{

    private ControlParameters controlParameters;
    
    [SerializeField]
    private List<SliderValue> sliders;
    private List<float> default_parameters;

    // Start is called before the first frame update
    void Start()
    {
        controlParameters = GameObject.Find("ControlParameters").GetComponent<ControlParameters>();
        Init();
        ResetParameters();
    }

    private void Init()
    {
        //FOR EACH PARAMETERS, you need to add a function void updateParameter() with the corresponding slider.
        //Be careful of the order of matching between sliders[] and default_parameters[].
        //Don't forget to add the Panel and the update function in the slider OnValueChanged.
        default_parameters = new List<float>();
        default_parameters.Add(controlParameters.GroundDetection_height);
        default_parameters.Add(controlParameters.GroundDetection_epsilonWidth);
        default_parameters.Add(controlParameters.WallDetection_height);
        default_parameters.Add(controlParameters.WallDetection_epsilonWidth);
        default_parameters.Add(controlParameters.JumpImpulsionSpeed);
        default_parameters.Add(controlParameters.MaxHorizontalSpeed);
        default_parameters.Add(controlParameters.CoyoteTimeDoubleJump);
        default_parameters.Add(controlParameters.CollisionEpsilon);
        default_parameters.Add(controlParameters.DescendingGravityModifier);
    }

    public void ResetParameters()
    {
        for (int i = 0; i < default_parameters.Count; i++)
        {
            sliders[i].UpdateSliderValue(default_parameters[i]);
            sliders[i].UpdateText();
        }
    }
    public void updateGroundHeight()
    {
        controlParameters.GroundDetection_height = sliders[0].GetValue();
        sliders[0].UpdateText();
    }

    public void updateGroundEpsilonWidth()
    {
        controlParameters.GroundDetection_epsilonWidth = sliders[1].GetValue();
        sliders[1].UpdateText();
    }

    public void updateWallDetectionHeight()
    {
        controlParameters.WallDetection_height = sliders[2].GetValue();
        sliders[2].UpdateText();
    }

    public void updateWallDetectionEpsilonWidth()
    {
        controlParameters.WallDetection_epsilonWidth = sliders[3].GetValue();
        sliders[3].UpdateText();
    }

    public void updateJumpImpulsionSpeed()
    {
        controlParameters.JumpImpulsionSpeed = sliders[4].GetValue();
        sliders[4].UpdateText();
    }

    public void updateMaxHorizontalSpeed()
    {
        controlParameters.MaxHorizontalSpeed = sliders[5].GetValue();
        sliders[5].UpdateText();
    }

    public void updateCoyoteTimeDoubleJump()
    {
        controlParameters.CoyoteTimeDoubleJump = sliders[6].GetValue();
        sliders[6].UpdateText();
    }

    public void updateCollisionEpsilon()
    {
        controlParameters.CollisionEpsilon = sliders[7].GetValue();
        sliders[7].UpdateText();
    }

    public void updateDescendingGravityModifier()
    {
        controlParameters.DescendingGravityModifier = sliders[8].GetValue();
        sliders[8].UpdateText();
    }
}
