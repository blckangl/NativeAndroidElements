using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NativeAndroidElements
{
    public class TextIntent
    {
        public void ShareText(string shareSubject, string shareMessage, string shareTag)
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
                    //Create intent for action send
                    AndroidJavaClass intentClass =
                        new AndroidJavaClass("android.content.Intent");
                    AndroidJavaObject intentObject =
                        new AndroidJavaObject("android.content.Intent");
                    intentObject.Call<AndroidJavaObject>
                        ("setAction", intentClass.GetStatic<string>("ACTION_SEND"));

                    //put text and subject extra
                    intentObject.Call<AndroidJavaObject>("setType", "text/plain");
                    intentObject.Call<AndroidJavaObject>
                        ("putExtra", intentClass.GetStatic<string>("EXTRA_SUBJECT"), shareSubject);
                    intentObject.Call<AndroidJavaObject>
                        ("putExtra", intentClass.GetStatic<string>("EXTRA_TEXT"), shareMessage);

                    //call createChooser method of activity class

                    AndroidJavaObject chooser =
                        intentClass.CallStatic<AndroidJavaObject>
                            ("createChooser", intentObject, shareTag);
                    activity.Call("startActivity", chooser);
                }));
            }
#endif
        }
    }
}