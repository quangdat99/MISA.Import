﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MISA.Import.Core.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("MISA.Import.Core.Properties.Resources", typeof(Resources).Assembly);
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
        ///   Looks up a localized string similar to Có lỗi xảy ra..
        /// </summary>
        public static string Error_Exception {
            get {
                return ResourceManager.GetString("Error_Exception", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Nữ.
        /// </summary>
        public static string Female {
            get {
                return ResourceManager.GetString("Female", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Nam.
        /// </summary>
        public static string Male {
            get {
                return ResourceManager.GetString("Male", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Nhóm khách hàng không có trong hệ thống..
        /// </summary>
        public static string MsgCustomerGroupIsExist {
            get {
                return ResourceManager.GetString("MsgCustomerGroupIsExist", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Mã khách hàng đã tồn tại trong hệ thống..
        /// </summary>
        public static string MsgDuplicateCustomerCodeExist {
            get {
                return ResourceManager.GetString("MsgDuplicateCustomerCodeExist", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Mã khách hàng đã trùng với khách hàng khác trong tệp nhập khẩu..
        /// </summary>
        public static string MsgDuplicateCustomerCodeImport {
            get {
                return ResourceManager.GetString("MsgDuplicateCustomerCodeImport", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SĐT đã có trong hệ thống..
        /// </summary>
        public static string MsgDuplicatePhoneNumberExist {
            get {
                return ResourceManager.GetString("MsgDuplicatePhoneNumberExist", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SĐT đã trùng với SĐT của khách hàng khác trong tệp nhập khẩu..
        /// </summary>
        public static string MsgDuplicatePhoneNumberImport {
            get {
                return ResourceManager.GetString("MsgDuplicatePhoneNumberImport", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to File rỗng..
        /// </summary>
        public static string MsgFileNull {
            get {
                return ResourceManager.GetString("MsgFileNull", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to File không đúng định dạng..
        /// </summary>
        public static string MsgMalformedFile {
            get {
                return ResourceManager.GetString("MsgMalformedFile", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Khác.
        /// </summary>
        public static string Other {
            get {
                return ResourceManager.GetString("Other", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Không xác định.
        /// </summary>
        public static string Unknown {
            get {
                return ResourceManager.GetString("Unknown", resourceCulture);
            }
        }
    }
}
