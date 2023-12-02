using System;
using System.Collections.Generic;
using System.IO;

namespace AppWeatherEventNotifier.Helper
{
    public static class DelegateFunction
    {
        public delegate double[] getCoordinates();
        public static getCoordinates funcGetCoordinates;

        public delegate void turnOnScreen();
        public static turnOnScreen funcTurnOnScreen;

        public delegate String getFolderPath();
        public static getFolderPath funcGetFolderPath;

        public delegate String getCachePath();
        public static getCachePath funcGetCachePath;

        public delegate String getInternalCachePath();
        public static getInternalCachePath funcGetInternalCachePath;

        public delegate String getStoreCameraDir();
        public static getStoreCameraDir funcGetStoreCameraDir;

        public delegate String getLibraryDir();
        public static getLibraryDir funcGetLibraryDir;

        public delegate String getStoreCameraDirString();
        public static getStoreCameraDir funcGetStoreCameraDirString;

        public delegate void displayLocationSettingsRequest();
        public static displayLocationSettingsRequest funcDisplayLocationSettingsRequest;

        public delegate bool checkLocationSettingsRequest();
        public static checkLocationSettingsRequest funcCheckLocationSettingsRequest;

        public delegate Stream readStreamFromAssets(string filenmae);
        public static readStreamFromAssets funcReadStreamFromAssets;

        public delegate void stopLocationService();
        public static stopLocationService funcStopLocationService;

        public delegate void bringToFront();
        public static bringToFront funcBringToFront;

        public delegate string getICCD();
        public static getICCD funcGetICCD;

        public delegate List<String> getSimInfo();
        public static getSimInfo funcGetSimInfo;

    }
}