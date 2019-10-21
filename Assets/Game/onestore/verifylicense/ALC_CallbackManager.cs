using System.Collections.Generic;
using System;
using UnityEngine;

namespace OneStore
{
    /**
     * Calling through ALC_Manager.cs receives a callback to this class.
     */
    public class ALC_CallbackManager : MonoBehaviour
    {
        public static event Action<string> CheckLicenseGrantedEvent;
        public static event Action CheckLicenseDeniedEvent;
        public static event Action<string> CheckLicenseErrorEvent;

        public static event Action<bool> VerifyLicenseEvent;

        public enum CallBackType
        {
            Granted,
            Denied,
            ErrorResult
        };

        public static Dictionary<CallBackType, string> definedString = new Dictionary<CallBackType, string>() {
            { CallBackType.Granted, "onGranted" },
            { CallBackType.Denied, "onDenied" },
            { CallBackType.ErrorResult, "onErrorResult" }
        };

        /**
         * Response to license check
         * 
         * - callback value
         * 1. onGranted    
         * 2. onDenied     
         * 3. onErrorResult
         */
        public void CheckLicenseListener(string callback)
        {
            Debug.Log("ALC_CallbackManager CheckLicenseListener - " + callback);
            if (callback.Contains(definedString[CallBackType.Granted]))
            {
                string data = FindStringFromCallBackType(callback, CallBackType.Granted);
                if (data.Length > 0)
                {
                    try
                    {
                        CheckLicenseGrantedEvent(data);
                    }
                    catch (Exception e)
                    {
                        CheckLicenseErrorEvent(e.Message);
                    }
                }
            }
            else if (callback.Contains(definedString[CallBackType.Denied]))
            {
                CheckLicenseDeniedEvent();
            }
            else
            {
                CheckLicenseErrorEvent(callback);
            }
        }

        /**
         * Verifies that the value obtained through CheckLicense is normal.
         * @param callback true or false
         */
        public void VerifyListener(string callback)
        {
            Debug.Log("ALC_CallbackManager VerifyListener - " + callback);
            VerifyLicenseEvent("true".Contains(callback));
        }

        /**
         * Returns the following string excluding the CallBackType string from the resulting callback string.
         */
        public static string FindStringFromCallBackType(string data, CallBackType type)
        {
            int length = definedString[type].Length;
            if (data.Substring(0, length).Equals(definedString[type]))
            {
                return data.Substring(length);
            }
            else
            {
                return "";
            }
        }
    }
}
