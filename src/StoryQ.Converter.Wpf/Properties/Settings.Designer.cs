﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace StoryQ.Converter.Wpf.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "14.0.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute(@"Story is Story Name
In order to Benefit
As a Role
I want Feature
With Scenario Scenario Name
Given Condition with a simple parameter $true
When operation with a named parameter $id:5
Then Outcome with with an multiword argument {multi-word string}
And Another Outcome with a parameter thats named with spaces {endoftheworld:1999-12-31 23:59 GMT}
And yet another Outcome with $2 parameters {string2:abc}")]
        public string InputText {
            get {
                return ((string)(this["InputText"]));
            }
            set {
                this["InputText"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string SettingsXml {
            get {
                return ((string)(this["SettingsXml"]));
            }
            set {
                this["SettingsXml"] = value;
            }
        }
    }
}
