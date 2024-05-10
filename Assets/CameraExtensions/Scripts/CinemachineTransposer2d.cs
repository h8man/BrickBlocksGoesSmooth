using Assets.Extensions;
using Unity.Cinemachine;
using System;
using UnityEngine;

namespace Camera2D.CameraExtensions
{
    [DisallowMultipleComponent]
    [CameraPipeline(CinemachineCore.Stage.Body)]
    public class CinemachineTransposer2d : CinemachineComponentBase//CinemachineExtension
    {
        public FollowTarget follow;
        public CinemachineCore.Stage ApplyAfter = CinemachineCore.Stage.Aim;
        public bool smooth;
        public bool stepped;
        [NonSerialized]
        public Vector2 offset = Vector2.zero;
        [NonSerialized]
        public Vector2 distance;
        [NonSerialized] 
        public Vector2 velocity;
        [NonSerialized]
        public float speed;
        [NonSerialized]
        public int PixelPerUnit;

 
        public override bool IsValid { get => enabled && FollowTarget != null; }

        public override CinemachineCore.Stage Stage { get => CinemachineCore.Stage.Body; }

        public override void MutateCameraState(ref CameraState curState, float deltaTime)
        {
            if (!IsValid)
                return;

            //Vector3 dampedPos = FollowTargetPosition;
            //if (VirtualCamera.PreviousStateIsValid && deltaTime >= 0)
            //    dampedPos = previousTargetPosition + VirtualCamera.DetachedFollowTargetDamp(
            //        dampedPos - previousTargetPosition, Damping, deltaTime);
            //previousTargetPosition = dampedPos;
            curState.RawPosition = FollowTargetPosition;

            var brain = CinemachineCore.FindPotentialTargetBrain(this.VirtualCamera);
            if (brain == null || !brain.IsLiveChild(this.VirtualCamera))
                return;

#if UNITY_2023_2_OR_NEWER
            UnityEngine.Rendering.Universal.PixelPerfectCamera pixelPerfectCamera;
#else
            UnityEngine.Experimental.Rendering.Universal.PixelPerfectCamera pixelPerfectCamera;
#endif
            brain.TryGetComponent(out pixelPerfectCamera);
            if (pixelPerfectCamera == null || !pixelPerfectCamera.isActiveAndEnabled)
                return;

#if UNITY_EDITOR
            if (!UnityEditor.EditorApplication.isPlaying && !pixelPerfectCamera.runInEditMode)
                return;
#endif
            PixelPerUnit = pixelPerfectCamera.assetsPPU;

            var newPosition = smooth
                ? Vector2.SmoothDamp(offset, follow.Overshoot, ref velocity, follow.speed, Mathf.Infinity, deltaTime)
                : Vector2.MoveTowards(offset, follow.Overshoot, follow.speed * deltaTime);
            distance = (newPosition - offset);
            speed = distance.magnitude / deltaTime;
            offset = newPosition;

            curState.PositionCorrection += stepped ? offset.ToStep(PixelPerUnit) : offset.ToVector3();
        }
        //        protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
        //        {
        //            if (stage != ApplyAfter)
        //            {
        //                return;
        //            }
        //            CinemachineBrain brain = CinemachineCore.FindPotentialTargetBrain(vcam);
        //            if (brain == null || !CinemachineCore.IsLive(vcam))
        //                return;

        //            UnityEngine.Experimental.Rendering.Universal.PixelPerfectCamera pixelPerfectCamera;
        //            brain.TryGetComponent(out pixelPerfectCamera);
        //            if (pixelPerfectCamera == null || !pixelPerfectCamera.isActiveAndEnabled)
        //                return;

        //#if UNITY_EDITOR
        //            if (!UnityEditor.EditorApplication.isPlaying && !pixelPerfectCamera.runInEditMode)
        //                return;

        //#endif
        //            PixelPerUnit = pixelPerfectCamera.assetsPPU;

        //            var newPosition = smooth
        //                ? Vector2.SmoothDamp(offset, follow.Overshoot, ref velocity, follow.speed, Mathf.Infinity, deltaTime)
        //                : Vector2.MoveTowards(offset, follow.Overshoot, follow.speed * deltaTime);
        //            distance = (newPosition - offset);
        //            speed = distance.magnitude / deltaTime;
        //            offset = newPosition;

        //            state.PositionCorrection += stepped ? offset.ToPixels(PixelPerUnit).ToUnits(PixelPerUnit) : offset.ToVector3();
        //        }
    }
}
