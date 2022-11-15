# ONEstore Application License Checker Unity Sample
Sample applications for Application License Checker.

[ALC v2 SDK](https://github.com/ONE-store/app_license_checker) is now available for Java.

### How to use ALC v2 SDK for unity?
First, clone the integrated OneStore SDK Unity Package. [Link](https://github.com/ONE-store/unity_plugins)
<br/>After then, follow the guide for unity **Guide** [for Korean](https://dev.onestore.co.kr/wiki/ko/doc/unity-alc-sdk-v2-39945604.html) / [for English](https://dev.onestore.co.kr/wiki/en/doc/using-alc-sdk-v2-in-unity-38077673.html) 

### Caution
These are required libraries for using check licenses.
* com.onestorecorp.core
* com.onestorecorp.auth

### Use a proguard

It's already obfuscated and in aar,so add the package to the proguard rules.
```
# Core proguard rules
-keep class com.gaa.sdk.base.** { *; }
-keep class com.gaa.sdk.auth.** { *; }

# Purchasing proguard rules
-keep class com.gaa.sdk.iap.** { *; }

# Licensing proguard rules
-keep class com.onestore.extern.licensing.** { *; }

```

### Use Licensing Module
```csharp
using OneStore.Alc;

ILicenseCheckCallback callback = new ILicenseCheckCallback() {
    // implements method
}

// License key for your app registered in the ONE store Developer Center.
var licenseKey = "...";
var licenseChecker = new OneStoreAppLicenseCheckerImpl(licenseKey);
licenseChecker.Initialize(callback);
```

***If you want to change older SDK(v1) to SDK(v2), follow this [for Korean](https://dev.onestore.co.kr/wiki/ko/doc/unity-alc-sdk-v2-39945598.html) / [for English](https://dev.onestore.co.kr/wiki/en/doc/upgrading-from-v1-to-v2-38077671.html)***
<br/>If you want to download older SDK(v1), click [This Link](https://github.com/ONE-store/applicense_unitySample/releases/tag/release%2Falc-v1.0.0)
