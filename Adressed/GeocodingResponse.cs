namespace Addressed
{
    public class GeocodingResponse
    {
        public class ResponseData
        {
            public Response Response { get; set; }
        }

        public class Response
        {
            public GeoObjectCollection GeoObjectCollection { get; set; }
        }

        public class GeoObjectCollection
        {
            public FeatureMember[] FeatureMember { get; set; }
        }

        public class FeatureMember
        {
            public GeoObject GeoObject { get; set; }
        }

        public class GeoObject
        {
            public string Description { get; set; }
            public string Name { get; set; }
        }
    }
}