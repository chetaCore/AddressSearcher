namespace Adressed
{
    public class Address
    {
        public string Name;
        public Coordinates Coordinates;

        public Address()
        {
            Coordinates = new();
        }
    }
}