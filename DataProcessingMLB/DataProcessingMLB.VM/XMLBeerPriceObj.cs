using System.Xml.Schema;
using System.Xml.Serialization;

namespace DataProcessingMLB.VM
{
    [XmlRoot(ElementName = "beercostList")]
    public class XMLBeerPriceObj
    {
        public XMLBeerPriceObj(int year, string team, string nickname, double price, double size, double price_per_Ounce)
        {
            this.year = year;
            this.team = team;
            this.nickname = nickname;
            this.price = price;
            this.size = size;
            this.price_per_Ounce = price_per_Ounce;
        }

        [XmlAttribute("noNamespaceLocation", Namespace = XmlSchema.InstanceNamespace)]
        public string attr = @".\xml\BeerCost.xsd";

        [XmlAttribute("year")]
        public int year { get; set; }

        [XmlAttribute("team")]
        public string team { get; set; }

        [XmlAttribute("nickname")]
        public string nickname { get; set; }

        [XmlAttribute("price")]
        public double price { get; set; }


        [XmlAttribute("size")]
        public double size { get; set; }


        [XmlAttribute("price_per_Ounce")]
        public double price_per_Ounce { get; set; }
    }
}