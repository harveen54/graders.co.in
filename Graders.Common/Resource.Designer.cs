﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.0
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Graders.Common {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class Resource {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resource() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Graders.Common.Resource", typeof(Resource).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The Username and Password you have entered does not exist. Please Sign up with this Id..
        /// </summary>
        public static string CustomerLogin_DoesNotExist {
            get {
                return ResourceManager.GetString("CustomerLogin_DoesNotExist", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The Username and Password do not match..
        /// </summary>
        public static string CustomerLogin_WrongPassword {
            get {
                return ResourceManager.GetString("CustomerLogin_WrongPassword", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to There is some error in the application. Please try again later..
        /// </summary>
        public static string CustomerSignUp_Error {
            get {
                return ResourceManager.GetString("CustomerSignUp_Error", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Password is a required field.
        /// </summary>
        public static string CustomerSignUp_PasswordRequired {
            get {
                return ResourceManager.GetString("CustomerSignUp_PasswordRequired", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to A user with the same email already exists.
        /// </summary>
        public static string CustomerSignUp_UserExistsAlready {
            get {
                return ResourceManager.GetString("CustomerSignUp_UserExistsAlready", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Username is a required field.
        /// </summary>
        public static string CustomerSignUp_UsernameRequired {
            get {
                return ResourceManager.GetString("CustomerSignUp_UsernameRequired", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Please enter Username in the correct format.
        /// </summary>
        public static string CustomerSignUp_WrongEmailFormat {
            get {
                return ResourceManager.GetString("CustomerSignUp_WrongEmailFormat", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Password and Confirm Password do not match.
        /// </summary>
        public static string CustomerSignUp_WrongPassword {
            get {
                return ResourceManager.GetString("CustomerSignUp_WrongPassword", resourceCulture);
            }
        }
    }
}
