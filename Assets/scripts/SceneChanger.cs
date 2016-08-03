using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider))]
public class SceneChanger : MonoBehaviour, IGvrGazeResponder
{
    public JpegViewer jpegViewer;

    void Start()
    {
    }

    public void ToggleScene()
    {
        jpegViewer.ToggleScene();
    }

    void LateUpdate()
    {
        GvrViewer.Instance.UpdateState();
        if (GvrViewer.Instance.BackButtonPressed)
        {
            Application.Quit();
        }
    }

    public void ToggleVRMode()
    {
        GvrViewer.Instance.VRModeEnabled = !GvrViewer.Instance.VRModeEnabled;
    }

    public void ToggleDistortionCorrection()
    {
        switch (GvrViewer.Instance.DistortionCorrection)
        {
            case GvrViewer.DistortionCorrectionMethod.Unity:
                GvrViewer.Instance.DistortionCorrection = GvrViewer.DistortionCorrectionMethod.Native;
                break;
            case GvrViewer.DistortionCorrectionMethod.Native:
                GvrViewer.Instance.DistortionCorrection = GvrViewer.DistortionCorrectionMethod.None;
                break;
            case GvrViewer.DistortionCorrectionMethod.None:
            default:
                GvrViewer.Instance.DistortionCorrection = GvrViewer.DistortionCorrectionMethod.Unity;
                break;
        }
    }

    public void ToggleDirectRender()
    {
        GvrViewer.Controller.directRender = !GvrViewer.Controller.directRender;
    }

    public void SetGazedAt(bool gazedAt)
    {
        GetComponent<Renderer>().material.color = gazedAt ? Color.green : Color.red;
    }

    #region IGvrGazeResponder implementation

    void IGvrGazeResponder.OnGazeEnter()
    {
        SetGazedAt(true);
    }

    void IGvrGazeResponder.OnGazeExit()
    {
        SetGazedAt(false);
    }

    void IGvrGazeResponder.OnGazeTrigger()
    {
        ToggleScene();
    }

    #endregion
}