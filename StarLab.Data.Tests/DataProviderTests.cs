namespace StarLab.Data
{
    public class DataProviderTests
    {
        // Create and populate a test database
        // Run tests
        // Remove test database


        [Ignore("Crashes MongDB occasionally")]
        public void TestGetStars()
        {
            var provider = new DataProvider();

            provider.Connect("localhost:27017", "local");

            var stars = provider.GetStars();

            Assert.That(stars, Is.Not.Null);

            for (int n = 0; n < stars.Count; n++)
            {
                var star = stars[n];
            }

            Assert.That(true);
        }
    }
}
