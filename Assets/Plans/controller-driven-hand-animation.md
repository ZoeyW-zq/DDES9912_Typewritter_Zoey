# Project Overview
- Game Title: DDES9912 Typewriter
- High-Level Concept: Implementing advanced hand animations using capacitive touch sensors (手指感应).
- Objective: Guide the user step-by-step through the manual setup process.

# Implementation Steps (Instructional Guide)

## Step 1: Create the Hand Animation Script
1. In the Project window, navigate to `Assets/__My Project/Scripts/`.
2. Right-click and select **Create > C# Script**. Name it `HandCapacitiveController`.
3. Open the script and paste the code (to be provided) which calculates animation values based on Touch and Press states.

## Step 2: Configure the Animator Controller
1. Open the `Assets/HandController/LeftHandAnimController.controller`.
2. Double-click the **Base Layer > Blend Tree** to open the Blend Tree graph.
3. Ensure the parameters `Grip` and `Trigger` are present.
4. Adjust the Blend Tree nodes for `Trigger`:
    - **Position 0.0**: Assign the `l_hand_point_anim` (Raised/Poke).
    - **Position 0.1**: Assign the `l_hand_cap_touch_anim` (Touching).
    - **Position 1.0**: Assign the `l_hand_fist_anim` (Pressed).
5. Repeat for the Right Hand Animator.

## Step 3: Scene Setup
1. Open the scene `Assets/__My Project/Scene/Controller-handTest.unity`.
2. In the Hierarchy, find `XR Origin Hands (XR Rig) > Camera Offset > Left Controller > Left Hand Model`.
3. **Add Component**: Add an `Animator` component.
4. **Assign Controller**: Drag `LeftHandAnimController` into the Controller slot.
5. **Add Component**: Add the `HandCapacitiveController` script you created.
6. Repeat for the `Right Hand Model`.

## Step 4: Map Input Actions
1. On the `HandCapacitiveController` component in the Inspector:
2. For **Trigger Value Action**: Use `XRI Left Interaction/Activate Value`.
3. For **Trigger Touch Action**: Click the "Use Reference" toggle, and search for/create a binding to `<XRController>{LeftHand}/triggerTouched`.
4. Do the same for Grip using `Select Value` and `gripTouched`.

## Step 5: Testing
1. Enter Play Mode.
2. Use your VR headset or the XR Device Simulator to test the three states:
    - Finger off trigger (Point).
    - Finger touching trigger (Slight curl).
    - Trigger pressed (Grab).

# Verification & Testing
- Manual inspection of the hand mesh transitions in the Game view.
- Verify that the Animator parameters in the Inspector change correctly in response to input.
