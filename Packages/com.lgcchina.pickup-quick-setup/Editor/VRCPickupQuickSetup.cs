#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using VRC.SDK3.Components;

public class VRCPickupQuickSetup : Editor
{
    [MenuItem("GameObject/快速创建 Pickup/同步", false, 0)]
    private static void QuickSetupPickupSync(MenuCommand menuCommand)
    {
        GameObject selectedObject = menuCommand.context as GameObject;
        if (selectedObject == null)
        {
            Debug.LogWarning("请先选择一个物体");
            return;
        }

        // 记录 Undo 操作
        Undo.RegisterCompleteObjectUndo(selectedObject, "Quick Setup Pickup Sync");

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
        // 设置 Auto Hold 为 No
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

        Debug.Log($"已为 {selectedObject.name} 快速配置 Pickup 组件（同步模式）");
    }

    [MenuItem("GameObject/快速创建 Pickup/不同步", false, 1)]
    private static void QuickSetupPickupNoSync(MenuCommand menuCommand)
    {
        GameObject selectedObject = menuCommand.context as GameObject;
        if (selectedObject == null)
        {
            Debug.LogWarning("请先选择一个物体");
            return;
        }

        // 记录 Undo 操作
        Undo.RegisterCompleteObjectUndo(selectedObject, "Quick Setup Pickup No Sync");

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
        // 设置 Auto Hold 为 No
        pickup.AutoHold = VRC.SDKBase.VRC_Pickup.AutoHoldMode.No;
        // 启用防止抢夺
        pickup.DisallowTheft = true;

        // 移除 VRC Object Sync（如果存在）
        VRCObjectSync existingSync = selectedObject.GetComponent<VRCObjectSync>();
        if (existingSync != null)
        {
            Undo.DestroyObjectImmediate(existingSync);
        }

        // 设置图层为 Pickup（Layer 13）
        selectedObject.layer = 13;

        Debug.Log($"已为 {selectedObject.name} 快速配置 Pickup 组件（不同步模式）");
    }

    // 验证菜单项是否可用
    [MenuItem("GameObject/快速创建 Pickup/同步", true)]
    private static bool ValidateQuickSetupPickupSync()
    {
        return Selection.activeGameObject != null;
    }

    [MenuItem("GameObject/快速创建 Pickup/不同步", true)]
    private static bool ValidateQuickSetupPickupNoSync()
    {
        return Selection.activeGameObject != null;
    }
}
#endif
