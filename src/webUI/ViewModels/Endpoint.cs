namespace Redpier.Web.UI.ViewModels
{
    public class Endpoint : ViewModelBase
    {
        public string Name { get; set; }

        public string Uri { get; set; }

        public string Type { get; set; }

        public Status Status { get; set; } = Status.Offline;
    }

    public enum Status
    {
        Offline = 0,
        Online = 1
    }
}
