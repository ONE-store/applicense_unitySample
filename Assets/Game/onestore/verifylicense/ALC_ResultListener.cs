using UnityEngine;
using System;

namespace OneStore
{
    /**
     * The app developer needs to modify the App Licensing post processing in this file.
     */
    public class ALC_ResultListener : MonoBehaviour
    {
        public static event Action<string, string> PrintLog;

        
        public static class ErrorType
        {
            public const int SERVICE_UNAVAILABLE = 2000;            // An unknown error occurred.
            public const int ERROR_DATA_PARSING = 2001;             // Parameter is invalid.
            public const int SERVICE_TIMEOUT = 2100;                // There is no response.
            public const int USER_LOGIN_CANCELED = 2101;            // Please sign in ONE Store.
            public const int ONESTORE_SERVICE_INSTALLING = 2102;    // Please check again after the installation of One store service.
            public const int INSTALL_USER_CANCELED = 2103;          // ONE store install has been canceled. Do you want to ONE store installation?
            public const int NOT_FOREGROUND = 2104;                 // An unknown error occurred.

            public const int RESULT_SERVICE_UNAVAILABLE = 2;
            public const int RESULT_ALC_UNAVAILABLE = 3;
            public const int RESULT_DEVELOPER_ERROR = 5;
            public const int RESULT_ERROR = 6;
            public const int UNKNOWN_ERROR = -1;
        }

        void Awake()
        {
            ALC_CallbackManager.CheckLicenseGrantedEvent += CheckLicenseGrantedEvent;
            ALC_CallbackManager.CheckLicenseDeniedEvent += CheckLicenseDeniedEvent;
            ALC_CallbackManager.CheckLicenseErrorEvent += CheckLicenseErrorEvent;
            ALC_CallbackManager.VerifyLicenseEvent += VerifyLicenseEvent;
        }

        void OnDestroy()
        {
            ALC_CallbackManager.CheckLicenseGrantedEvent -= CheckLicenseGrantedEvent;
            ALC_CallbackManager.CheckLicenseDeniedEvent -= CheckLicenseDeniedEvent;
            ALC_CallbackManager.CheckLicenseErrorEvent -= CheckLicenseErrorEvent;
            ALC_CallbackManager.VerifyLicenseEvent -= VerifyLicenseEvent;
        }

        /**
         * Called when the purchase history is confirmed in App License Checker.
         */
        void CheckLicenseGrantedEvent(string data)
        {
            ALC_Response response = JsonUtility.FromJson<ALC_Response>(data);
            PrintLog("CheckLicenseGrantedEvent", "license:\n" + response.license + "\n\nsignature: \n" + response.signature + "\n");

            // SDK 에서 verifyLicense를 수행 하지만 Third party app 에서도 다시 한번 체크를 한다.
            ALC_Manager.VerifyLicense(Constant.BASE_64_ENCODED_PUBLIC_KEY, response.license, response.signature);
        }

        /**
         * Called when there is no purchase in App License Checker.
         */
        void CheckLicenseDeniedEvent()
        {
            PrintLog("CheckLicenseDeniedEvent", "response is denied");
        }

        /**
         * Called when an error occurs in the app license checker.
         */
        void CheckLicenseErrorEvent(string result)
        {
            string resultData = ALC_CallbackManager.FindStringFromCallBackType(result, ALC_CallbackManager.CallBackType.ErrorResult);
            ErrorResult errorResult = JsonUtility.FromJson<ErrorResult>(resultData);

            // Retry, etc. according to error code
            PrintLog("CheckLicenseErrorEvent", errorResult.ToString());

            int errorCode = errorResult.code;
            if (ErrorType.SERVICE_TIMEOUT == errorCode || ErrorType.RESULT_SERVICE_UNAVAILABLE == errorCode)
            {
                // TODO: Please check the network status.
            }
            else if (ErrorType.USER_LOGIN_CANCELED == errorCode)
            {
                // TODO: You have canceled your one store login.
            }
            else if (ErrorType.ONESTORE_SERVICE_INSTALLING == errorCode)
            {
                // TODO: One Store Service is being installed. Please retry after installation.
            }
            else if (ErrorType.INSTALL_USER_CANCELED == errorCode)
            {
                // TODO: One store service installation has been canceled. Please install One Store.
            }
            else if (ErrorType.NOT_FOREGROUND == errorCode)
            {
                // TODO: You cannot proceed in the background service.
            }
            else if (ErrorType.RESULT_ALC_UNAVAILABLE == errorCode)
            {
                // TODO: Please update your library to the latest version.
            }
            else
            {
                // TODO: Please contact one store.
            }
        }

        /**
         * Called after ALC_Manager.VerifyLicense call.
         */
        void VerifyLicenseEvent(bool result)
        {
            PrintLog("VerifyLicenseEvent", "verify is " + result.ToString());
        }
    }
}
