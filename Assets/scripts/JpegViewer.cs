using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(Collider))]
public class JpegViewer : MonoBehaviour, IGvrGazeResponder
{
    public GameObject leftSphere, rightSphere;  // the two spheres
    public int numberOfFrames = 0;
    public float frameRate = 30;

    private Texture2D[] roomFramesLeft;
    private Texture2D[] roomFramesRight;
    private Texture2D[] incFramesLeft;
    private Texture2D[] incFramesRight;
    private bool play;
    private bool scene;

    void Start()
    {
        // set play state
        play = false;
        scene = true;
        // load the frames
        roomFramesLeft = new Texture2D[numberOfFrames];
        roomFramesRight = new Texture2D[numberOfFrames];
        incFramesLeft = new Texture2D[numberOfFrames];
        incFramesRight = new Texture2D[numberOfFrames];
        for (int i = 0; i < numberOfFrames; ++i) {
            roomFramesLeft[i] = (Texture2D)Resources.Load(string.Format("l.{0:d4}", i));
            roomFramesRight[i] = (Texture2D)Resources.Load(string.Format("r.{0:d4}", i));
            incFramesLeft[i] = (Texture2D)Resources.Load(string.Format("inc.l.{0:d4}", i));
            incFramesRight[i] = (Texture2D)Resources.Load(string.Format("inc.r.{0:d4}", i));
        }
        leftSphere.GetComponent<Renderer>().material.mainTexture = roomFramesLeft[0];
        rightSphere.GetComponent<Renderer>().material.mainTexture = roomFramesRight[0];
    }

    public void TogglePlay()
    {
        play = !play;
        leftSphere.GetComponent<Renderer>().material.mainTexture = (scene) ? roomFramesLeft[0] : incFramesLeft[0];
        rightSphere.GetComponent<Renderer>().material.mainTexture = (scene) ? roomFramesRight[0] : incFramesRight[0];
    }

    public void ToggleScene()
    {
        scene = !scene;
        leftSphere.GetComponent<Renderer>().material.mainTexture = (scene) ? roomFramesLeft[0] : incFramesLeft[0];
        rightSphere.GetComponent<Renderer>().material.mainTexture = (scene) ? roomFramesRight[0] : incFramesRight[0];
    }

    void Update()
    {
        if (play) {
            int currentFrame = (int)(Time.time * frameRate) % (numberOfFrames*2);
            currentFrame = (currentFrame >= numberOfFrames) ? numberOfFrames - currentFrame : currentFrame ;
            leftSphere.GetComponent<Renderer>().material.mainTexture = (scene) ? roomFramesLeft[currentFrame] : incFramesLeft[currentFrame];
            rightSphere.GetComponent<Renderer>().material.mainTexture = (scene) ? roomFramesRight[currentFrame] : incFramesRight[currentFrame];
        }   
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
        TogglePlay();
    }

    #endregion
}