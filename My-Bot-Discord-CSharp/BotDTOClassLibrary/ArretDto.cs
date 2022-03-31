using BusClassLibrary;

namespace BotDTOClassLibrary
{
    public class ArretDto : BaseEntity
    {
        public Guid Id { get; set; }

        public string stop_name { get; set; }

        public float xlocation { get; set; }

        public float ylocation { get; set; }

        public string ville { get; set; }

    }
}