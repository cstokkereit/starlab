namespace StarLab.Domain
{
    public class StellarDesignation
    {
        private readonly string catalogue;

        private readonly string designation;


        public StellarDesignation(string catalogue, string designation)
        {
            this.designation = designation;
            this.catalogue = catalogue;
        }

        public string Catalogue => catalogue;

        public string Designation => designation;
    }
}
