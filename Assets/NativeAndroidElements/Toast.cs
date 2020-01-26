using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NativeAndroidElements
{
    public class Toast
    {
        public static string LENGTH_SHORT = " LENGTH_SHORT";
        public static string LENGTH_LONG = "LENGTH_LONG";


        public static void Show(string message, string length)
        {
#if UNITY_ANDROID
            if (!Application.isEditor)
            {
                AndroidJavaClass unityActivity =
                    new AndroidJavaClass("com.unity3d.player.UnityPlayer");
                var activity = unityActivity.GetStatic<AndroidJavaObject>
                    ("currentActivity");


                activity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
                {
                    AndroidJavaClass toastClass =
                        new AndroidJavaClass("android.widget.Toast");

                    object[] toastParams = new object[3];

                    toastParams[0] = activity;
                    toastParams[1] = message;
                    toastParams[2] = toastClass.GetStatic<int>
                        (length);

                    AndroidJavaObject toastObject =
                        toastClass.CallStatic<AndroidJavaObject>
                            ("makeText", toastParams);
                    toastObject.Call("show");
                }));
            }
#endif
        }
    }
}