using UnityEngine;

//using Valve.VR;  // Make sure OpenVR is referenced

public class OpenVRThumbstickInputDirect : MonoBehaviour
{
    private OpenVRThumbstickInputDirect(IntPtr ptr) : base(ptr)
    {
    }

    // Device index for left controller (can be dynamically queried as needed)
    private uint leftHandDeviceIndex = 0; //OpenVR.k_unTrackedDeviceIndexInvalid;
/*
    void Update()
    {
        var system = OpenVR.System;
        if (system == null)
        {
           Dev("SteamVR", "OpenVR System not initialized");
            return;
        }

        // Get the left hand device index if it is valid
        leftHandDeviceIndex = GetControllerDeviceIndex(ETrackedControllerRole.LeftHand);
        if (leftHandDeviceIndex == 0)
        {
            Dev("SteamVR", "Left Hand Controller not found");
            return;
        }

        // Get the controller state for the left hand
        VRControllerState_t controllerState = new VRControllerState_t();
        if (system.GetControllerState(leftHandDeviceIndex, ref controllerState, (uint)System.Runtime.InteropServices.Marshal.SizeOf(typeof(VRControllerState_t))))
        {
            // Access the thumbstick axis (x, y)
            Vector2 thumbstickValue = new Vector2(controllerState.rAxis0.x, controllerState.rAxis0.y);  // Axis 0 is typically the thumbstick
            Log("SteamVR", $"Thumbstick X: {thumbstickValue.x}, Y: {thumbstickValue.y}");
        }
    }

    // Function to get the device index for the left or right hand controller
    private uint GetControllerDeviceIndex(ETrackedControllerRole role)
    {
        var system = OpenVR.System;
        for (uint i = 0; i < OpenVR.k_unMaxTrackedDeviceCount; i++)
        {
            if (system.GetTrackedDeviceClass(i) == ETrackedDeviceClass.Controller &&
                system.GetControllerRoleForTrackedDeviceIndex(i) == role)
            {
                return i;
            }
        }
        return OpenVR.k_unTrackedDeviceIndexInvalid;
    }*/
}