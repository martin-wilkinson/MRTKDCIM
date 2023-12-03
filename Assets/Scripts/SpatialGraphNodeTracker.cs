// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using UnityEngine;
using Microsoft.MixedReality.Toolkit;

using SpatialGraphNode = Microsoft.MixedReality.OpenXR.SpatialGraphNode;
using Microsoft.MixedReality.OpenXR;

namespace Microsoft.MixedReality.QuestMRTK3
{
    internal class SpatialGraphNodeTracker : MonoBehaviour
    {
        private SpatialGraphNode node;

        private readonly FrameTime frametime = FrameTime.OnUpdate;

        public System.Guid Id { get; set; }

        void Update()
        {
            if (node == null || node.Id != Id)
            {
                node = (Id != System.Guid.Empty) ? SpatialGraphNode.FromStaticNodeId(Id) : null;
                Debug.Log("Initialize SpatialGraphNode Id= " + Id);
            }

            if (node != null)
            {
                if (node.TryLocate(frametime, out Pose pose))
                {
                    // If there is a parent to the camera that means we are using teleport and we should not apply the teleport
                    // to these objects so apply the inverse
                    //No Idea how this CameraCache is supposed to be in context
                    /*if (CameraCache.Main.transform.parent != null)
                    {
                        pose = pose.GetTransformedBy(CameraCache.Main.transform.parent);
                    }*/

                    gameObject.transform.SetPositionAndRotation(pose.position, pose.rotation);
                    Debug.Log("Id= " + Id + " QRPose = " + pose.position.ToString("F7") + " QRRot = " + pose.rotation.ToString("F7"));
                }
                else
                {
                    Debug.LogWarning("Cannot locate " + Id);
                }
            }
        }
    }
}