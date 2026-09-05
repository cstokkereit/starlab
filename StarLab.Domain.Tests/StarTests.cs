using StarLab.Domain.Data;
using StarLab.Domain.Entities;

namespace StarLab.Domain
{
    /// <summary>
    /// 
    /// </summary>
    public class StarTests
    {
        /// <summary>
        /// 
        /// </summary>
        [Test]
        public void TestGetEffectiveTemperature()
        {
            var data = Substitute.For<IEntityData>();

            data.GetDoubleValue("B-V").Returns(0.03);


            var star = new Star(data);

            Assert.That(star.EffectiveTemperature, Is.EqualTo(9766));




        }


    }
}
