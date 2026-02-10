#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using VRC.SDK3.Components;

public class VRCPickupQuickSetup : Editor
{
    [MenuItem("GameObject/快速创建 Pickup/默认同步", false, 0)]
    private static void QuickSetupPickup(MenuCommand menuCommand)
    {
        GameObject selectedObject = menuCommand.context as GameObject;
        if (selectedObject == null)
        {
            Debug.LogWarning("请先选择一个物体");
            return;
        }

        // 记录 Undo 操作
        Undo.RegisterCompleteObjectUndo(selectedObject, "Quick Setup Pickup");

        // 添加 BoxCollider（如果没有）
        if (selectedObject.GetComponent<BoxCollider>() == null)
        {
            selectedObject.AddComponent<BoxCollider>();
        }

        // 添加 Rigidbody（如果没有）
        Rigidbody rb = selectedObject.GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = selectedObject.AddComponent<Rigidbody>();
        }
        // 配置 Rigidbody：取消重力，打开 Kinematic
        rb.useGravity = false;
        rb.isKinematic = true;

        // 添加 VRC Pickup（如果没有）
        VRCPickup pickup = selectedObject.GetComponent<VRCPickup>();
        if (pickup == null)
        {
            pickup = selectedObject.AddComponent<VRCPickup>();
        }
        // 设置 Auto Hold 为 Yes
        pickup.AutoHold = VRC.SDKBase.VRC_Pickup.AutoHoldMode.No;
        // 启用防止抢夺
        pickup.DisallowTheft = true;

        // 添加 VRC Object Sync（如果没有）
        if (selectedObject.GetComponent<VRCObjectSync>() == null)
        {
            selectedObject.AddComponent<VRCObjectSync>();
        }

        // 设置图层为 Pickup（Layer 13）
        selectedObject.layer = 13;

        Debug.Log($"已为 {selectedObject.name} 快速配置 Pickup 组件（Auto Hold: Yes, Disallow Theft: True, Layer: Pickup）");
    }
// 验证菜单项是否可用
    [MenuItem("GameObject/VRChat/Quick Setup Pickup", true)]
    private static bool ValidateQuickSetupPickup()
    {
        return Selection.activeGameObject != null;
    }
}
#endif
