using UnityEngine;

public class TankCardboardHead : CardboardHead {

    // Compute new head pose.
    protected override void UpdateHead()
    {
        if (updated)
        {  // Only one update per frame, please.
            return;
        }
        updated = true;
        Cardboard.SDK.UpdateState();

        if (trackRotation)
        {
            var rot = Cardboard.SDK.HeadPose.Orientation;

            Vector3 rotInVector = rot.eulerAngles;
            rotInVector.x += 270;

            rot = Quaternion.Euler(rotInVector);

            if (target == null)
            {
                transform.localRotation = rot;
            }
            else
            {
                transform.rotation = rot * target.rotation;
            }
        }

        if (trackPosition)
        {
            Vector3 pos = Cardboard.SDK.HeadPose.Position;
            if (target == null)
            {
                transform.localPosition = pos;
            }
            else
            {
                transform.position = target.position + target.rotation * pos;
            }
        }
    }

}
