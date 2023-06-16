namespace Addressed
{
    public class Address
    {
        public string Name { get; set; }
        public Coordinates Coordinates { get; set; }

        public Address()
        {
            Coordinates = new Coordinates();
        }
    }
}
