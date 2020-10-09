using System;
using System.Xml.Serialization;

namespace Domogeek.Net.Api.Models.External
{
    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    [XmlRoot("cartevigilance", Namespace = "", IsNullable = false)]
    public partial class CarteVigilance
    {

        /// <remarks/>
        //public cartevigilanceEntetevigilance entetevigilance
        //{
        //    get;set;
        //}

        /// <remarks/>
        [XmlElement("datavigilance")]
        public DataVigilance[] Data
        {
            get; set;
        }
    }

    ///// <remarks/>
    //[Serializable()]
    //[System.ComponentModel.DesignerCategory("code")]
    //[System.Xml.Serialization.XmlType(AnonymousType = true)]
    //public partial class EnteteVigilance
    //{

    //    /// <remarks/>
    //    public textField vigilanceconseil { get; set; }

    //    /// <remarks/>
    //    public textField vigilancecommentaire
    //    {
    //        get; set;
    //    }

    //    /// <remarks/>
    //    [System.Xml.Serialization.XmlAttribute()]
    //    public ulong dateinsert
    //    {
    //        get; set;
    //    }

    //    /// <remarks/>
    //    [System.Xml.Serialization.XmlAttribute()]
    //    public ulong dateprevue
    //    {
    //        get; set;
    //    }

    //    /// <remarks/>
    //    [System.Xml.Serialization.XmlAttribute()]
    //    public ulong daterun
    //    {
    //        get; set;
    //    }

    //    /// <remarks/>
    //    [System.Xml.Serialization.XmlAttribute()]
    //    public byte echeance
    //    {
    //        get; set;
    //    }

    //    /// <remarks/>
    //    [System.Xml.Serialization.XmlAttribute()]
    //    public byte noversion
    //    {
    //        get; set;
    //    }

    //    /// <remarks/>
    //    [System.Xml.Serialization.XmlAttribute()]
    //    public string producteur
    //    {
    //        get; set;
    //    }

    //    /// <remarks/>
    //    [System.Xml.Serialization.XmlAttribute()]
    //    public byte typeprev
    //    {
    //        get; set;
    //    }
    //}

    ///// <remarks/>
    //[Serializable()]
    //[System.ComponentModel.DesignerCategory("code")]
    //[System.Xml.Serialization.XmlType(AnonymousType = true)]
    //public partial class textField
    //{

    //    /// <remarks/>
    //    [System.Xml.Serialization.XmlAttribute()]
    //    public string texte
    //    {
    //        get; set;
    //    }
    //}

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    public partial class DataVigilance
    {
        /// <remarks/>
        [XmlElement("risque")]
        public Risque Risque
        {
            get; set;
        }

        /// <remarks/>
        [XmlElement("crue")]
        public Crue Crue
        {
            get; set;
        }
         
        /// <remarks/>
        [XmlAttribute("couleur")]
        public byte Couleur
        {
            get; set;
        }
        
        public CouleurEnum CouleurValue => (CouleurEnum)Enum.Parse(typeof(CouleurEnum), Couleur.ToString());

        /// <remarks/>
        [XmlAttribute("dep")]
        public string Departement
        {
            get; set;
        }
    }

    public enum CouleurEnum
    {
        Vert = 1,
        Jaune = 2,
        Orange = 3,
        Rouge = 4,
        Gris = 0
    }

    public enum RisqueEnum
    {
        Vent = 1,
        Pluie_Inondation = 2,
        Orages = 3,
        Inondation = 4,
        Neige = 5,
        Canicule = 6,
        Grand_Froid = 7,
        Avalanches = 8,
        Vagues_Submersion = 9,
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    public partial class Risque
    {
        /// <remarks/>
        [XmlAttribute("valeur")]
        public byte Valeur
        {
            get; set;
        }
        public RisqueEnum RisqueValeur => (RisqueEnum) Enum.Parse(typeof(RisqueEnum), Valeur.ToString()) ;
    }

    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    public partial class Crue
    {

        /// <remarks/>
        [XmlAttribute("valeur")]
        public byte Valeur
        {
            get; set;
        }

        public CouleurEnum CrueValeur => (CouleurEnum)Enum.Parse(typeof(CouleurEnum), Valeur.ToString());
    }
}
