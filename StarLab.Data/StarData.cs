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

        public double AbsoluteMagnitude {  get; }

        public double ApparentMagnitude { get; }

        public double BVColourIndex { get; }

        public string Designation { get; }

        public string Name { get; }

        public SpectralType SpectralType { get; }
    }
}
