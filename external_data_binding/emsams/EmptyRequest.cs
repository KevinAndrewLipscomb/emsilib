﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.5466
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This source code was auto-generated by xsd, Version=2.0.50727.3038.
// 
namespace external_data_binding.emsams.EmptyRequest {
    using System.Xml.Serialization;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://ems.health.state.pa.us/WcfWebServices/Schemas/EmptyRequest")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace="http://ems.health.state.pa.us/WcfWebServices/Schemas/EmptyRequest", IsNullable=false)]
    public partial class EmptyRequest {
        
        private string gUIDField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string GUID {
            get {
                return this.gUIDField;
            }
            set {
                this.gUIDField = value;
            }
        }
    }
}
