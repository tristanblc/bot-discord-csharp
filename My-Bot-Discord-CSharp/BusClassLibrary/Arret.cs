namespace BusClassLibrary
{
    public class Arret : BaseEntity
    {

        public string  stop_name { get; set; }

        public float xlocation { get; set; }

        public float ylocation { get; set; }

        public string ville { get; set; }
        
        public Arret(string stop_name, float xlocation, float ylocation, string ville)
        {
            this.stop_name = stop_name;
            this.xlocation = xlocation;
            this.ylocation = ylocation;
            this.ville = ville;
        }

    }
}