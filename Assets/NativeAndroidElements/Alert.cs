using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

namespace NativeAndroidElements
{
    public class Alert
    {
        public delegate void ResponseDelegate();

        public ResponseDelegate onSuccess;
        public ResponseDelegate onFail;

        private bool mYesPressed = false;
        private bool mNoPressed = false;

        public Alert()
        {
            onSuccess = null;
        }

        public void onSucessLitener(Action callback)
        {
            onSuccess = new ResponseDelegate(callback);
        }

        public void onFailLitener(Action callback)
        {
            onSuccess = new ResponseDelegate(callback);
        }

        public void Show(string title, string message, [CanBeNull] string successBtn = null,
            [CanBeNull] string failBtn = null)
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
                    AndroidJavaObject alertDialogBuilder =
                        new AndroidJavaObject("android/app/AlertDialog$Builder", activity);

                    alertDialogBuilder.Call<AndroidJavaObject>("setMessage", message);
                    alertDialogBuilder.Call<AndroidJavaObject>("setTitle", title);
                    if (successBtn != null)
                    {
                        alertDialogBuilder.Call<AndroidJavaObject>("setPositiveButton", successBtn,
                            new PositiveButtonListner(this));

                        if (failBtn != null)
                        {
                            alertDialogBuilder.Call<AndroidJavaObject>("setNegativeButton", failBtn,
                                new NegativeButtonListner(this));
                        }
                    }

                    AndroidJavaObject alertObject =
                        alertDialogBuilder.Call<AndroidJavaObject>
                            ("create");
                    alertObject.Call("show");
                }));
            }

#endif
        }

#if UNITY_ANDROID

        private class PositiveButtonListner : AndroidJavaProxy
        {
            private Alert mDialog;

            public PositiveButtonListner(Alert d)
                : base("android.content.DialogInterface$OnClickListener")
            {
                mDialog = d;
            }

            public void onClick(AndroidJavaObject obj, int value)
            {
                mDialog.mYesPressed = true;
                mDialog.mNoPressed = false;
                mDialog.onSuccess.Invoke();
            }
        }


        private class NegativeButtonListner : AndroidJavaProxy
        {
            private Alert mDialog;

            public NegativeButtonListner(Alert d)
                : base("android.content.DialogInterface$OnClickListener")
            {
                mDialog = d;
            }

            public void onClick(AndroidJavaObject obj, int value)
            {
                mDialog.mYesPressed = false;
                mDialog.mNoPressed = true;
                mDialog.onFail.Invoke();
            }
        }


#endif
    }
}