using UnityEngine;
using System.Collections.Generic;
using DilmerGames.Enums;
using DilmerGames.Core.Utilities;
using Photon.Pun;
using System;

namespace DilmerGames
{
    public class VRDraw : MonoBehaviourPunCallbacks, IPunObservable
    {
        private int current_index = 0;
        private Vector3 trackPosition;
        private int numCapVectices;
        private Vector3 linePosition;
        private Vector3 cameraPosition;
        private int removeLines = 0;

        private bool needDrawLine = false;
        private bool lineDrawn = false;

        private GameObject annotation;

        [SerializeField]
        private ControlHand controlHand = ControlHand.NoSet;

        [SerializeField]
        private GameObject objectToTrackMovement;

        private Vector3 prevPointDistance = Vector3.zero;

        private float minDistanceBeforeNewPoint = 0.01f;

        [SerializeField, Range(0, 1.0f)]
        private float minDrawingPressure = 0.8f;

        [SerializeField, Range(0, 1.0f)]
        private float lineDefaultWidth = 0.010f;

        private int positionCount = 0; // 2 by default

        private List<LineRenderer> lines = new List<LineRenderer>();

        private LineRenderer currentLineRender;

        [SerializeField]
        private Color defaultColor = Color.white;

        [SerializeField]
        private GameObject editorObjectToTrackMovement;

        [SerializeField]
        private bool allowEditorControls = true;

        [SerializeField]
        private VRControllerOptions vrControllerOptions;
        
        public VRControllerOptions VRControllerOptions => vrControllerOptions;

        private bool recordStartTime = false;

        private GameObject timer;

        /*
        void Awake() 
        {
#if UNITY_EDITOR
            
            // if we allow editor controls use the editor object to track movement because oculus
            // blocks the movement of LeftControllerAnchor and RightControllerAnchor
            if(allowEditorControls)
            {
                objectToTrackMovement = editorObjectToTrackMovement != null ? editorObjectToTrackMovement : objectToTrackMovement;
            }

#endif
            if (gameObject.name == "VRDrawLeft(Clone)")
            {
                var trackObject = GameObject.Find("OculusTouchForQuestAndRiftS_Left");
                objectToTrackMovement = trackObject;
            }
            if (gameObject.name == "VRDrawRight(Clone)")
            {
                var trackObject = GameObject.Find("OculusTouchForQuestAndRiftS_Right");
                objectToTrackMovement = trackObject;
            }

            AddNewLineRenderer();
        }
        */

        void Start()
        {
            if (gameObject.name == "VRDrawLeft(Clone)")
            {
                var trackObject = GameObject.Find("LeftHandAnchor");
                objectToTrackMovement = trackObject;
            }
            if (gameObject.name == "VRDrawRight(Clone)")
            {
                var trackObject = GameObject.Find("RightHandAnchor");
                objectToTrackMovement = trackObject;
            }

            annotation = GameObject.Find("Annotation");

            AddNewLineRenderer();

            timer = GameObject.Find("Timer");
        }

        void AddNewLineRenderer()
        {
            positionCount = 0;
            GameObject go = new GameObject($"LineRenderer_{controlHand.ToString()}_{lines.Count}");
            go.transform.parent = objectToTrackMovement.transform.parent;
            go.transform.position = objectToTrackMovement.transform.position;
            LineRenderer goLineRenderer = go.AddComponent<LineRenderer>();
            goLineRenderer.startWidth = lineDefaultWidth;
            goLineRenderer.endWidth = lineDefaultWidth;
            goLineRenderer.useWorldSpace = true;
            goLineRenderer.material = MaterialUtils.CreateMaterial(defaultColor, $"Material_{controlHand.ToString()}_{lines.Count}");
            goLineRenderer.positionCount = 1;
            goLineRenderer.numCapVertices = 90;
            goLineRenderer.SetPosition(0, objectToTrackMovement.transform.position);

            // send position
            TCPControllerClient.Instance.AddNewLine(objectToTrackMovement.transform.position);

            currentLineRender = goLineRenderer;
            lines.Add(goLineRenderer);

            current_index = lines.Count;
            trackPosition = objectToTrackMovement.transform.position;
            cameraPosition = Camera.main.transform.position;
        }

        void Update()
        {
            if (annotation == null)
            {
                return;
            }
            //#if !UNITY_EDITOR
            // primary left controller
            cameraPosition = Camera.main.transform.position;
            if (controlHand == ControlHand.Left)
            {
                // Remove all lines
                if(OVRInput.GetDown(OVRInput.RawButton.X))
                {
                    //ToggleScreen();
                    foreach (var line in lines)
                    {
                        if (line != null)
                        {
                            line.SetVertexCount(1);
                        }
                    }
                    removeLines = 1;
                }


                if (OVRInput.Get(OVRInput.Button.Four))
                {
                    needDrawLine = true;
                }
                else
                {
                    needDrawLine = false;
                }
                if (needDrawLine && OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger) > minDrawingPressure)
                {
                    UpdateLine();
                    lineDrawn = true;
                }
                if (lineDrawn && OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger))
                {

                    var script = timer.GetComponent<RecoredDurationTime>();
                    script.hasStudyStarts = true;

                    AddNewLineRenderer();
                    lineDrawn = false;
                    removeLines = 0;
                }
            }

            if (controlHand == ControlHand.Right)
            {
                // Remove all lines
                if(OVRInput.GetDown(OVRInput.RawButton.A))
                {
                    foreach (var line in lines)
                    {
                        if (line != null)
                        {
                            line.SetVertexCount(1);
                        }
                    }
                    removeLines = 1;
                }


                if (OVRInput.Get(OVRInput.Button.Two))
                {
                    needDrawLine = true;
                }
                else
                {
                    needDrawLine = false;
                }
                if (needDrawLine && OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger) > minDrawingPressure)
                {
                    UpdateLine();
                    lineDrawn = true;
                }
                if (lineDrawn && OVRInput.GetUp(OVRInput.Button.SecondaryIndexTrigger))
                {
                    var script = timer.GetComponent<RecoredDurationTime>();
                    script.hasStudyStarts = true;

                    AddNewLineRenderer();
                    lineDrawn = false;
                    removeLines = 0;
                }
            }

    // #endif
        }

        void UpdateLine()
        {
            if(prevPointDistance == null)
            {
                prevPointDistance = objectToTrackMovement.transform.position;
            }

            if(prevPointDistance != null && Mathf.Abs(Vector3.Distance(prevPointDistance, objectToTrackMovement.transform.position)) >= minDistanceBeforeNewPoint)
            {
                Vector3 dir = (objectToTrackMovement.transform.position - Camera.main.transform.position).normalized;
                prevPointDistance = objectToTrackMovement.transform.position;
                AddPoint(prevPointDistance, dir);
            }
        }

        void AddPoint(Vector3 position, Vector3 direction)
        {
            currentLineRender.SetPosition(positionCount, position);
            positionCount++;
            currentLineRender.positionCount = positionCount + 1;
            currentLineRender.SetPosition(positionCount, position);
            
            // send position
            TCPControllerClient.Instance.UpdateLine(position);
        }

        public void UpdateLineWidth(float newValue)
        {
            currentLineRender.startWidth = newValue;
            currentLineRender.endWidth = newValue;
            lineDefaultWidth = newValue;
        }

        public void UpdateLineColor(Color color)
        {
            // in case we haven't drawn anything
            if(currentLineRender.positionCount == 1)
            {
                currentLineRender.material.color = color;
                currentLineRender.material.EnableKeyword("_EMISSION");
                currentLineRender.material.SetColor("_EmissionColor", color);
            }
            defaultColor = color;
        }

        public void UpdateLineMinDistance(float newValue)
        {
            minDistanceBeforeNewPoint = newValue;
        }

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsWriting)
            {
                cameraPosition = Camera.main.transform.position;
                stream.SendNext(removeLines);
                stream.SendNext(current_index);
                stream.SendNext(prevPointDistance);
                print("prevPointDistance is " + prevPointDistance);
                stream.SendNext(cameraPosition);
                if (removeLines == 1)
                {
                    removeLines = 0;
                }
            }
            else
            {

            }
        }
    }
}