﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Gremlins.DataAccess.Properties {
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
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Gremlins.DataAccess.Properties.Resources", typeof(Resources).Assembly);
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
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Error during data layer operation. [sql].
        /// </summary>
        internal static string DataLayerExceptionMessage {
            get {
                return ResourceManager.GetString("DataLayerExceptionMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Entity reader for specified entity type already registered. [duplicate reader].
        /// </summary>
        internal static string DuplicateEntityType {
            get {
                return ResourceManager.GetString("DuplicateEntityType", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Maping for part already specified. [duplicate part].
        /// </summary>
        internal static string DuplicatePartName {
            get {
                return ResourceManager.GetString("DuplicatePartName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Part parameter for class or collection entity read operation must be set. [read part must be set].
        /// </summary>
        internal static string MapperPartMustBeSet {
            get {
                return ResourceManager.GetString("MapperPartMustBeSet", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Path parameter for any entity read operation must be set. [read path must be set].
        /// </summary>
        internal static string MapperPathMustBeSet {
            get {
                return ResourceManager.GetString("MapperPathMustBeSet", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Mapping is not specified. [mapping must be set].
        /// </summary>
        internal static string MappingNotSpecified {
            get {
                return ResourceManager.GetString("MappingNotSpecified", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Output collection is empty. [output is empty].
        /// </summary>
        internal static string OutputCollectionHasNoItems {
            get {
                return ResourceManager.GetString("OutputCollectionHasNoItems", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Output collection is not specified. [output is null].
        /// </summary>
        internal static string OutputCollectionNotSpecified {
            get {
                return ResourceManager.GetString("OutputCollectionNotSpecified", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Leading primary reader not found. [reader not found].
        /// </summary>
        internal static string PrimaryMapperNotRegistered {
            get {
                return ResourceManager.GetString("PrimaryMapperNotRegistered", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Instance of mapper specified for accessot is null. [mapper is null].
        /// </summary>
        internal static string RegistrationOfNullMapper {
            get {
                return ResourceManager.GetString("RegistrationOfNullMapper", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Non primary output parameter has to contain primary parameter. [invalid non primary output parameter].
        /// </summary>
        internal static string SecondaryOutputParameterHaveToBeSplited {
            get {
                return ResourceManager.GetString("SecondaryOutputParameterHaveToBeSplited", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Setter action for collection read must be set. [setter action must be set].
        /// </summary>
        internal static string SetterActionMustBeSet {
            get {
                return ResourceManager.GetString("SetterActionMustBeSet", resourceCulture);
            }
        }
    }
}
