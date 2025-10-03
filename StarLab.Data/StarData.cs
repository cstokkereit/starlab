using MongoDB.Bson;
using StarLab.Domain;

namespace StarLab.Data
{
    internal class StarData : IStar
    {
        public StarData(BsonDocument data)
        {
            ApparentMagnitude = data.GetElement("Apparent Magnitude").Value.AsDouble;
            BVColourIndex = data.GetElement("B-V").Value.AsDouble;
            //Designation = data.GetElement("Designation").Value.AsString;
            //Name = data.GetElement("Name").Value.AsString;
            SpectralType = new SpectralType(data.GetElement("Spectral Type").Value.AsString);
        }

        public double AbsoluteMagnitude {  get; private set; }

        public double ApparentMagnitude { get; private set; }

        public double BVColourIndex { get; private set; }

        public string Designation { get; private set; }

        public string Name { get; private set; }

        public SpectralType SpectralType { get; private set; }
    }
}
