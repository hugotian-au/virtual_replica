﻿using UnityEngine;
using System.Collections.Generic;
using DilmerGames.Enums;
using DilmerGames.Core.Utilities;
using Photon.Pun;

namespace DilmerGames
{
    public class VRDraw : MonoBehaviourPunCallbacks, IPunObservable
    {
        private int current_index = 1;
        private int previous_index = 0;
        private Vector3 trackPosition;
        public Vector3 previous_trackPosition;
        private int numCapVectices;
        private int previous_numCapVectices;
        private Vector3 linePosition;
        public Vector3 previousLinePosition;
        private Vector3 cameraPosition;
        public Vector3 previousCameraPosition;
        private int removeLines = 0;
        private int prev_removeLines = 0;

        private GameObject coordinateGo;

        private Vector3 offPosition = new Vector3(0.221f, 0.0f, -0.486f);
        // private Vector3 offPosition = new Vector3(0.0f, 0.0f, -0.0f);

        private int position_number = 0;

        private bool newLine = false;
        private bool updateLine = false;

        [SerializeField]
        private ControlHand controlHand = ControlHand.NoSet;

        [SerializeField]
        private GameObject objectToTrackMovement;

        private Vector3 prevPointDistance = Vector3.zero;

        private float minDistanceBeforeNewPoint = 0.01f;
        private float previousMinDistanceBeforeNewPoint = 0.01f;

        private float minDrawingPressure = 0.8f;

        private float lineDefaultWidth = 0.010f;
        private float previousLineWidth = 0.010f;

        private int positionCount = 0; // 2 by default
        private int previousPositionCount = 0;

        private List<LineRenderer> lines = new List<LineRenderer>();

        private LineRenderer currentLineRender;

        [SerializeField]
        private Color defaultColor = Color.white;

        [SerializeField]
        private GameObject editorObjectToTrackMovement;

        [SerializeField]
        private bool allowEditorControls = true;
       
        void Awake()
        {

            var trackObject = GameObject.Find("ARContent");
            objectToTrackMovement = trackObject;

            coordinateGo = GameObject.Find("CoordinateConvert");
            
            // AddNewLineRenderer();
        }

        void AddNewLineRenderer()
        {
            positionCount = 0;
            GameObject go = new GameObject($"LineRenderer_{controlHand.ToString()}_{lines.Count}");
            go.transform.parent = objectToTrackMovement.transform;
            go.transform.position = trackPosition;
            LineRenderer goLineRenderer = go.AddComponent<LineRenderer>();
            goLineRenderer.startWidth = lineDefaultWidth;
            goLineRenderer.endWidth = lineDefaultWidth;
            goLineRenderer.useWorldSpace = true;
            goLineRenderer.material = MaterialUtils.CreateMaterial(defaultColor, $"Material_{controlHand.ToString()}_{lines.Count}");
            goLineRenderer.positionCount = 1;
            goLineRenderer.numCapVertices = 90;
            goLineRenderer.SetPosition(0, trackPosition);

            currentLineRender = goLineRenderer;
            lines.Add(goLineRenderer);
        }

        void Update()
        {
            // primary left controller
            if(controlHand == ControlHand.Left && updateLine)
            {
                //VRStats.Instance.firstText.text = $"Axis1D.PrimaryIndexTrigger: {OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger)}";
                // UpdateLine();
                updateLine = false;

                previous_trackPosition = trackPosition;
                previousLineWidth = lineDefaultWidth;
                previousPositionCount = positionCount;
                previous_numCapVectices = numCapVectices;
                // previousLinePosition = linePosition;
                previousMinDistanceBeforeNewPoint = minDistanceBeforeNewPoint;
                previousCameraPosition = cameraPosition;
            }
            else if(controlHand == ControlHand.Left && newLine)
            {
                //VRStats.Instance.secondText.text = $"Button.PrimaryIndexTrigger: {Time.deltaTime}";
                //AddNewLineRenderer();
                newLine = false;
                previous_index = current_index;
            }

            // secondary right controller
            if (controlHand == ControlHand.Right && updateLine)
            {
                //VRStats.Instance.firstText.text = $"Axis1D.SecondaryIndexTrigger: {OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger)}";
                // UpdateLine();
                updateLine = false;
                previous_trackPosition = trackPosition;
                previousLineWidth = lineDefaultWidth;
                previousPositionCount = positionCount;
                previous_numCapVectices = numCapVectices;
                // previousLinePosition = linePosition;
                previousMinDistanceBeforeNewPoint = minDistanceBeforeNewPoint;
                previousCameraPosition = cameraPosition;
            }
            else if(controlHand == ControlHand.Right && newLine)
            {
                //VRStats.Instance.secondText.text = $"Button.SecondaryIndexTrigger: {Time.deltaTime}";
                //AddNewLineRenderer();
                newLine = false;
                previous_index = current_index;
            }

            if (removeLines == 1)
            {
                //ToggleScreen();
                var count = lines.Count;
                int i = 0;
                foreach (var line in lines)
                {
                    if (line != null && (i != count - 1))
                    {
                        line.SetVertexCount(1); 
                    }
                    i = i + 1;
                }
                removeLines = 0;

            }

        }

        void UpdateLine()
        {
            if(prevPointDistance == null)
            {
                prevPointDistance = trackPosition;
            }

            if(prevPointDistance != null)
            {
                Vector3 dir = (trackPosition - cameraPosition).normalized;
                prevPointDistance = trackPosition;
                AddPoint(prevPointDistance, dir);
            }
        }

        void AddPoint(Vector3 position, Vector3 direction)
        {
            //print("current addded position is " + position);
            //print("positionCount is before setposition " + positionCount);
            currentLineRender.SetPosition(positionCount, position);
            positionCount++;
            currentLineRender.positionCount = positionCount + 1;
            currentLineRender.SetPosition(positionCount, position);
            //print("positionCount is " + positionCount);
            
            // send position
            // TCPControllerClient.Instance.UpdateLine(position);
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

            print("Before Receive line points!");
            print("linePosition is: " + linePosition);
            print("previousLinePosition is: " + previousLinePosition);

            if (stream.IsWriting)
            {
               

            }
            else
            {
                removeLines = (int)stream.ReceiveNext();
                current_index = (int)stream.ReceiveNext();
                trackPosition = (Vector3)stream.ReceiveNext();
                cameraPosition = (Vector3)stream.ReceiveNext();
                // defaultColor = (Color)stream.ReceiveNext();
                // minDistanceBeforeNewPoint = (float)stream.ReceiveNext();
                // trackPosition = linePosition/* - offPosition*/;

                

                coordinateGo.transform.localPosition = trackPosition;
                trackPosition = coordinateGo.transform.position;

                if (removeLines == 0)
                {


                    if (previous_index != current_index)
                    {
                        newLine = true;
                        previous_trackPosition = trackPosition;
                        previousLineWidth = lineDefaultWidth;
                        // previousPositionCount = positionCount;
                        // previous_numCapVectices = numCapVectices;
                        previousLinePosition = linePosition;
                        previousMinDistanceBeforeNewPoint = minDistanceBeforeNewPoint;
                        AddNewLineRenderer();
                        previous_index = current_index;
                        newLine = false;
                        position_number = 0;
                        //print("Begin a new line");
                    }
                    else if (current_index != 0)
                    {
                        print("Receive line points");
                        print("linePosition is: " + linePosition);
                        print("previousLinePosition is: " + previousLinePosition);
                        if (linePosition != new Vector3(0.0f, 0.0f, 0.0f))
                        {
                            print("Not equal to vector 0.0");
                        }
                        if (linePosition != previousLinePosition)
                        {
                            print("Line Position and previous Line Position Not equal to vector 0.0");
                        }
                        if ((trackPosition != previous_trackPosition))
                        {
                            updateLine = true;

                            print("Update a old line");

                            previous_trackPosition = trackPosition;
                            // previousCameraPosition = cameraPosition;
                            position_number += 1;
                            UpdateLine();
                            updateLine = false;

                        }
                    }
                }

            }
        }
    }
}