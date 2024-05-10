using Assets.Extensions;
using Camera2D.CameraExtensions;
using Unity.Cinemachine;
using UnityEngine;

namespace Assets.CameraExtensions.Scripts
{
    public class TargetPredictor : CinemachineExtension
    {
        public FollowTarget follow;
        public CinemachineCore.Stage ApplyAfter = CinemachineCore.Stage.Aim;
        public Vector2 position { get; private set; }
        public Vector2 direction { get; private set; }

        public override void OnTargetObjectWarped(CinemachineVirtualCameraBase vcam, Transform target, Vector3 positionDelta)
        {
            base.OnTargetObjectWarped(vcam, target, positionDelta);
            position += positionDelta.ToVector2();
            direction = Vector2.zero;
            follow?.UpdateTarget(position, Vector2.zero);
            follow?.Center();
        }
        protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
        {
            if (stage != ApplyAfter)
            {
                return;
            }
            direction = (vcam.Follow.position.ToVector2() - position).normalized.RoundToInt();
            position = vcam.Follow.position;


            follow?.UpdateTarget(position, direction);
        }
    }
}
