namespace StarLab.Domain
{
    internal struct Designation : IDesignation
    {
        private readonly string catalogue;

        private readonly string designation;

        public Designation(string designation, string catalogue)
        {
            this.designation = designation;
            this.catalogue = catalogue;
        }

        public string Catalogue => catalogue;

        public override string ToString() => designation;
    }
}
