namespace DemoAPI_Base.Services
{
    public interface IGuidGeneratorService
    {
        string GuidGenerated { get; set; }
    }

    public class GuidGeneratorService : IGuidGeneratorService
    {
        public string GuidGenerated { get; set; }

        public GuidGeneratorService()
        {
            GuidGenerated = Guid.NewGuid().ToString();
        }
    }

    public interface IGuidGeneratorServiceBis
    {
        string GuidGenerated { get; set; }
    }

    public class GuidGeneratorServiceBis : IGuidGeneratorServiceBis
    {
        public string GuidGenerated { get; set; }

        public GuidGeneratorServiceBis()
        {
            GuidGenerated = Guid.NewGuid().ToString();
        }
    }

    public interface IGuidGeneratorServiceTer
    {
        string GuidGenerated { get; set; }
    }

    public class GuidGeneratorServiceTer : IGuidGeneratorServiceTer
    {
        public string GuidGenerated { get; set; }

        public GuidGeneratorServiceTer()
        {
            GuidGenerated = Guid.NewGuid().ToString();
        }
    }
}
