using System.Collections.Generic; 

namespace Global.Scripts.Generated { 
    public static class Address { 
        public static HashSet<string> AllKeys = new() {
            "WindowRegistry",
            "MainScene",
            "SpriteAtlas",
            "MainScreen",
            "MainCanvasContainer",
            "LoadingScreen",
            "LibraryLoadingScreen",
            "Application",
            "DIContainer",
            "SelectLibraryWindow",
            "ColorSchemeConfiguration",
        }; 
        public static class Registries { 
            public const System.String WindowRegistry = "Assets/Content/Features/WindowManagerModule/GameResources/WindowRegistry.asset"; 
            public static HashSet<string> AllKeys = new() {
                "WindowRegistry",
            }; 
        } 

        public static class Scenes { 
            public const System.String MainScene = "MainScene"; 
            public static HashSet<string> AllKeys = new() {
                "MainScene",
            }; 
        } 

        public static class Sprites { 
            public const System.String SpriteAtlas = "Assets/GameResources/Sprites/SpriteAtlas.spriteatlasv2"; 
            public static HashSet<string> AllKeys = new() {
                "SpriteAtlas",
            }; 
        } 

        public static class UIScreens { 
            public const System.String MainScreen = "Assets/Content/Features/MainScreenModule/GameResources/MainScreen.prefab"; 
            public const System.String MainCanvasContainer = "Assets/Content/Features/ScreensModule/GameResources/MainCanvasContainer.prefab"; 
            public const System.String LoadingScreen = "Assets/Content/Features/LoadingScreenModule/GameResources/LoadingScreen.prefab"; 
            public const System.String LibraryLoadingScreen = "Assets/Content/Features/LibraryLoadingModule/GameResources/LibraryLoadingScreen.prefab"; 
            public static HashSet<string> AllKeys = new() {
                "MainScreen",
                "MainCanvasContainer",
                "LoadingScreen",
                "LibraryLoadingScreen",
            }; 
        } 

        public static class Prefabs { 
            public const System.String Application = "Assets/Content/Features/SolverApplication/GameResources/Application.prefab"; 
            public const System.String DIContainer = "Assets/Core/DIContainer/GameResources/DIContainer.prefab"; 
            public static HashSet<string> AllKeys = new() {
                "Application",
                "DIContainer",
            }; 
        } 

        public static class Windows { 
            public const System.String SelectLibraryWindow = "SelectLibraryWindow"; 
            public static HashSet<string> AllKeys = new() {
                "SelectLibraryWindow",
            }; 
        } 

        public static class ScriptableObjects { 
            public const System.String ColorSchemeConfiguration = "Assets/Core/ColorSchemeModule/GameResources/ColorSchemeConfiguration.asset"; 
            public static HashSet<string> AllKeys = new() {
                "ColorSchemeConfiguration",
            }; 
        } 

    } 
} 
