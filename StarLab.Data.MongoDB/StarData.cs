using MongoDB.Bson;

namespace StarLab.Data.MongoDB
{
    internal class StarData : IStar
    {
        public StarData(BsonDocument data)
        {
            if (data.GetElement("Apparent Magnitude").Value.IsDouble)
            {
                ApparentMagnitude = data.GetElement("Apparent Magnitude").Value.AsDouble;
            }
            
            if (data.GetElement("B-V").Value.IsDouble)
            {
                BVColourIndex = data.GetElement("B-V").Value.AsDouble;
            }

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
