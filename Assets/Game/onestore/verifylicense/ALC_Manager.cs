using UnityEngine;

namespace OneStore
{
    // ALC (App Licensing Check)
    public class ALC_Manager
    {
        private static AndroidJavaObject requestAdapter = null;
        private static AndroidJavaClass jClass = null;

        /**
         * Constructor
         */
        static ALC_Manager()
        {
            jClass = new AndroidJavaClass("com.onestore.licensing.sdk.unity.AlcPlugin");
            requestAdapter = jClass.CallStatic<AndroidJavaObject>("initAndGet", "ALC_CallbackManager");
        }

        /**
         * Destructor
         */
        ~ALC_Manager()
        {
        }

        /**
         * Check license in flexible or strict mode
         */
        public static void CheckLicense(string base64encodedPublicKey, string policy)
        {
            Debug.Log("ALC_Manager CheckLicense - " + policy);
            requestAdapter.Call("checkLicense", base64encodedPublicKey, policy);
        }

        /**
         * Validity is checked using ALC_Response.license and ALC_Response.signature values obtained through CheckLicense.
         */
        public static void VerifyLicense(string base64encodedPublicKey, string signedData, string signature)
        {
            Debug.Log("ALC_Manager VerifyLicense");
            requestAdapter.Call("verifyLicense", base64encodedPublicKey, signedData, signature);
        }

        /**
         * Quit App License Checker.
         */
        public static void Destroy()
        {
            Debug.Log("ALC_Manager Destroy");
            requestAdapter.Call("destroy");
        }
    }

}

