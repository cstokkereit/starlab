namespace StarLab.Application.Workspace.Documents.Charts.Overlays
{
    /// <summary>
    /// A class for performing unit tests on the <see cref="OverlayDataProvider"/> class.
    /// </summary>
    public class OverlayDataProviderTests
    {
        /// <summary>
        /// Test that the <see cref="OverlayDataProvider()"/> constructor works correctly.
        /// </summary>
        [Test]
        public void TestConstruction()
        {
            var provider = new OverlayDataProvider();

            Assert.That(provider, Is.Not.Null);
        }

        /// <summary>
        /// Test that the <see cref="OverlayDataProvider.GetNamedStarsOverlayData()"/> method works correctly.
        /// </summary>
        [Test]
        public void TestGetNamedStarsOverlayData()
        {
            var provider = new OverlayDataProvider();

            var data = provider.GetNamedStarsOverlayData();

            Assert.That(data, Is.Not.Null);

            var names = data.Labels;

            Assert.That(names.Length, Is.EqualTo(1));
            Assert.That(names[0], Is.EqualTo("Achernar"));
            //Assert.That(names[1], Is.EqualTo("Aldebaran"));
            //Assert.That(names[2], Is.EqualTo("Altair"));
            //Assert.That(names[3], Is.EqualTo("Antares"));
            //Assert.That(names[4], Is.EqualTo("Arcturus"));
            //Assert.That(names[5], Is.EqualTo("Betelgeuse"));
            //Assert.That(names[6], Is.EqualTo("Canopus"));
            //Assert.That(names[7], Is.EqualTo("Capella"));
            //Assert.That(names[8], Is.EqualTo("Deneb"));
            //Assert.That(names[9], Is.EqualTo("Fomalhaut"));
            //Assert.That(names[10], Is.EqualTo("Hadar"));
            //Assert.That(names[11], Is.EqualTo("Pollux"));
            //Assert.That(names[12], Is.EqualTo("Regulus"));
            //Assert.That(names[13], Is.EqualTo("Rigel"));
            //Assert.That(names[14], Is.EqualTo("Sirius"));
            //Assert.That(names[15], Is.EqualTo("Spica"));
            //Assert.That(names[16], Is.EqualTo("Vega"));

            var mv = data.GetValues("AbsoluteMagnitude");

            Assert.That(mv.Length, Is.EqualTo(1));

            Assert.That(mv[0], Is.EqualTo(-2.3));
            //Assert.That(mv[1], Is.EqualTo(-0.7));
            //Assert.That(mv[2], Is.EqualTo(2.2));
            //Assert.That(mv[3], Is.EqualTo(-5.1));
            //Assert.That(mv[4], Is.EqualTo(-0.3));
            //Assert.That(mv[5], Is.EqualTo(-5.6));
            //Assert.That(mv[6], Is.EqualTo(-3.1));
            //Assert.That(mv[7], Is.EqualTo(-0.6));
            //Assert.That(mv[8], Is.EqualTo(-7.1));
            //Assert.That(mv[9], Is.EqualTo(2.0));
            //Assert.That(mv[10], Is.EqualTo(-5.2));
            //Assert.That(mv[11], Is.EqualTo(1.0));
            //Assert.That(mv[12], Is.EqualTo(-0.7));
            //Assert.That(mv[13], Is.EqualTo(-7.1));
            //Assert.That(mv[14], Is.EqualTo(1.4));
            //Assert.That(mv[15], Is.EqualTo(-3.3));
            //Assert.That(mv[16], Is.EqualTo(0.5));
        }

        /// <summary>
        /// Test that the <see cref="OverlayDataProvider.GetMagnitudeClassesOverlayData()"/> method works correctly.
        /// </summary>
        [Test]
        public void TestGetMagnitudeClassesOverlayData()
        {
            var provider = new OverlayDataProvider();

            var data = provider.GetMagnitudeClassesOverlayData();

            Assert.That(data, Is.Not.Null);
        }
    }
}
